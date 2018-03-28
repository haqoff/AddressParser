using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressParserLib
{
    class Variant
    {
        public List<AddressObject> AObjects { get; private set; }

        public Variant()
        {
            AObjects = new List<AddressObject>();
        }

        public static List<Variant> Combine(List<Variant> a, List<Variant> b)
        {
            var combineds = new List<Variant>();

            if (a.Count == 0)
                return b;
            if (b.Count == 0)
                return a;

            foreach (var aVar in a)
            {
                foreach (var bVar in b)
                {
                    var newVar = new Variant();
                    newVar.AObjects.AddRange(aVar.AObjects);
                    newVar.AObjects.AddRange(bVar.AObjects);
                    combineds.Add(newVar);
                }
            }
            return combineds;
        }
    }
}
