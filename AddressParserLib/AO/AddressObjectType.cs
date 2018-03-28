﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressParserLib
{
    /// <summary>
    /// Тип адресного обьекта.
    /// </summary>
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

        public AddressObjectType(string fullName, string abbreviatedName, int level, GenderNoun genderType)
        {
            FullName = fullName;
            AbbreviatedName = abbreviatedName;
            GenderType = GenderType;
            Level = level;
        }

        /// <summary>
        /// Род названия обьекта.
        /// </summary>
        public enum GenderNoun
        {
            Uknown,     //Неизвестно
            Fiminine,   //Женский
            Masculine,  //Мужской
            Neuter      //Средний
        }
    }
}
