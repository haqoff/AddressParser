using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressParserLib.AO
{
    internal enum ObjectLevel
    {
        Region = 1,
        District = 3,
        Locality = 4,
        Street = 7,
        House = 8,
        Room = 9
    }
}
