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
            HouseFinder hFinder = HouseFinder.GetInstance();
            Console.WriteLine(hFinder.Parse("361325, КАБАРДИНО - БАЛКАРСКАЯ РЕСП, УРВАНСКИЙ Р - Н, СТАРЫЙ ЧЕРЕК С, ПОЧТОВАЯ УЛ, ДОМ № 10"));


            Console.ReadKey();
           
        }
    }
}
