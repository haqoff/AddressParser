using System.Collections.Generic;
using System.Linq;
using System;

namespace FiasParserLib
{
    public class ObjectConverter
    {
        private FiasClassesDataContext context;

        public ObjectConverter(FiasClassesDataContext context)
        {
            this.context = context;
            context.CommandTimeout = 0;
        }

        public ParsedId GetHouse(List<ParsedId> parsedIds)
        {
            foreach (var item in parsedIds)
            {
                if (item.type == IdType.House) return item;
            }
            return null;
        }


        public ParsedId GetStreet(List<ParsedId> parsedIds)
        {
            foreach (var item in parsedIds)
            {
                if (item.level == 7 || item.level == 65 || item.level == 75)
                {
                    return item;
                }
            }
            return null;
        }

        public ParsedId GetCity(List<ParsedId> parsedIds)
        {
            foreach (var item in parsedIds)
            {
                if (item.level == 35 || item.level == 4 || item.level == 5 || item.level == 6)
                    return item;
            }
            return null;
        }

        public ParsedId GetRegion(List<ParsedId> parsedIds)
        {
            foreach (var item in parsedIds)
            {
                if (item.level == 1 || item.level == 2) return item;
            }
            return null;
        }

        public string GetDistrict(List<ParsedId> parsedIds)
        {
            string q = (from t in context.District
                        where t.Region == GetRegion(parsedIds).name
                        select t.District1).FirstOrDefault();

            return q;
        }




        public List<ParsedId> GetPreviousObjects(string id, IdType type)
        {
            var res = new List<ParsedId>();

            ParsedId lastParsed = GetInfo(id, type);
            res.Add(lastParsed);

            int countIterations = 0;
            while (!string.IsNullOrEmpty(lastParsed.prevId) && countIterations < 10)
            {
                IdType searchType = IdType.Object;

                if (lastParsed.type == IdType.Room) searchType = IdType.House;
                else if (lastParsed.type == IdType.House) searchType = IdType.Object;

                lastParsed = GetInfo(lastParsed.prevId, searchType);
                res.Add(lastParsed);
                countIterations++;
            }

            return res;
        }

        public ParsedId GetInfo(string guId, IdType type)
        {
            ParsedId res = null;

            switch (type)
            {
                case (IdType.Room):
                    //ищем комнату
                    res = (from h in context.Room
                           where h.ROOMGUID == guId
                           select new ParsedId()
                           {
                               id = h.ROOMGUID,
                               level = -1,
                               name = h.ROOMNUMBER,
                               prevId = h.HOUSEGUID,
                               type = IdType.Room
                           }).ToList().First();
                    break;
                case (IdType.House):
                    //ищем дом
                    res = (from h in context.House
                           where h.HOUSEGUID == guId
                           select new ParsedId()
                           {
                               id = guId,
                               level = -1,
                               name = h.HOUSENUM,
                               prevId = h.AOGUID,
                               type = IdType.House
                           }
                              ).ToList().First();
                    break;
                case (IdType.Object):
                    res = (from o in context.Object
                           where o.AOGUID == guId && o.ACTSTATUS == 1
                           select new ParsedId()
                           {
                               id = guId,
                               level = o.AOLEVEL,
                               name = o.FORMALNAME,
                               shortName = o.SHORTNAME,
                               prevId = o.PARENTGUID,
                               type = IdType.Object
                           }).ToList().First();
                    break;
            }
            return res;
        }

    }

    public class ParsedId
    {
        public string id;
        public string prevId;
        public string name = "";
        public string shortName = "";
        public int level;
        public IdType type;
    }
}
