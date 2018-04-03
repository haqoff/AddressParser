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

            dictionary.Add("кв",9);
            dictionary.Add("д", 8);
            dictionary.Add("пос.", 4);
            dictionary.Add("ул", 7);
            dictionary.Add("респ", 1);
            dictionary.Add("обл", 1,"облатсь", AddressObjectType.GenderNoun.Fiminine);
            dictionary.Add("г", 4);
            dictionary.Add("шос",7);
            dictionary.Add("км", 7);
            dictionary.Add("дом",8);
            dictionary.Add("пр", 7);
            dictionary.Add("ш", 7);
            dictionary.Add("р-н", 2);
            dictionary.Add("п.", 4);


            var parser = new AddressParser(dictionary);

           parser.Parse(@"191119, САНКТ-ПЕТЕРБУРГ Г, САНКТ-ПЕТЕРБУРГ, КОНСТАНТИНА ЗАСЛОНОВА УЛ, ДОМ № 26, КОРПУС А");
         //   Console.Write("лесная    ".Trim(' ').Trim('.'));

         //   Console.Write("@");
            Console.ReadKey();       
        }
    }
}
