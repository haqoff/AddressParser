using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiasParserLib
{
    public class SearchVariant
    {
        public StringBuilder FullAddress { get; set; }
        public List<ObjectMargins> prevObjects { get; set; }
        public List<string> HouseGuids { get; set; }
        public List<string> RoomGuids { get; set; }

        public SearchVariant()
        {
            FullAddress = new StringBuilder();
            prevObjects = new List<ObjectMargins>();
            HouseGuids = new List<string>();
            RoomGuids = new List<string>();

            prevObjects.Add(null);
        }

    }


    public class ObjectMargins
    {
        public string AOGUID;
        public int AOLEVEL;
    }
}
