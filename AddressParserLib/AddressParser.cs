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

            foreach (var item in truncator.Split(source))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();           
                Console.WriteLine("Вариант:");
                Console.ResetColor();

                item.AObjects.AddRange(obviousObjects);
                foreach (var obj in item.AObjects)
                {
                    Console.Write(obj.Name + "-->");
                }
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

            var (buildingNum, roomNum) = truncator.TruncBuildingAndRoomNum(ref source);

            if (buildingNum != null)
                preparsedObjects.Add(buildingNum);
            if (roomNum != null)
                preparsedObjects.Add(roomNum);

            preparsedObjects.AddRange(truncator.TruncByCorrectPos(ref source));

            return (preparsedObjects, postalCode);
        }

    }
}

