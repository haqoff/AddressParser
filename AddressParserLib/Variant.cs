﻿using AddressParserLib.AO;
using AddressParserLib.Utils;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AddressParserLib
{
    public class Variant : IEnumerable<AddressObject>
    {
        private List<AddressObject> AObjects;
        private int knownProbability = 0;

        public Variant()
        {
            AObjects = new List<AddressObject>();
        }

        public void Add(AddressObject ao, int probability = 0)
        {
            if (ao != null)
            {
                AObjects.Add(ao);
                knownProbability += probability;
            }
        }

        public void AddRange(List<AddressObject> objects)
        {
            AObjects.AddRange(objects);
        }


        /// <summary>
        /// Возвращает вероятность того, что вариант является верным.
        /// Чем больше число, тем вероятней, что вариант правильный.
        /// </summary>
        /// <returns></returns>
        public int GetProbability()
        {
            int tempProbability = knownProbability + AObjects.Count;
            int needForFullAddress = 0;
            int nullTypeObjects = 0;

            if (GetAO((int)ObjectLevel.House) != null)
                tempProbability--;

            if (GetAO((int)ObjectLevel.Room) != null)
                tempProbability--;

            if (GetAO((int)ObjectLevel.Street) == null)
            {
                needForFullAddress++;

                if (GetAO((int)ObjectLevel.Locality) == null)
                {
                    needForFullAddress++;

                    if (GetAO((int)ObjectLevel.Region) == null)
                        needForFullAddress++;

                }


            }




            foreach (var obj in AObjects)
            {
                if (obj.Type == null)
                    nullTypeObjects++;
            }

            tempProbability -= Math.Abs(needForFullAddress - nullTypeObjects) * 2;
            return tempProbability;
        }


        /// <summary>
        /// Возвращает AddressObject по уровню, если найден.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public AddressObject GetAO(int level)
        {
            foreach (var obj in AObjects)
            {
                if (obj.Type?.Level == level)
                    return obj;
            }
            return null;
        }

        public static List<Variant> Combine(List<Variant> a, List<Variant> b)
        {

            if (a.Count == 0)
                return b;
            if (b.Count == 0)
                return a;

            var combineds = new List<Variant>();

            foreach (var aVar in a)
            {
                foreach (var bVar in b)
                {
                    var newVar = new Variant();
                    newVar.AddRange(aVar.AObjects);
                    newVar.AddRange(bVar.AObjects);
                    newVar.knownProbability = aVar.knownProbability + bVar.knownProbability;
                    combineds.Add(newVar);
                }
            }
            return combineds;
        }

        public void Sort()
        {
            AObjects.Sort(new AOComparer());
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
