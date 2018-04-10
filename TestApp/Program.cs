using FiasParserLib;
using System;

namespace TestApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var parser = new FiasParser();

             var id = parser.Parse(@"117133, г. Москва, Москва, Генерала Тюленева ул., 2");
             Print(id);
            
  

            parser.Close();

            Console.ReadKey();
        }

        public static void Print(string id)
        {
            Console.WriteLine();
            Console.WriteLine("---Правильный вариант в БД---");
            Console.WriteLine("Тип ID:  :: GUID: {1}",  id);
            Console.WriteLine("-----------------------------");
        }
    }
}
