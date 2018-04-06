using AddressSplitterLib.AO;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace AddressSplitterLib.Utils
{

    /// <summary>
    /// Сортирует по убыванию индекса вхождения.
    /// </summary>
    class MatchIndexComparer : IComparer<Match>
    {
        public int Compare(Match x, Match y)
        {
            if (x.Index > y.Index) return -1;
            if (x.Index < y.Index) return 1;
            return 0;
        }
    }

    /// <summary>
    /// Сортирует адресные обьекты по возрастанию.
    /// </summary>
    class AOComparer : IComparer<AddressObject>
    {
        public int Compare(AddressObject x, AddressObject y)
        {
            if (x.Type != null && y.Type != null)
            {
                if (x.Type.Level != (int)ObjectLevel.House && x.Type.Level != (int)ObjectLevel.Room &&
                    y.Type.Level != (int)ObjectLevel.House && y.Type.Level != (int)ObjectLevel.Room) return (x.Type.Level > y.Type.Level) ? 1 : -1;

                if ((x.Type.Level == (int)ObjectLevel.House || x.Type.Level == (int)ObjectLevel.Room) &&
                      y.Type.Level != (int)ObjectLevel.House && y.Type.Level != (int)ObjectLevel.Room) return 1;

                if (x.Type.Level != (int)ObjectLevel.House && x.Type.Level != (int)ObjectLevel.Room &&
                    (y.Type.Level != (int)ObjectLevel.House || y.Type.Level != (int)ObjectLevel.Room)) return -1;
            }
            if (x.Type == null && y.Type != null && y.Type.Level != (int)ObjectLevel.House && y.Type.Level != (int)ObjectLevel.Room) return 1;

            if (x.Type != null && x.Type.Level != (int)ObjectLevel.House && x.Type.Level != (int)ObjectLevel.Room && y.Type == null) return -1;

            if (x.Type == null && y.Type != null && (y.Type.Level == (int)ObjectLevel.House || y.Type.Level == (int)ObjectLevel.Room)) return -1;

            if (x.Type != null && (x.Type.Level == (int)ObjectLevel.House || x.Type.Level == (int)ObjectLevel.Room) && y.Type == null) return 1;

            return 0;
        }
    }

    class VariantCorrectnessComparer : IComparer<Variant>
    {
        public int Compare(Variant x, Variant y)
        {
            if (x.GetProbability() > y.GetProbability()) return -1;
            if (x.GetProbability() < y.GetProbability()) return 1;
            return 0;
        }
    }
}
