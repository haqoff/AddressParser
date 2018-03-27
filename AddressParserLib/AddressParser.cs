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
        private List<AddressObjectType> objectTypes;

        public AddressParser(List<AddressObjectType> objectTypes)
        {
            truncator = AddressTruncator.GetInstance();
        }
        public void Parse(string source)
        {
     
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
