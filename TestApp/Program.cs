using AddressParserLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AddressTruncator truncator = AddressTruncator.GetInstance();

            string source = @"361325, КАБАРДИНО - БАЛКАРСКАЯ РЕСП, УРВАНСКИЙ Р - Н, СТАРЫЙ ЧЕРЕК С, ПОЧТОВАЯ УЛ, ДОМ № 10";

            Console.WriteLine("Исходный: " + source);
            Console.WriteLine("Дом: "+truncator.TruncHouse(source));
            Console.WriteLine("PostalCode: " + truncator.TruncPostalCode(source));

            Console.ReadKey();
           
        }
    }
}
