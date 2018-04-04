using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AddressParserLib.Utils
{
    public class RegexGroup
    {
        private Dictionary<string, Regex> cachedRegexes;
        private RegexOptions options;

        public RegexGroup(RegexOptions options)
        {
            cachedRegexes = new Dictionary<string, Regex>();
            this.options = options;
        }


        /// <summary>
        /// Возвращает первое вхождение по регулярному выражению.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public Match GetMatch(string pattern, string input) => GetRegex(pattern)?.Match(input);

        /// <summary>
        /// Возвращает все вхождения по регулярному выражению.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public MatchCollection GetMatches(string pattern, string input) => GetRegex(pattern)?.Matches(input);


        /// <summary>
        /// Возвращает отсортированный список Match
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="input"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public List<Match> GetListSortedMatches(string pattern, string input, IComparer<Match> comparer)
        {
            var res = new List<Match>();
            MatchCollection col = GetMatches(pattern, input);

            foreach (Match _match in col)
            {
                res.Add(_match);
            }
            res.Sort(comparer);

            return res;
        }

        /// <summary>
        /// Возвращает список вхождения по внешнему паттерну и вхождения паттерна внутри внешнего.
        /// </summary>
        /// <param name="outerPattern"></param>
        /// <param name="innerPattern"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<InnerMatch> GetInnerMatch(string outerPattern, string innerPattern, string input)
        {
            var result = new List<InnerMatch>();
            var matches = GetMatches(outerPattern, input);
            if (matches == null)
                return null;

            foreach (Match outterMatch in matches)
            {
                result.Add(new InnerMatch()
                {
                    inner = GetMatch(innerPattern, outterMatch.Value),
                    outer = outterMatch
                }
                );
            }
            if (result.Count > 1)
                result.Sort(new InnerLengthComparer());
            return result;
        }


        /// <summary>
        /// Проверяет является ли строка регулярным выражением.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsMatch(string pattern, string input) => GetRegex(pattern)?.IsMatch(input) ?? false;

        /// <summary>
        /// Заменяет все вхождения по регулярному выражению.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="input"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public string Replace(string pattern, string input, string replacement) => GetRegex(pattern)?.Replace(input, replacement);


        private Regex GetRegex(string pattern)
        {
            if (pattern == null)
                return null;
            if (!cachedRegexes.TryGetValue(pattern, out Regex r))
            {
                r = new Regex(pattern, options);
                cachedRegexes.Add(pattern, r);
            }
            return r;
        }
    }
    public struct InnerMatch
    {
        public Match inner;
        public Match outer;
    }

    class InnerLengthComparer : IComparer<InnerMatch>
    {
        public int Compare(InnerMatch x, InnerMatch y)
        {
            if (x.inner.Length > y.inner.Length)
                return -1;
            else if (x.inner.Length < y.inner.Length)
                return 1;
            return 0;
        }
    }

    public class LengthComparer : IComparer<Match>
    {
        public int Compare(Match x, Match y)
        {
            if (x.Length > y.Length)
                return 1;
            else if (x.Length < y.Length)
                return -1;
            return 0;
        }
    }
}
