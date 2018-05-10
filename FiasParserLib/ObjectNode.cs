﻿namespace FiasParserLib
{
    public class ObjectNode
    {
        public string Guid { get; private set; }
        public string Name { get; private set; }
        public string ShortNameType { get; private set; }
        public string ParentGuid { get; private set; }
        public ObjectNode Parent { get; private set; }
        public int AOLevel { get; private set; }
        public TableType Type { get; private set; }

        public ObjectNode(string guid, string name, TableType type, string parentGuid ,ObjectNode parent, string shortNameType, int aoLevel)
            : this(guid, name, type, parentGuid,parent)
        {
            ShortNameType = shortNameType;
            AOLevel = aoLevel;
        }

        public ObjectNode(string guid, string name, TableType type, string ParentGuid , ObjectNode parent)
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

        public override int GetHashCode()
        {
            int result = Guid.GetHashCode() + Name.GetHashCode() + AOLevel.GetHashCode() + Type.GetHashCode();
            if (ShortNameType != null) result += ShortNameType.GetHashCode();
            if (Parent != null) result += Parent.GetHashCode();
            return result;
        }
    }
}
