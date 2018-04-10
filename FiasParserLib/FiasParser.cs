using AddressSplitterLib;
using AddressSplitterLib.AO;
using FiasParserLib.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiasParserLib
{
    public class FiasParser
    {
        private FiasClassesDataContext dataContext;
        private AddressParser parser;

        public FiasParser()
        {
            dataContext = new FiasClassesDataContext();
            parser = new AddressParser(GetDictionaryFromSql());
        }

        public string Parse(string source)
        {
            var variants = new List<SearchVariant>();

            foreach (var variant in parser.Parse(source))
            {
                var sVariant = new SearchVariant();

                int cur = -1;
                foreach (var versionObj in variant)
                {
                    cur++;
                    if (versionObj.Type?.Level == 8)
                    {
                        sVariant.HouseGuids = GetHousesGUIDVariants(versionObj, sVariant.prevObjects);
                        if (sVariant.HouseGuids.Count == 1 && cur == variant.GetCount() - 1) break;
                        continue;
                    }

                    if (versionObj.Type?.Level == 9)
                    {
                        sVariant.RoomGuids = GetRoomsGUIDVariants(versionObj, sVariant.HouseGuids);
                        if (sVariant.RoomGuids.Count == 1 && cur == variant.GetCount() - 1) break;
                        continue;
                    }

                    var newObjs = IterateAddressObject(versionObj, sVariant.prevObjects);
                    sVariant.prevObjects = newObjs;
                }
                variants.Add(sVariant);
            }

            int maxLevelObj = Int32.MinValue;
            foreach (var item in variants)
            {
                if (item.RoomGuids.Count > 0 && 9 > maxLevelObj)
                {
                    maxLevelObj = 9;
                    continue;
                }
                else if (item.HouseGuids.Count > 0 && 8 > maxLevelObj)
                {
                    maxLevelObj = 8;
                    continue;
                }
                foreach (var obj in item.prevObjects)
                {
                    int lvl = obj.AOLEVEL;
                    if (lvl > 7) lvl = 7;

                    if (lvl > maxLevelObj) maxLevelObj = lvl;
                }
            }

            var bestVariants = new List<SearchVariant>();

            foreach (var item in variants)
            {
                if (item.RoomGuids.Count > 0 && maxLevelObj == 9) bestVariants.Add(item);
                if (item.HouseGuids.Count > 0 && maxLevelObj == 8) bestVariants.Add(item);
                if (item.prevObjects.Count == 0 && item.prevObjects[0]!=null && maxLevelObj < 8) bestVariants.Add(item);
            }

            if (bestVariants.Count == 1)
            {
                if (maxLevelObj == 9)
                {
                    if (bestVariants[0].RoomGuids.Count > 1) throw new ManyObjectsFindedException(bestVariants[0],
                           "Найдено много помещений в БД сы одинаковым номером, требуется уточнить.");
                    return (bestVariants[0].RoomGuids[0]);
                }
                if (maxLevelObj == 8)
                {
                    if (bestVariants[0].HouseGuids.Count > 1) throw new ManyObjectsFindedException(bestVariants[0],
                                              "Найдено много строений в БД с одинаковым номером на одной улице, требуется уточнить.");
                    return (bestVariants[0].HouseGuids[0]);
                }
                if (bestVariants[0].prevObjects.Count > 1) throw new ManyObjectsFindedException(bestVariants[0],
                                             "Найдено много адресных обьектов, требуется уточнить.");
                return (bestVariants[0].prevObjects[0].AOGUID);

            }
            else if (bestVariants.Count > 1) throw new NoOneGoodVariantException(bestVariants, "По базе найдено очень много вариаций это адреса, требуется уточнение.");
            throw new Exception("Не удалось найти такой адрес, требуется ручной ввод.");
        }

        private List<string> GetHousesGUIDVariants(AddressObject house, List<ObjectMargins> objects)
        {
            var houseGuids = new List<string>();

            if (objects != null)
            {
                foreach (var obj in objects)
                {
                    if (obj == null) continue;
                    var q = (from h in dataContext.House
                             where h.HOUSENUM == house.Name && h.AOGUID == obj.AOGUID
                             select h.HOUSEGUID).ToList().Distinct();
                    houseGuids.AddRange(q);
                }
            }

            return houseGuids;
        }

        private List<string> GetRoomsGUIDVariants(AddressObject room, List<string> housesGuids)
        {
            var roomsGuids = new List<string>();

            foreach (var house in housesGuids)
            {
                var q = (from r in dataContext.Room
                         where r.FLATNUMBER == room.Name && r.HOUSEGUID == house
                         select r.ROOMGUID).ToList().Distinct();

                roomsGuids.AddRange(q);
            }

            return roomsGuids;
        }

        private List<ObjectMargins> IterateAddressObject(AddressObject variantObj, List<ObjectMargins> prevVariants, bool retrying = false)
        {

            var finded = new List<ObjectMargins>();

            foreach (var prevVar in prevVariants)
            {
                if (prevVar != null)
                {
                    var query = (from obj in dataContext.Object
                                 where obj.FORMALNAME == variantObj.Name && obj.PARENTGUID == prevVar.AOGUID && obj.ACTSTATUS == 1
                                 select new ObjectMargins { AOGUID = obj.AOGUID, AOLEVEL = obj.AOLEVEL }).ToList();

                    int min = Int32.MaxValue;

                    foreach (var obj in query)
                    {
                        if (obj.AOLEVEL >= prevVar.AOLEVEL && obj.AOLEVEL < min) min = obj.AOLEVEL;
                    }

                    for (int i = 0; i < query.Count; i++)
                    {
                        if (query[i].AOLEVEL != min)
                        {
                            query.RemoveAt(i);
                            i--;
                        }
                    }
                    if (query.Count > 0) finded.AddRange(query);
                    else if (!retrying && prevVar?.AOLEVEL < 4)
                    {
                        var allQ = (from obj in dataContext.Object
                                    where obj.PARENTGUID == prevVar.AOGUID && obj.ACTSTATUS == 1
                                    select new ObjectMargins { AOGUID = obj.AOGUID, AOLEVEL = obj.AOLEVEL }).ToList();
                        if (allQ.Count > 0 && allQ.Count < 200)
                        {
                            finded.AddRange(IterateAddressObject(variantObj, allQ, true));
                            finded.AddRange(prevVariants);
                        }
                        finded.Add(null);
                    }
                }
                else
                {
                    var firstQuery = (from obj in dataContext.Object
                                      where obj.FORMALNAME == variantObj.Name && obj.ACTSTATUS == 1
                                      select new ObjectMargins { AOGUID = obj.AOGUID, AOLEVEL = obj.AOLEVEL }).ToList();

                    int min = Int32.MaxValue;
                    foreach (var obj in firstQuery)
                    {
                        if (obj.AOLEVEL < min) min = obj.AOLEVEL;
                    }

                    for (int i = 0; i < firstQuery.Count; i++)
                    {
                        if (firstQuery[i].AOLEVEL != min)
                        {
                            firstQuery.RemoveAt(i);
                            i--;
                        }
                    }
                    finded.AddRange(firstQuery);


                }
            }
            return finded;
        }


        private AOTypeDictionary GetDictionaryFromSql()
        {
            var dictionary = new AOTypeDictionary();

            foreach (var type in dataContext.AddressObjectType)
            {
                dictionary.Add(type.SCNAME, type.LEVEL, type.SOCRNAME);
                dictionary.Add(type.SOCRNAME, type.LEVEL, type.SOCRNAME);
            }
            dictionary.Sort();
            return dictionary;
        }


        public void Close()
        {
            dataContext.Dispose();
        }

    }

}
