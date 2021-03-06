﻿using AddressSplitterLib.AO;
using AddressSplitterLib.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressSplitterLib
{
    public sealed class AddressParser
    {
        private AddressTruncator truncator;
        private StringBuilder sb;


        public AddressParser(AOTypeDictionary dictionary)
        {
            truncator = new AddressTruncator(dictionary);
            sb = new StringBuilder();
        }

        /// <summary>
        /// Возвращает все возможные варианты адресов из строки.
        /// </summary>
        /// <param name="source"></param>
        public List<Variant> Parse(string source)
        {
            var obviousObjects = ParseObvious(ref source).objects;

            var buildingAndRoomVariants = truncator.TruncBuildingAndRoomNum(ref source);

            var splitted = truncator.Split(source);

            var res = Variant.Combine(splitted, buildingAndRoomVariants);

            res.Sort(new ProbabilityCorrectnessComparer());

            foreach (var item in res)
            {
                item.AddRange(obviousObjects);
                item.Sort();
            }

            return res;
        }


        /// <summary>
        /// Очищает и возвращает только однозначные и очевидные обьекты, которые точно являются правильными, из строки.
        /// </summary>
        /// <param name="source"></param>
        internal ObviousObjects ParseObvious(ref string source)
        {
            var preparsedObjects = new List<AddressObject>();

            string _postalCode = truncator.TruncPostalCode(ref source);

            preparsedObjects.AddRange(truncator.TruncByCorrectPos(ref source));

            return new ObviousObjects() {postalCode = _postalCode, objects = preparsedObjects };
        }
        internal struct ObviousObjects
        {
            public string postalCode;
            public List<AddressObject> objects;
        }

    }
}

