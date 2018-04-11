using FiasParserLib;
using System;

namespace TestApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var parser = new FiasParser();

            //Пензенская обл., г.Заречный, Мира пр-кт66   А
            ParseResult res = parser.Parse(@"Пензенская обл., г.Нижний Ломов, Московская ул.10Б");
            Print(res);
     
            parser.Close();

            Console.ReadKey();
        }

        public static void Print(ParseResult res)
        {
            Console.WriteLine();
            Console.WriteLine("---Правильный вариант в БД---");
            Console.WriteLine("Тип ID: {0} :: GUID: {1}",  res.type, res.id);
            Console.WriteLine("Фулл адрес: {0}", res.address);
            Console.WriteLine("-----------------------------");
        }
    }
}
