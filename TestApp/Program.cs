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


            var parser = new AddressParser(dictionary);

        //    parser.Parse(@"432071, Ульяновск, ул. Урицкого, дом №100-Д");

         //   parser.Parse(@"432063, УЛЬЯНОВСКАЯ ОБЛ, УЛЬЯНОВСК Г, МИНАЕВА УЛ, ДОМ № 15");

          //  parser.Parse("г. Санкт-Петербург, Лиговский пр., д. 174, лит.А");

         //   parser.Parse("105187, г. Москва, Москва, Измайловское ш., 71");

            parser.Parse("Нижегородская обл Кулебаки Фластвоа 2");


            Console.ReadKey();       
        }
    }
}
