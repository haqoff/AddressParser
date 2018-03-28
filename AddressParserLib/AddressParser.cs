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


        public AddressParser(List<AddressObjectType> objectTypes)
        {
            truncator = new AddressTruncator(objectTypes);
            sb = new StringBuilder();
        }
        public void Parse(string source)
        {
            Console.WriteLine("Исходная: {0}", source);
            var preparsedObjects = new List<AddressObject>();

            string postalCode = truncator.TruncPostalCode(source);

            var (buildingNum, roomNum, clearedString) = truncator.TruncBuildingAndRoomNum(source);
            source = clearedString;

            if (buildingNum != null)
                preparsedObjects.Add(buildingNum);
            if (roomNum != null)
                preparsedObjects.Add(roomNum);

            foreach (var item in truncator.Split(source))
            {
                Console.WriteLine("------");
                Console.WriteLine("Вариант:");

                item.AObjects.AddRange(preparsedObjects);
                foreach (var obj in item.AObjects)
                {
                    Console.Write(obj.Name + "-->");
                }
                Console.WriteLine();
                Console.WriteLine("----");
            }
        }
        

     

    }



}

