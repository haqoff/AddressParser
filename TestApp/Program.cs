using AddressParserLib;
using AddressParserLib.AO;
using System;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dictionary = new AOTypeDictionary();

            dictionary.Add("к", 9);
            dictionary.Add("ш", 7);
            dictionary.Add("п", 4);
            dictionary.Add("пр-кт", 7);
            dictionary.Add("пр-т", 7);
            dictionary.Add("кв",9);
            dictionary.Add("д", 8);
            dictionary.Add("пос", 4);
            dictionary.Add("ул", 7);
            dictionary.Add("респ", 1);
            dictionary.Add("обл", 1,"облатсь", AddressObjectType.GenderNoun.Fiminine);
            dictionary.Add("г", 4);
            dictionary.Add("шос",7);
            dictionary.Add("км", 7);
            dictionary.Add("дом",8);
            dictionary.Add("пр", 7);   
            dictionary.Add("р-н", 2);
            dictionary.Add("б-р", 4);
            dictionary.Add("вал", 91);
            dictionary.Add("с.п.", 4);
            dictionary.Add("АО", 1);
            dictionary.Add("г.г.", 5);
            dictionary.Add("область", 1,"область",AddressObjectType.GenderNoun.Fiminine);
            dictionary.Add("край", 1);

            dictionary.Sort();




            var parser = new AddressParser(dictionary);

            parser.Parse(@"Обнинск г. Маркса пр-кт 45, пом.1");

            Console.ReadKey();       
        }
    }
}
