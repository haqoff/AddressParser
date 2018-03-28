using AddressParserLib;
using AddressParserLib.AO;
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
            var dictionary = new AOTypeDictionary();

            dictionary.Add("кв",9);
            dictionary.Add("д", 8);
            dictionary.Add("пос.", 4);
            dictionary.Add("ул", 7);
            dictionary.Add("респ", 1);
            dictionary.Add("обл", 1);
            dictionary.Add("г", 4);
            dictionary.Add("шос",7);
            dictionary.Add("д", 7);


            var parser = new AddressParser(dictionary);

            parser.Parse(@"Самарская обл., Самара, 5 sыбенко ул.,36     ЛИТЕР А ASD");

            parser.Parse(@"140073, МО, Томилино,23/7 км Ново-Рязанского шоссе,17 лит. А кв2");

            parser.Parse("428023, ЧУВАШСКАЯ РЕСПУБЛИКА- ЧУВАШИЯ, ЧЕБОКСАРЫ Г, КОМПОЗИТОРА МАКСИМОВА УЛ, ДОМ № 13 123213123" );

            parser.Parse("620017, ВЛАДИМИРСКАЯ ОБЛАСТЬ МУРОМ УЛИЦА ФИЛАТОВА 7");


            Console.ReadKey();       
        }
    }
}
