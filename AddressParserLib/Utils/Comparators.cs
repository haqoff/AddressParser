using AddressSplitterLib.AO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddressSplitterLib.Utils
{
    internal class ByIndexDesending : IComparer<Match>
    {
        public int Compare(Match x, Match y)
        {
            if (x.Index > y.Index) return -1;
            if (x.Index < y.Index) return 1;
            return 0;
        }
    }


    class AOComparer : IComparer<AddressObject>
    {
        public int Compare(AddressObject x, AddressObject y)
        {
            //if (x.Type != null && y.Type != null)
            //{
            //    if (x.Type.Level != (int)ObjectLevel.House && x.Type.Level != (int)ObjectLevel.Room &&
            //        y.Type.Level != (int)ObjectLevel.House && y.Type.Level != (int)ObjectLevel.Room) return (x.Type.Level > y.Type.Level) ? 1 : -1;

            //    if ((x.Type.Level == (int)ObjectLevel.House || x.Type.Level == (int)ObjectLevel.Room) &&
            //          y.Type.Level != (int)ObjectLevel.House && y.Type.Level != (int)ObjectLevel.Room) return 1;

            //    if (x.Type.Level != (int)ObjectLevel.House && x.Type.Level != (int)ObjectLevel.Room &&
            //        (y.Type.Level != (int)ObjectLevel.House || y.Type.Level != (int)ObjectLevel.Room)) return -1;
            //}
          //    if (x.Type == null && y.Type != null && y.Type.Level != (int)ObjectLevel.House && y.Type.Level != (int)ObjectLevel.Room) return 1;

          //  if (x.Type != null && x.Type.Level != (int)ObjectLevel.House && x.Type.Level != (int)ObjectLevel.Room && y.Type == null) return -1;

            if (x.Type == null && y.Type != null && (y.Type.Level == (int)ObjectLevel.House || y.Type.Level == (int)ObjectLevel.Room)) return -1;

            if (x.Type != null && (x.Type.Level == (int)ObjectLevel.House || x.Type.Level == (int)ObjectLevel.Room) && y.Type == null) return 1;

            return 0;
        }
    }

    class ProbabilityCorrectnessComparer : IComparer<Variant>
    {
        public int Compare(Variant x, Variant y)
        {
            var x_prob = x.GetProbability();
            var y_prob = y.GetProbability();
            if (x_prob > y_prob) return -1;
            if (x_prob < y_prob) return 1;
            return 0;
        }
    }
}
