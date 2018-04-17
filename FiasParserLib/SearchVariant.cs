using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiasParserLib
{
    public class SearchVariant
    {
        public int CountFinded { get; set; }
        public StringBuilder FullAddress { get; set; }
        public string LastFindedName { get; set; }
        public List<ObjectMargins> PrevObjects { get; set; }
        public List<string> HouseGuids { get; set; }
        public List<string> RoomGuids { get; set; }

        public SearchVariant()
        {
            FullAddress = new StringBuilder();
            PrevObjects = new List<ObjectMargins>();
            HouseGuids = new List<string>();
            RoomGuids = new List<string>();
            CountFinded = 0;

            PrevObjects.Add(null);
        }

    }


    public class ObjectMargins
    {
        public string AOGUID;
        public int AOLEVEL;
    }
}
