using AddressParserLib.AO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
            var obviousObjects = ParseObvious(ref source).obviousObjects;


            

            foreach (var item in truncator.Split(source))
            {
                Console.WriteLine("------");
                Console.WriteLine("Вариант:");

                item.AObjects.AddRange(obviousObjects);
                foreach (var obj in item.AObjects)
                {
                    Console.Write(obj.Name + "-->");
                }
                Console.WriteLine();
                Console.WriteLine("----");
            }
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

            return (preparsedObjects, postalCode);
        }

    }
}

