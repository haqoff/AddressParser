using System.Collections;
using System.Collections.Generic;

namespace FiasParserLib
{
    public class ObjectNode
    {
        public string Guid { get; private set; }
        public string Name { get; private set; }
        public string ShortNameType { get; private set; }
        public string ParentGuid { get; private set; }
        public ObjectNode Parent { get; set; }
        public int AOLevel { get; private set; }
        public TableType Type { get; private set; }


        public ObjectNode(string guid, string name, TableType type, string parentGuid, ObjectNode parent, string shortNameType, int aoLevel)
            : this(guid, name, type, parentGuid, parent)
        {
            ShortNameType = shortNameType;
            AOLevel = aoLevel;
        }

        public ObjectNode(string guid, string name, TableType type, string parentGuid, ObjectNode parent)
        {
            Guid = guid;
            Name = name;
            Type = type;
            Parent = parent;
            ParentGuid = parentGuid;
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
            int result = Guid.GetHashCode() + AOLevel.GetHashCode() + Type.GetHashCode();
            if (Name != null) result += Name.GetHashCode();
            if (ShortNameType != null) result += ShortNameType.GetHashCode();
            if (Parent != null) result += Parent.GetHashCode();
            return result;
        }

        public class ByNameComparer : IEqualityComparer<ObjectNode>
        {
            public bool Equals(ObjectNode x, ObjectNode y)
            {
                if (x.ToString() == y.ToString()) return true;
                return false;
            }

            public int GetHashCode(ObjectNode obj)
            {
                return obj.ToString().GetHashCode();
            }
        }

        public override string ToString()
        {
            if (Type == TableType.Object) return ShortNameType + " " + Name;
            return Name;
        }

        private enum Status
        {
            NEEDBUILD,
            BUILDED
        }

    }
}
