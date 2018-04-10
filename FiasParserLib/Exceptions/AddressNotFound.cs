using AddressSplitterLib;
using System;
using System.Collections.Generic;

namespace FiasParserLib.Exceptions
{
    public class AddressNotFound : Exception
    {

        public AddressNotFound(string message) : base(message)
        {
        }
    }
}
