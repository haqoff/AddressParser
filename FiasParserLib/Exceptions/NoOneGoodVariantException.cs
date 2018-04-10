using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiasParserLib.Exceptions
{
    class NoOneGoodVariantException : Exception
    {
        public List<SearchVariant> variants{ get; private set; }

        public NoOneGoodVariantException(List<SearchVariant> variants,string message) : base(message)
        {
            this.variants = variants;
        }
    }
}
