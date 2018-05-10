using FiasParserLib;
using System;
using System.Collections.Generic;

namespace TestApp
{
    public class Program
    {
        static ObjectConverter _oc;

        static void Main(string[] args)
        {
            ObjectConverter oc = new ObjectConverter(new FiasClassesDataContext("Data Source=SCORPION;Initial Catalog=fias;User ID=client;Password=root;"));
            _oc = oc;

            var objs = oc.GetPreviousObjects("4b971768-15bf-4712-b6ce-8027c11f0994", IdType.House);
            Print(objs);
            objs = oc.GetPreviousObjects("389b7055-4599-46b6-ab81-63e5de0a0d5a", IdType.House);
            Print(objs);
            objs = oc.GetPreviousObjects("102d5085-8f59-4067-8a95-52ad112a3835", IdType.House);
            Print(objs);
           

            Console.ReadKey();
        }

        public static void Print(List<ParsedId> parsedIds)
        {
            Console.WriteLine();
            Console.WriteLine("ОКРУГ: " + _oc.GetDistrict(parsedIds));
            var region = _oc.GetRegion(parsedIds);
            Console.WriteLine("оБЛАСТЬ: {0} {1}", region.shortName, region.name);

            var city = _oc.GetCity(parsedIds);
            Console.WriteLine("Город: {0} {1}", city.shortName, city.name);

            var street = _oc.GetStreet(parsedIds);
            Console.WriteLine("Улица: {0} {1}", street.shortName, street.name);

            var house = _oc.GetHouse(parsedIds);
            Console.WriteLine("Дом: {0}", house.name);

            Console.WriteLine("---");
            foreach (var item in parsedIds)
            {
                Console.WriteLine(item.shortName + " " + item.name);
            }
        }
    }
}
