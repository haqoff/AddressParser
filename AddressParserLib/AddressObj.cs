using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressParserLib
{
    class AddressObj
    {
        public string PostalCode { get; set; }
        public string HouseNum { get; set; }
        public string FlatNumber { get; set; }
        private List<string> objects;

    }
}
