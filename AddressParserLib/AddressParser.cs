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
        private string[] AOTypePatterns;

        public AddressParser(string[] AOTypePatterns)
        {
            this.AOTypePatterns = AOTypePatterns;
            truncator = AddressTruncator.GetInstance();
        }
        public void Parse(string source)
        {
     
        }

        private List<AddressObject> PreparseAddress(string source)
        {
            var preparsedObjects = new List<AddressObject>();

            var postalCode = truncator.TruncPostalCode(source);
            if (postalCode != null)
            {
                source = source.Replace(postalCode.Name, "");
                preparsedObjects.Add(postalCode);
;           }

            var houseAndRoom = truncator.TruncHouseAndRoom(source);
            if (houseAndRoom.house != null)
            {
                source = source.Replace(houseAndRoom.subString, "");
                preparsedObjects.Add();
            }

            string region = truncator.TruncRegion(source);
            if (region != null)
            {
                preparsedObjects.Add(new AddressObject(region, ObjectType.Other));
            }

            return preparsedObjects;
        }

        private List<AddressVersion> GetAddressVersions(List<AddressObject> preparsedObjects)
        {

        }

    }
}
