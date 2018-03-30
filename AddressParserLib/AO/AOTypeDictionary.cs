using System;
using System.Collections.Generic;
using System.Text;
using static AddressParserLib.AddressObjectType;

namespace AddressParserLib.AO
{
    /// <summary>
    /// Словарь типов адресных обьектов.
    /// </summary>
    public class AOTypeDictionary
    {
        private Dictionary<string, AddressObjectType> objectTypes;
        private StringBuilder sb;

        public AOTypeDictionary()
        {
            objectTypes = new Dictionary<string, AddressObjectType>();
            sb = new StringBuilder();
        }

        /// <summary>
        /// Добавляет новый тип адресного обьекта, если такого не существует.
        /// </summary>
        public void Add(string abbreviatedName, int level, string fullName = null, GenderNoun gender = GenderNoun.Uknown)
        {
            try
            {
                objectTypes.Add(abbreviatedName.ToLower(), new AddressObjectType(fullName, abbreviatedName, level, gender));
            }
            catch (Exception)
            {

            }
        }

        public AddressObjectType GetAOType(string abbreviatedName) => objectTypes[abbreviatedName.ToLower()];


        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns>Возвращает регулярное выражение всех аббревиатур типов обьекта по уровню.</returns>
        internal string GetRegexMultiPattern(int level)
        {
            sb.Clear();
            sb.Append("(");
            foreach (var obj in objectTypes)
            {
                if (obj.Value.Level == level)
                {
                    if (sb.ToString() != "(")
                        sb.Append("|");
                    sb.Append(obj.Value.AbbreviatedName);
                }
            }
            sb.Append(")");
            return sb.ToString();
        }

        /// <summary>
        /// Возвращает регулярное выражение всех аббревиатур типов обьекта по роду существительного.
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        internal string GetRegexMultiPattern(GenderNoun gender)
        {
            sb.Clear();
            sb.Append("(");
            foreach (var obj in objectTypes)
            {
                if (obj.Value.GenderType == gender)
                {
                    if (sb.ToString() != "(")
                        sb.Append("|");
                    sb.Append(obj.Value.AbbreviatedName);
                }
            }
            sb.Append(")");
            return sb.ToString();
        }

        /// <summary>
        /// Возвращает регулярное выражение всех аббревиатур типов обьекта по роду и уровню обьекта.
        /// </summary>
        /// <returns></returns>
        internal string GetRegexMultiPattern(int level, GenderNoun gender)
        {
            sb.Clear();
            sb.Append("(");
            foreach (var obj in objectTypes)
            {
                if (obj.Value.Level == level && obj.Value.GenderType == gender)
                {
                    if (sb.ToString() != "(")
                        sb.Append("|");
                    sb.Append(obj.Value.AbbreviatedName);
                }
            }
            sb.Append(")");
            return sb.ToString();
        }

        /// <summary>
        /// Возвращает регулярное выражение всех аббревиатур типов обьекта.
        /// </summary>
        /// <returns></returns>
        internal string GetRegexMultiPattern()
        {
            sb.Clear();
            sb.Append("(");
            foreach (var obj in objectTypes)
            {
                if (sb.ToString() != "(")
                    sb.Append("|");
                sb.Append(obj.Value.AbbreviatedName);
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}
