using AddressParserLib.AO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AddressParserLib
{
    internal class AddressTruncator
    {
        private Regex houseRegex;
        private Regex postalRegex;
        private Regex[] regionRegex;

        private List<AddressObjectType> objectTypes;


        internal AddressTruncator(List<AddressObjectType> objectTypes)
        {
            this.objectTypes = objectTypes;

            houseRegex = new Regex(@"([0-9]+)|((д\.| дом).*[0-9]+.*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            postalRegex = new Regex(@"[0-9]{6}", RegexOptions.Compiled);
            regionRegex = new Regex(@"(?<=\W|^).+?ая(?=((\W)+о))", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
        }


        /// <returns>Возвращает индекс из исходной строки.</returns>
        internal string TruncPostalCode(string source) => postalRegex.Match(source).Value;


        /// <returns>Вовзращает AddressObject номера здания/сооружения и AddressObject номера помещения.</returns>
        internal (AddressObject buildingNum, AddressObject roomNum) TruncBuildingAndRoomNum(string source)
        {
            //дом: здесь сначала ищем по патернам данным нам, если не находим, то ищем просто любое число с символом
            return (null, null);
            //после чего ищем опять по патерну квартиру, если не находим то берём любое число если есть.
        }


        private string GetHousePatterns(AddressObjectType.GenderNoun gender)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("(");
            foreach (var obj in objectTypes)
            {
                if(obj.Level == (int) ObjectLevel.House && obj.GenderType == gender)
                {
                    if(sb.ToString() == "(")
                    {
                        sb.Append(obj.AbbreviatedName);
                        continue;
                    }
                    sb.Append("|");
                    sb.Append(obj.AbbreviatedName);
                }
            }
   
            sb.Append(")");

            return sb.ToString();
        }
    }
}

