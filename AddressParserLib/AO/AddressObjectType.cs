namespace AddressSplitterLib
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

        public AddressObjectType(string fullName, string abbreviatedName, int level, GenderNoun genderType = GenderNoun.Uknown)
        {
            FullName = fullName;
            AbbreviatedName = abbreviatedName?.Replace(".", @"\.").Replace("/", @"\/");
            GenderType = genderType;
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
