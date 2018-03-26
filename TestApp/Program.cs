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
            AddressParser parser = new AddressParser();

   
            string source = @"Самарская обл., Самара, Дыбенко ул., 36А";

            Console.WriteLine("Исходный: " + source);
            // Console.WriteLine("Дом: "+truncator.TruncHouse(source));
            // Console.WriteLine("PostalCode: " + truncator.TruncPostalCode(source));
            parser.Parse(source);

            Console.WriteLine("Область: " + truncator.TruncRegion(source));

            Console.ReadKey();
           
        }
    }
}
