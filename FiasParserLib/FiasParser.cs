using AddressSplitterLib;
using AddressSplitterLib.AO;
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

        public void Parse(string source)
        {
            int currentVarNum = 0;
            foreach (var variant in parser.Parse(source))
            {
                currentVarNum++;

                List<ObjectMargins> prevFindedObjects = null;
                List<string> houseGuids = null;
                List<string> roomGuids = null;

                foreach (var versionObj in variant)
                {
                    if (versionObj.Type?.Level == 8)
                    {
                        houseGuids = GetHousesGUIDVariants(versionObj, prevFindedObjects);
                        continue;
                    }

                    if (versionObj.Type?.Level == 9)
                    {
                        roomGuids = GetRoomsGUIDVariants(versionObj, houseGuids);
                        continue;
                    }

                    var newObj = IterateAddressObject(versionObj, prevFindedObjects);
                    if (newObj.Count > 0) prevFindedObjects = newObj;
                }

                Console.WriteLine("Текщуий номер варианта:{0}", currentVarNum);
                foreach (var item in prevFindedObjects)
                {
                    Console.WriteLine("Last AOGUID:{0} :: AOLEVEL:{1}", item.AOGUID, item.AOLEVEL);
                }
                foreach (var item in houseGuids)
                {
                    Console.WriteLine("Last HOUSEGUID:{0}", item);
                }
                Console.WriteLine("----------");
            }
        }

        private List<string> GetHousesGUIDVariants(AddressObject house, List<ObjectMargins> objects)
        {
            var houseGuids = new List<string>();

            if (objects != null)
            {
                foreach (var obj in objects)
                {
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

        private List<ObjectMargins> IterateAddressObject(AddressObject variantObj, List<ObjectMargins> prevVariants = null)
        {
            if (prevVariants == null)
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

                return firstQuery;
            }
            else
            {
                var finded = new List<ObjectMargins>();

                foreach (var prevVar in prevVariants)
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

                    finded.AddRange(query);
                }
                return finded;
            }
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


    internal struct ObjectMargins
    {
        public string AOGUID;
        public int AOLEVEL;
    }
}
