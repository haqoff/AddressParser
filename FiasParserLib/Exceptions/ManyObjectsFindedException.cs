using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiasParserLib.Exceptions
{
    class ManyObjectsFindedException : Exception
    {
        public SearchVariant Variant { get; private set; }

        public ManyObjectsFindedException(SearchVariant variant, string message) : base(message)
        {
            Variant = variant;
        }
    }
}
