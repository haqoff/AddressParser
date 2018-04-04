using AddressParserLib.AO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressParserLib
{
    public sealed class AddressParser
    {
        private AddressTruncator truncator;
        private StringBuilder sb;


        public AddressParser(AOTypeDictionary dictionary)
        {
            truncator = new AddressTruncator(dictionary);
            sb = new StringBuilder();
        }

        /// <summary>
        /// Возвращает все возможные варианты адресов из строки.
        /// </summary>
        /// <param name="source"></param>
        public List<FullAddress> Parse(string source)
        {
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("-----------------------");
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Исходная: {0}", source);
            Console.ResetColor();

            var obviousObjects = ParseObvious(ref source).obviousObjects;

            var buildingAndRoomVariants = truncator.TruncBuildingAndRoomNum(ref source);

            var splitted = truncator.Split(source);

            foreach (var item in Variant.Combine(splitted, buildingAndRoomVariants))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();           
                Console.WriteLine("Вариант:");
                Console.ResetColor();

                item.AddRange(obviousObjects);

                foreach (var obj in item)
                {
                    Console.Write(obj.Name + "-->");
                }
                Console.Write("Вероятность правильности: {0}",item.GetProbability());
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("-----------------------");
            Console.ResetColor();

            return null;
        }


        /// <summary>
        /// Очищает и возвращает только однозначные и очевидные обьекты, которые точно являются правильными, из строки.
        /// </summary>
        /// <param name="source"></param>
        public (List<AddressObject> obviousObjects, string postalCode) ParseObvious(ref string source)
        {
            var preparsedObjects = new List<AddressObject>();

            string postalCode = truncator.TruncPostalCode(ref source);

            preparsedObjects.AddRange(truncator.TruncByCorrectPos(ref source));

            return (preparsedObjects, postalCode);
        }

    }
}

