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
        public static void Parse(string source, ParserOptions options)
        {
            foreach (var item in source.Split(options.Splitters))
            {
                if (item == "")
                    continue;

                item.IndexOf()
            }
        }

    }

    public class ParserOptions
    {
        public string[] AddressTypePatterns { get; set; }
        public bool CaseSensetive { get; set; }
        public char[] Splitters { get; set; }
    }
}
