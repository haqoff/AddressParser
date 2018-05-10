using AddressSplitterLib;
using AddressSplitterLib.AO;
using AddressSplitterLib.Utils;
using FiasParserGUI.Exceptions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FiasParserLib
{
    public class FiasParser
    {
        private FiasClassesDataContext dataContext;
        private AddressParser parser;
        private AOTypeDictionary dictionary;
        private StringBuilder sb;
        private List<string> bannedAbbriviatedNames;


        public FiasParser(string connectionString)
        {
            dataContext = new FiasClassesDataContext(connectionString);
            dataContext.CommandTimeout = 0;
            if (!dataContext.DatabaseExists()) throw new BadLoginException("Не удалось подключиться к БД.");

            bannedAbbriviatedNames = new List<string>();
            bannedAbbriviatedNames.Add("к.");
            bannedAbbriviatedNames.Add("вал");

            sb = new StringBuilder();
            dictionary = GetDictionaryFromSql();

            dictionary.Add(new AddressSplitterLib.AddressObjectType("область", "область", 1, AddressSplitterLib.AddressObjectType.GenderNoun.Fiminine));
            dictionary.Add(new AddressSplitterLib.AddressObjectType("область", "обл", 1, AddressSplitterLib.AddressObjectType.GenderNoun.Fiminine));
            dictionary.Add(new AddressSplitterLib.AddressObjectType("проспект", "пр-т", 7));
            dictionary.Add(new AddressSplitterLib.AddressObjectType("проспект", "пр.", 7));
            dictionary.Add(new AddressSplitterLib.AddressObjectType("квартира", "к", 9));
            dictionary.Sort();
            parser = new AddressParser(dictionary);
        }

        public List<ObjectNode> Parse(string source)
        {
            var bestVariant = new List<ObjectNode>();

            foreach (Variant variant in parser.Parse(source))
            {
                List<ObjectNode> lastFindedNodes = null;

                foreach (var versionObj in variant)
                {

                    if (versionObj.Type?.Level == 9)
                    {
                        var roomNodes = GetRoomsGUIDVariants(versionObj, lastFindedNodes);
                        if (roomNodes.Count > 0) lastFindedNodes = roomNodes;
                    }
                    else if (versionObj.Type?.Level == 8)
                    {
                        var houseNodes = GetHousesGUIDVariants(versionObj, lastFindedNodes);
                        if (houseNodes.Count > 0) lastFindedNodes = houseNodes;
                    }
                    else
                    {
                        lastFindedNodes = IterateAddressObject(versionObj, lastFindedNodes);
                    }
                }


                if (lastFindedNodes?.Count > 0)
                {
                    if (bestVariant.Count == 0) bestVariant = lastFindedNodes;
                    else
                    {
                        if (lastFindedNodes[0].Type > bestVariant[0].Type) bestVariant = lastFindedNodes;
                        else if ((lastFindedNodes[0].Type == bestVariant[0].Type) && (lastFindedNodes.Count < bestVariant.Count))
                            bestVariant = lastFindedNodes;
                    }
                }
                var lastObject = variant.GetAddressObject(variant.GetCount() - 1);
                if (bestVariant.Count == 1 && bestVariant[0].Name.ToLower() == lastObject.Name.ToLower()) return bestVariant;
            }
            return bestVariant;
        }

        private List<ObjectNode> GetHousesGUIDVariants(AddressObject house, List<ObjectNode> objects)
        {
            var houseGuids = new List<ObjectNode>();
            foreach (var obj in objects)
            {
                if (obj == null) continue;

                string housing = StringHelper.GetLettersOrNumbersAfterSlash(house.Name, out int firstIndexHousing, out int lengthHousing);
                string nameWithoutHousing = house.Name;
                if (!string.IsNullOrEmpty(housing))
                    nameWithoutHousing = house.Name.Remove(firstIndexHousing, lengthHousing).Replace("/", "");

                if (house.Type?.AbbreviatedName == "дом с корпусом" && housing.Length > 0)
                {
                    var qWithLitter = (from h in dataContext.House
                                       where h.HOUSENUM == nameWithoutHousing && h.AOGUID == obj.Guid && h.BUILDNUM == housing && h.STRUCNUM == null
                                       select new ObjectNode(h.HOUSEGUID, nameWithoutHousing + ", корпус " + housing, TableType.House, obj.Guid,obj)).ToList().Distinct();
                    houseGuids.AddRange(qWithLitter);

                    if (houseGuids.Count == 0)
                    {
                        var qWithLitter2 = (from h in dataContext.House
                                            where h.HOUSENUM == nameWithoutHousing && h.AOGUID == obj.Guid && h.BUILDNUM == housing
                                            select new ObjectNode(h.HOUSEGUID, nameWithoutHousing + ", корпус " + housing, TableType.House, obj.Guid,obj)).ToList().Distinct();
                        houseGuids.AddRange(qWithLitter2);
                    }
                }

                if (houseGuids.Count == 0)
                {
                    var qWithLitter = (from h in dataContext.House
                                       where h.HOUSENUM == house.Name && h.AOGUID == obj.Guid && h.STRUCNUM == null && h.BUILDNUM == null
                                       select new ObjectNode(h.HOUSEGUID, house.Name, TableType.House, obj.Guid,obj)).ToList().Distinct();
                    houseGuids.AddRange(qWithLitter);
                }

                if (houseGuids.Count == 0 && housing.Length > 0)
                {

                    var qWithNumericHousing = (from h in dataContext.House
                                               where h.HOUSENUM == nameWithoutHousing && h.AOGUID == obj.Guid && (h.BUILDNUM == housing || h.STRUCNUM == housing)
                                               select new ObjectNode(h.HOUSEGUID, house.Name, TableType.House, obj.Guid, obj)).ToList().Distinct();
                    houseGuids.AddRange(qWithNumericHousing);

                }
                if (houseGuids.Count == 0)
                {
                    var q = (from h in dataContext.House
                             where h.HOUSENUM == house.Name && h.AOGUID == obj.Guid
                             select new ObjectNode(h.HOUSEGUID, house.Name, TableType.House, obj.Guid, obj)).ToList().Distinct();
                    houseGuids.AddRange(q);
                }
            }
            return houseGuids;
        }

        private List<ObjectNode> GetRoomsGUIDVariants(AddressObject room, List<ObjectNode> houses)
        {
            var roomsGuids = new List<ObjectNode>();

            foreach (var house in houses)
            {
                var q = (from r in dataContext.Room
                         where r.FLATNUMBER == room.Name && r.HOUSEGUID == house.Guid
                         select new ObjectNode(r.ROOMGUID, room.Name, TableType.Room, house.Guid,house)).ToList().Distinct();

                roomsGuids.AddRange(q);
            }

            return roomsGuids;
        }

        private List<ObjectNode> IterateAddressObject(AddressObject variantObj, List<ObjectNode> prevNodes, bool retrying = false)
        {
            var result = new List<ObjectNode>();

            if (prevNodes != null)
            {
                foreach (var prevNode in prevNodes)
                {
                    var query = (from obj in dataContext.Object
                                 where obj.FORMALNAME == variantObj.Name && obj.PARENTGUID == prevNode.Guid && obj.ACTSTATUS == 1 && obj.AOLEVEL >= prevNode.AOLevel
                                 select new ObjectNode(obj.AOGUID, variantObj.Name, TableType.Object, prevNode.Guid,prevNode, obj.SHORTNAME ,obj.AOLEVEL)).ToList();

                    result.AddRange(query);

                    if (query.Count == 0 && !retrying)
                    {
                        var allQ = (from obj in dataContext.Object
                                    where obj.PARENTGUID == prevNode.Guid && obj.ACTSTATUS == 1
                                    select new ObjectNode(obj.AOGUID, obj.FORMALNAME, TableType.Object, prevNode.Guid,prevNode, obj.SHORTNAME, obj.AOLEVEL)).ToList();

                        if (allQ.Count > 0 && allQ.Count < 500)
                        {
                            var missed = IterateAddressObject(variantObj, allQ, true);
                            result.AddRange(missed);
                        }
                    }
                }
                //TODO
                if (result.Count == 0 && !retrying && false)
                {
                    var concrete = IterateAddressObject(variantObj, null, true);
                    result.AddRange(concrete);
                    foreach (var prevNode in prevNodes)
                    {
                        if (prevNode.AOLevel < 4) result.Add(prevNode);
                    }
                }
            }
            else if (prevNodes == null)
            {
                var firstQuery = (from obj in dataContext.Object
                                  where obj.FORMALNAME == variantObj.Name && obj.ACTSTATUS == 1
                                  select new ObjectNode(obj.AOGUID, variantObj.Name, TableType.Object, obj.PARENTGUID, null, obj.SHORTNAME, obj.AOLEVEL)).ToList();
                result = firstQuery;
            }


            if (variantObj.Type != null)
            {
                var types = dictionary.GetAOTypes(variantObj.Type.AbbreviatedName);

                for (int i = 0; i < result.Count; i++)
                {
                    bool finded = false;
                    foreach (var type in types)
                    {
                        if (result[i].AOLevel == type.Level)
                        {
                            finded = true;
                            break;
                        }
                    }

                    if (!finded)
                    {
                        if (result.Count > 1)
                        {
                            result.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }
            else if (result.Count > 50)
            {
                //берём самое минимальное
                int minAO = int.MaxValue;
                foreach (var r in result)
                {
                    if (r.AOLevel < minAO) minAO = r.AOLevel;
                }
                var sorted = new List<ObjectNode>();
                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].AOLevel == minAO) sorted.Add(result[i]);
                }
                result = sorted;
            }

            return result;
        }

        public ObjectNode GetActualHierarchy(ObjectNode node)
        {
            ObjectNode res = null;
            if (node == null) return null;


        }


        private AOTypeDictionary GetDictionaryFromSql()
        {
            var dictionary = new AOTypeDictionary();

            foreach (var type in dataContext.AddressObjectType)
            {
                if (!string.IsNullOrEmpty(type.SCNAME) && !bannedAbbriviatedNames.Contains(type.SCNAME))
                    dictionary.Add(new AddressSplitterLib.AddressObjectType(type.SOCRNAME, type.SCNAME, type.LEVEL));
                if (!string.IsNullOrEmpty(type.SOCRNAME) && !bannedAbbriviatedNames.Contains(type.SOCRNAME))
                    dictionary.Add(new AddressSplitterLib.AddressObjectType(type.SOCRNAME, type.SOCRNAME, type.LEVEL));
            }
            return dictionary;
        }

        public string GetDistrictByRegion(string regionName)
        {
            var q = (from d in dataContext.District
                     select d.District1).FirstOrDefault();

            return q;
        }


        public void Close()
        {
            try
            {
                dataContext.Connection.Close();
            }
            catch
            {

            }
        }

    }


}
