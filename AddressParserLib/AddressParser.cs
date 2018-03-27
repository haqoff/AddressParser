using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddressParserLib
{
    public sealed class AddressParser
    {
        private AddressTruncator truncator;

        public AddressParser(List<AddressObjectType> objectTypes)
        {
            truncator = new AddressTruncator(objectTypes);
        }
        public void Parse(string source)
        {
            Console.WriteLine("Исходный: " + source);
            
            Console.WriteLine("Индекс: {0}", truncator.TruncPostalCode(source));
            Console.WriteLine("Дом: {0}", truncator.TruncBuildingAndRoomNum(source).buildingNum?.Name);
            Console.WriteLine("Кв: {0}", truncator.TruncBuildingAndRoomNum(source).roomNum?.Name);
            Console.WriteLine("");

        }

        private List<AddressObject> PreparseAddress(string source)
        {
            return null;
        }

        private List<FullAddress> GetAddressVersions(List<AddressObject> preparsedObjects, string PostalCode = null)
        {
            return null;
        }

    }
}
