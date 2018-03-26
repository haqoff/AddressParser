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
        private Regex regionRegex;


        private AddressTruncator()
        {
            houseRegex = new Regex(@"([0-9]+)|((д\.| дом).*[0-9]+.*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            postalRegex = new Regex(@"[0-9]{6}", RegexOptions.Compiled);
            regionRegex = new Regex(@"(?<=\W|^).+?ая(?=((\W)+о))", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
        }

        public static AddressTruncator GetInstance()
        {
            if (_instance != null)
                return _instance;
            return _instance = new AddressTruncator();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">Исходная строка</param>
        /// <returns>Возвращает AddressObject региона, если найден.</returns>
        public AddressObject TruncRegion(string source) => new AddressObject(regionRegex.Match(source).Value);

        internal AddressObject TruncPostalCode(string source) => new AddressObject(postalRegex.Match(source).Value, ObjectType.PostalCode);

        internal (AddressObject house, AddressObject room, string subString) TruncHouseAndRoom(string source)
        {
            string sub = GetHouseSubstring(source);
            return (new AddressObject(sub, ObjectType.House), null, sub);
        }

        private string GetHouseSubstring(string source, float searchRatio = 2 / 3f)
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
