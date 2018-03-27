using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressParserLib
{
    public class AddressObjectType
    {
        /// <summary>
        /// Полное имя типа обьекта.
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// Сокращённое имя типа обьекта.
        /// </summary>
        public string AbbreviatedName { get; private set; }

        /// <summary>
        /// Уровень обьекта
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// Род существительного обьекта.
        /// </summary>
        public GenderNoun GenderType { get; private set; }

        public AddressObjectType(string fullName, string abbreviatedName, GenderNoun genderType, int level)
        {
            FullName = fullName;
            AbbreviatedName = abbreviatedName;
            GenderType = GenderType;
            Level = level;
        }

        public enum GenderNoun
        {
            Fiminine,   //Женский
            Masculine,  //Мужской
            Neuter      //Средний
        }
    }
}
