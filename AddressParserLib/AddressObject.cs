namespace AddressParserLib
{
    public class AddressObject
    {
        /// <summary>
        /// Имя обьекта
        /// </summary>
        public string Name { get; private set; }

        public ObjectType Type { get; private set; }


        /// <summary>
        /// Имя типа обьекта (обл.,ул. и т.п.)
        /// </summary>
        public string TypeName { get; private set; }

        /// <summary>
        /// Уровень обьекта;
        /// </summary>
        public int Level { get; private set; }
 
        public AddressObject(string name, ObjectType type = ObjectType.Other, string typeName = null, int level = -1)
        {
            Name = name;
            Level = level;
            Type = type;
            TypeName = typeName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Возвращает True, если тип или уровень объекта не определён.</returns>
        public bool IsUnknown() => TypeName == null || Level == -1;
    }
}
