using System;
using System.Collections.Generic;
using System.Linq;
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
                objectTypes.Add(abbreviatedName.ToLower(), new AddressObjectType(fullName, abbreviatedName.Replace(".",@"\."), level, gender));
            }
            catch (Exception e)
            {
                //Мы специально игнорируем, ибо попытка добавить новый AO с такой же абревиатурой не страшна, это + ещё и в документации написано. 
            }
        }

        /// <summary>
        /// Сортирует элементы словаря по убыванию длины строки абревиатуры.
        /// </summary>
        public void Sort()
        {
            objectTypes = objectTypes.OrderByDescending(pair => pair.Key.Length).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public AddressObjectType GetAOType(string abbreviatedName) => objectTypes[abbreviatedName.ToLower()];


        /// <summary>
        /// Возвращает регулярное выражение всех аббревиатур типов обьекта по уровню
        /// </summary>
        /// <param name="level"></param>
        /// <returns>.</returns>
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
                Console.WriteLine(obj.Value.AbbreviatedName);
            }
            sb.Append(")");
            return sb.ToString();
        }

    }
}
