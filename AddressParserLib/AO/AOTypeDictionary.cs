using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AddressSplitterLib.AddressObjectType;

namespace AddressSplitterLib.AO
{
    /// <summary>
    /// Словарь типов адресных обьектов.
    /// </summary>
    public class AOTypeDictionary
    {
        private Dictionary<TypeKey, AddressObjectType> objectTypes;
        private StringBuilder sb;

        public AOTypeDictionary()
        {
            objectTypes = new Dictionary<TypeKey, AddressObjectType>();

            sb = new StringBuilder();
        }

        /// <summary>
        /// Добавляет новый тип адресного обьекта, если такого не существует.
        /// </summary>
        public void Add(AddressObjectType objectType)
        {
            try
            {
                var key = new TypeKey()
                {
                    abbreviatedName = objectType.AbbreviatedName.ToLower(),
                    level = objectType.Level
                };

                objectType.AbbreviatedName = objectType.AbbreviatedName?.Replace(".", @"\.").Replace("/", @"\/");
                objectTypes.Add(key, objectType);
            }
            catch (Exception)
            {
                //Мы специально игнорируем, ибо попытка добавить новый AO с такой же абревиатурой не страшна, это + ещё и в документации написано. 
            }
        }

        /// <summary>
        /// Сортирует элементы словаря по убыванию длины строки абревиатуры.
        /// </summary>
        public void Sort()
        {
            objectTypes = objectTypes.OrderByDescending(pair => pair.Key.abbreviatedName.Length).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        /// Возвращает обьект типа адресного обьекта.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public AddressObjectType GetAOType(TypeKey key)
        {
            key.abbreviatedName = key.abbreviatedName.ToLower();
            return objectTypes[key];
        }

        /// <summary>
        /// Возвращает первое вхождение типа адресного обьекта по аббревиатуре.
        /// </summary>
        /// <param name="abbreviatedName"></param>
        /// <returns></returns>
        public AddressObjectType GetAOType(string abbreviatedName)
        {
            //TODO: сейчас ключ ищется линейно, желательно искать двоичным поиском.
            foreach (var obj in objectTypes)
            {
                if (obj.Key.abbreviatedName == abbreviatedName) return obj.Value;
            }
            return null;
        }

        public List<AddressObjectType> GetAOTypes(string abbreviatedName)
        {
            var types = new List<AddressObjectType>();

            foreach (var obj in objectTypes)
            {
                if (obj.Value.AbbreviatedName == abbreviatedName) types.Add(obj.Value);
            }
            return types;
        }

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
              //  Console.WriteLine(obj.Value.AbbreviatedName);
            }
            sb.Append(")");
            return sb.ToString();
        }

        public struct TypeKey
        {
            public string abbreviatedName;
            public int level;
        }

    }
}
