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
            var types = new List<AddressObjectType>();
            types.Add(new AddressObjectType("", "д",AddressObjectType.GenderNoun.Masculine, 8));
            types.Add(new AddressObjectType("", "кв", AddressObjectType.GenderNoun.Neuter, 9));

            var parser = new AddressParser(types);

            parser.Parse(@"Самарская обл., Самара, 5 sыбенко ул.,36     ЛИТЕР А ASD");

            parser.Parse(@"140073, МО, пос. Томилино,23/7 км Ново-Рязанского шоссе,17 лит.А кв2");

            parser.Parse("428023, ЧУВАШСКАЯ РЕСПУБЛИКА- ЧУВАШИЯ, ЧЕБОКСАРЫ Г, КОМПОЗИТОРА МАКСИМОВА УЛ, ДОМ № 13");

            Console.ReadKey();       
        }
    }
}
