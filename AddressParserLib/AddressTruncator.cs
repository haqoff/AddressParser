using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AddressParserLib
{
    public class AddressTruncator
    {
        private static AddressTruncator _instance;

        private Regex houseRegex;
        private Regex postalRegex;


        public AddressTruncator()
        {
            houseRegex = new Regex(@"([0-9]+)|((д\.| дом).*[0-9]+.*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            postalRegex = new Regex(@"[0-9]{6}", RegexOptions.Compiled);
        }

        public static AddressTruncator GetInstance()
        {
            if (_instance != null)
                return _instance;
            return _instance = new AddressTruncator();
        }

        public string TruncPostalCode(string source)
        {
            if (postalRegex.IsMatch(source))
                return postalRegex.Match(source).Value;
            return null;

        }

        public string TruncHouse(string source, float searchRatio = 2 / 3f)
        {
            MatchCollection matchCollection = houseRegex.Matches(source);

            if (matchCollection.Count == 0)
            {
                return null;
            }
            else if (matchCollection.Count == 1)
            {
                if (matchCollection[0].Index > source.Length * searchRatio)
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
                    if ((index < minIndex) && (index > source.Length * searchRatio))
                        minIndex = index;
                }

                return source.Substring(minIndex);
            }
        }


    }
}
