using AddressSplitterLib;
using AddressSplitterLib.AO;
using FiasParserLib.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FiasParserLib
{
    public class FiasParser
    {
        private FiasClassesDataContext dataContext;
        private AddressParser parser;
        private AOTypeDictionary dictionary;

        public FiasParser()
        {
            dataContext = new FiasClassesDataContext();
            dictionary = GetDictionaryFromSql();
            parser = new AddressParser(dictionary);
        }

        public ParseResult Parse(string source)
        {
            var variants = new List<SearchVariant>();
            bool alreadyFinded;

            foreach (var variant in parser.Parse(source))
            {
                var sVariant = new SearchVariant();
                alreadyFinded = false;

                int cur = -1;
                foreach (var versionObj in variant)
                {
                    cur++;

                    if (versionObj.Type?.Level == 9)
                    {
                        sVariant.RoomGuids = GetRoomsGUIDVariants(versionObj, sVariant.HouseGuids);
                        if (sVariant.HouseGuids.Count > 0)
                        {
                            sVariant.FullAddress.Append("помещение ");
                            sVariant.FullAddress.Append(versionObj.Name);
                            sVariant.FullAddress.Append("-->");
                        }
                        if (sVariant.RoomGuids.Count == 1 && cur == variant.GetCount() - 1) alreadyFinded = true;
                        continue;
                    }

                    if (versionObj.Type?.Level == 8)
                    {
                        sVariant.HouseGuids = GetHousesGUIDVariants(versionObj, sVariant.PrevObjects);
                        if (sVariant.HouseGuids.Count > 0)
                        {
                            sVariant.FullAddress.Append("строение ");
                            sVariant.FullAddress.Append(versionObj.Name);
                            sVariant.FullAddress.Append("-->");
                        }
                        if (sVariant.HouseGuids.Count == 1 && cur == variant.GetCount() - 1) alreadyFinded = true;
                        continue;
                    }

                    var newObjs = IterateAddressObject(versionObj, sVariant.PrevObjects);
                    if (newObjs.Count > 0)
                    {
                        sVariant.FullAddress.Append(versionObj.Name);
                        sVariant.FullAddress.Append("-->");
                    }
                    sVariant.PrevObjects = newObjs;
                }
                variants.Add(sVariant);
                if (alreadyFinded) break;
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
                foreach (var obj in item.PrevObjects)
                {
                    if (obj == null) continue;

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
                if (item.PrevObjects.Count > 0 && item.PrevObjects[0]!=null && maxLevelObj < 8) bestVariants.Add(item);
            }

            if (bestVariants.Count == 1)
            {
                if (maxLevelObj == 9)
                {
                    if (bestVariants[0].RoomGuids.Count > 1) throw new ManyObjectsFindedException(bestVariants[0],
                           "Найдено много помещений в БД с одинаковым номером, требуется уточнить.");

                    var prRoom = new ParseResult()
                    {
                        id = bestVariants[0].RoomGuids[0],
                        type = IdType.Room,
                        address = bestVariants[0].FullAddress.ToString()
                    };
                    return prRoom;
                }
                if (maxLevelObj == 8)
                {
                    if (bestVariants[0].HouseGuids.Count > 1) throw new ManyObjectsFindedException(bestVariants[0],
                                              "Найдено много строений в БД с одинаковым номером на одной улице, требуется уточнить.");

                    var prHouse = new ParseResult()
                    {
                        id = bestVariants[0].HouseGuids[0],
                        type = IdType.House,
                        address = bestVariants[0].FullAddress.ToString()
                    };
                    return prHouse;
                }
                if (bestVariants[0].PrevObjects.Count > 1) throw new ManyObjectsFindedException(bestVariants[0],
                                             "Найдено много адресных обьектов, требуется уточнить.");

                var prObject = new ParseResult()
                {
                    id = bestVariants[0].PrevObjects[0].AOGUID,
                    type = IdType.Object,
                    address = bestVariants[0].FullAddress.ToString()
                };
                return prObject;
            }
            else if (bestVariants.Count > 1) throw new NoOneGoodVariantException(bestVariants, "По базе найдено очень много вариаций это адреса, требуется уточнение.");
            throw new AddressNotFound("Не удалось найти такой адрес, требуется ручной ввод.");
        }

        private List<string> GetHousesGUIDVariants(AddressObject house, List<ObjectMargins> objects)
        {
            var houseGuids = new List<string>();

            if (objects != null)
            {
                foreach (var obj in objects)
                {
                    if (obj == null) continue;

                    var litter = new StringBuilder();
                    bool numeric = false;

                    foreach (char ch in house.Name)
                    {
                        if (ch == '/')
                        {
                            numeric = true;
                            continue;
                        }
                        if (Char.IsLetter(ch) || numeric) litter.Append(ch);
                    }

                    if (litter.Length > 0)
                    {
                        if (numeric)
                        {
                            var qWithLitter = (from h in dataContext.House
                                               where h.HOUSENUM == house.Name && h.AOGUID == obj.AOGUID && h.BUILDNUM == litter.ToString()
                                               select h.HOUSEGUID).ToList().Distinct();
                            houseGuids.AddRange(qWithLitter);
                        }
                        else
                        {
                            var qWithLitter = (from h in dataContext.House
                                               where h.HOUSENUM == house.Name && h.AOGUID == obj.AOGUID && h.STRUCNUM == litter.ToString()
                                               select h.HOUSEGUID).ToList().Distinct();
                            houseGuids.AddRange(qWithLitter);
                        }
                    }
                    if(houseGuids.Count==0)
                    {
                        var qWithLitter = (from h in dataContext.House
                                           where h.HOUSENUM == house.Name && h.AOGUID == obj.AOGUID && h.STRUCNUM == null && h.BUILDNUM == null
                                           select h.HOUSEGUID).ToList().Distinct();
                        houseGuids.AddRange(qWithLitter);
                    }
                    

                    if (houseGuids.Count == 0)
                    {
                        var q = (from h in dataContext.House
                                 where h.HOUSENUM == house.Name && h.AOGUID == obj.AOGUID
                                 select h.HOUSEGUID).ToList().Distinct();
                        houseGuids.AddRange(q);
                    }
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

                    if(variantObj.Type !=null)
                    {
                        List<AddressSplitterLib.AddressObjectType> types = dictionary.GetAOTypes(variantObj.Type.AbbreviatedName);

                        for (int i = 0; i < firstQuery.Count; i++)
                        {
                            bool abbrFinded = false;

                            foreach (var type in types)
                            {
                                if (firstQuery[i].AOLEVEL == type.Level) abbrFinded = true;
                            }

                            if(!abbrFinded)
                            {
                                firstQuery.RemoveAt(i);
                                i--;
                            }
                        }
                    }

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
                if (!string.IsNullOrEmpty(type.SCNAME))
                    dictionary.Add(new AddressSplitterLib.AddressObjectType(type.SOCRNAME, type.SCNAME, type.LEVEL));
                if (!string.IsNullOrEmpty(type.SOCRNAME))
                    dictionary.Add(new AddressSplitterLib.AddressObjectType(type.SOCRNAME, type.SOCRNAME, type.LEVEL));
            }
            dictionary.Sort();
            return dictionary;
        }


        public void Close()
        {
            dataContext.Dispose();
        }

    }

    public struct ParseResult
    {
        public string id;
        public IdType type;
        public string address;
    }

}
