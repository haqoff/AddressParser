namespace AddressParserLib
{
    public class AddressObject
    {
        /// <summary>
        /// Имя обьекта.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Тип обьекта.
        /// </summary>
        public AddressObjectType Type { get; private set; }

        public AddressObject(string name, AddressObjectType type = null)
        {
            Name = name;
            Type = type;
        }
    }
}
