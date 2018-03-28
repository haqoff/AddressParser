using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AddressParserLib.Utils
{
    public class RegexGroup
    {
        private List<Regex> cachedRegexes;
        private RegexOptions options;

        public RegexGroup(RegexOptions options)
        {
            cachedRegexes = new List<Regex>();
            this.options = options;
        }


        /// <returns>Возвращает первое вхождение по регулярному выражению.</returns>
        public Match GetMatch(string pattern, string input) => GetRegex(pattern).Match(input);

        /// <returns>Возвращает все вхождения по регулярному выражению</returns>
        public MatchCollection GetMatches(string pattern, string input) => GetRegex(pattern).Matches(input);


        public bool IsMatch(string pattern, string input) => GetRegex(pattern).IsMatch(input);

        /// <returns>Заменяет все вхождения по регулярному выражению</returns>
        public string Replace(string pattern, string input, string replacement) => GetRegex(pattern).Replace(input, replacement);


        private Regex GetRegex(string pattern)
        {
            foreach (var reg in cachedRegexes)
            {
                if (reg.ToString() == pattern)
                {
                    return reg;
                }
            }
            Regex regex = new Regex(pattern, options);
            cachedRegexes.Add(regex);
            return regex;
        }
    }
}
