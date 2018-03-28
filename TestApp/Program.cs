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
            types.Add(new AddressObjectType("", "д", 8));
            types.Add(new AddressObjectType("", "кв", 9));
            types.Add(new AddressObjectType("", "пос.", 9));
            types.Add(new AddressObjectType("", "ул", 9));
            types.Add(new AddressObjectType("", "респ", 9));
            types.Add(new AddressObjectType("", "обл", 1));
            types.Add(new AddressObjectType("", "г", 1));

            var parser = new AddressParser(types);

            //  parser.Parse(@"Самарская обл., Самара, 5 sыбенко ул.,36     ЛИТЕР А ASD");

            //  parser.Parse(@"140073, МО, пос. Томилино,23/7 км Ново-Рязанского шоссе,17 лит.А кв2");

            //  parser.Parse("428023, ЧУВАШСКАЯ РЕСПУБЛИКА- ЧУВАШИЯ, ЧЕБОКСАРЫ Г, КОМПОЗИТОРА МАКСИМОВА УЛ, ДОМ № 13 123213123" );

            parser.Parse("620017, СВЕРДЛОВСКАЯ ОБЛ, ЕКАТЕРИНБУРГ Г, ЭЛЕКТРИКОВ УЛ, ДОМ № 23");

            Console.ReadKey();       
        }
    }
}
