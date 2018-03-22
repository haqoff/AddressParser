using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AddressParserLib
{
    public class HouseFinder
    {
        private static HouseFinder hs;
        private Regex houseReg;
        private static readonly string HOUSE_PATTERN = @"([0-9]+)|((д\.|дом).*[0-9]+.*)";
        private static readonly float FIND_RATIO = 2 / 3f;

        public HouseFinder()
        {
            houseReg = new Regex(HOUSE_PATTERN, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }


        public static HouseFinder GetInstance()
        {
            if (hs != null)
                return hs;
            return hs = new HouseFinder();
        }

        public string Parse(string source)
        {
            MatchCollection matchCollection = houseReg.Matches(source);

            if (matchCollection.Count == 0)
            {
                return null;
            }
            else if (matchCollection.Count == 1)
            {
                if (matchCollection[0].Index > source.Length * 3 / 4)
                    return source.Substring(matchCollection[0].Index);
                return matchCollection[0].Value;
            }
            else
            {
                List<Int32> indexes = new List<Int32>();
                foreach (Match match in matchCollection)
                {
                    indexes.Add(match.Index);
                }
                indexes.Sort();

                int minIndex = indexes[indexes.Count - 1];

                foreach (var index in indexes)
                {
                    if ((index < minIndex) && (index > source.Length * FIND_RATIO))
                        minIndex = index;
                }

                return source.Substring(minIndex);
            }
        }
    }
}
