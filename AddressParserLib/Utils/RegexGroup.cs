﻿using System.Collections.Generic;
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
        public Match GetMatch(string pattern, string input) => GetRegex(pattern).Match(input);

        /// <summary>
        /// Возвращает все вхождения по регулярному выражению.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public MatchCollection GetMatches(string pattern, string input) => GetRegex(pattern).Matches(input);


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
            foreach (Match outterMatch in GetMatches(outerPattern,input))
            {
                result.Add(new InnerMatch()
                {
                    inner = GetMatch(innerPattern,outterMatch.Value).Value,
                    outer = outterMatch.Value
                }
                );
            }
            return result;
        }


        /// <summary>
        /// Проверяет является ли строка регулярным выражением.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsMatch(string pattern, string input) => GetRegex(pattern).IsMatch(input);

        /// <summary>
        /// Заменяет все вхождения по регулярному выражению.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="input"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public string Replace(string pattern, string input, string replacement) => GetRegex(pattern).Replace(input, replacement);


        private Regex GetRegex(string pattern)
        {
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
        public string inner;
        public string outer;
    }
}
