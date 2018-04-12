using AddressSplitterLib;
using AddressSplitterLib.AO;
using FiasParserLib;
using System;

namespace TestApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            AOTypeDictionary dictionary = new AOTypeDictionary();
            dictionary.Add(new AddressSplitterLib.AddressObjectType("город", "г", 4));
            dictionary.Add(new AddressSplitterLib.AddressObjectType("ул", "ул", 7));
            dictionary.Add(new AddressSplitterLib.AddressObjectType("дом", "д", 8));
            dictionary.Add(new AddressSplitterLib.AddressObjectType("к", "ком", 9));

            var parser = new AddressParser(dictionary);

            //г. Москва, г.Москва, Варшавское ш 9 стр.1Б


            // Print(parser.Parse(@"г.Санкт-Петербург, пр-т Авиаконструкторов, д.7А, лит.А, часть пом.1-Н (ч.п.1, ч.п.3, ч.п.4, ч.п.5, ч.п.6, ч.п.7, ч.п.10, ч.п.11, ч.п.12), часть коридора (ч.п.2), часть кабельной (ч.п.8)"));
            // Print(parser.Parse(@"Ростовская область РОСТОВ-НА-ДОНУ 20 ЛИНИЯ 4   3 МАЙСКАЯ 1-Я 3"));
            parser.Parse(@"Г. МОСКВА ЛИПЕЦКАЯ УЛ. 16/14 к.1");
           // parser.Close();

            Console.ReadKey();
        }

        public static void Print(ParseResult res)
        {
            Console.WriteLine();
            Console.WriteLine("---Правильный вариант в БД---");
            Console.WriteLine("Тип " +
                "ID: {0} :: GUID: {1}",  res.type, res.id);
            Console.WriteLine("Фулл адрес: {0}", res.address);
            Console.WriteLine("-----------------------------");
        }
    }
}
