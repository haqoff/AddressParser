namespace FiasParserLib
{
    public class ObjectNode
    {
        public string Guid { get; private set; }
        public string Name { get; private set; }
        public string ShortNameType { get; private set; }
        public ObjectNode Parent { get; private set; }
        public int AOLevel { get; private set; }
        public TableType Type { get; private set; }

        public ObjectNode(string guid, string name, TableType type, ObjectNode parent, string shortNameType, int aoLevel)
            : this(guid, name, type, parent)
        {
            ShortNameType = shortNameType;
            AOLevel = aoLevel;
        }

        public ObjectNode(string guid, string name, TableType type, ObjectNode parent)
        {
            Guid = guid;
            Name = name;
            Type = type;
            Parent = parent;
        }

        public override bool Equals(object obj)
        {
            if (obj is ObjectNode node)
            {
                if (node.AOLevel == AOLevel && node.Guid == Guid && node.Name == Name && node.Parent == Parent
                    && node.ShortNameType == ShortNameType && node.Type == Type) return true;
            }
            return false;
        }
    }
}
