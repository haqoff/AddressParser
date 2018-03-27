using System.Collections.Generic;

namespace AddressParserLib
{
    public class FullAddress
    {
        /// <summary>
        /// Индекс адреса.
        /// </summary>
        public string PostalCode { get; private set; }

        private List<AddressObject> objects;

        public FullAddress(List<AddressObject> objects, string postalCode)
        {
            PostalCode = postalCode;
            this.objects = objects;
        }
    }
}
