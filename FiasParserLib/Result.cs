using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiasParserLib
{
    public class Result
    {
        public List<ObjectKnownMargins> objects = new List<ObjectKnownMargins>();
        public List<HouseKnownMargins> houses = new List<HouseKnownMargins>();
        public List<RoomKnownMargins> rooms = new List<RoomKnownMargins>();
    }

    public struct ObjectKnownMargins
    {
        public string AOGUID;
        public string FORMALNAME;
        public string SHORTNAME;
        public int AOLEVEL;
    }

    public struct HouseKnownMargins
    {
        public string HOUSEGUID;
        public string HOUSENUM;
        public string BUILDNUM;
        public string STRUCTNUM;

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("строение:");
            sb.Append(HOUSENUM);
            if (BUILDNUM != null)
            {
                sb.Append(" корпус:");
                sb.Append(BUILDNUM);
            }

            if (STRUCTNUM != null)
            {
                sb.Append(" стр:");
                sb.Append(STRUCTNUM);
            }
            return sb.ToString();
        }
    }

    public struct RoomKnownMargins
    {
        public string FLATNUMBER;
        public string ROOMGUID;
    }
}
