using System.Collections;
using System.Collections.Generic;

namespace AddressParserLib
{
    class Variant : IEnumerable<AddressObject>
    {
        private List<AddressObject> AObjects;

        public Variant()
        {
            AObjects = new List<AddressObject>();
        }

        public void Add(AddressObject ao)
        {
            if (ao != null)
                AObjects.Add(ao);
        }

        public void AddRange(List<AddressObject> objects)
        {
            AObjects.AddRange(objects);
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

        public IEnumerator<AddressObject> GetEnumerator()
        {
            return ((IEnumerable<AddressObject>)AObjects).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<AddressObject>)AObjects).GetEnumerator();
        }
    }
}
