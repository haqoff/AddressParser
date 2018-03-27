using AddressParserLib.AO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AddressParserLib
{
    internal class AddressTruncator
    {
        private const string litterPattern = @"\s*([а-я](?=[^а-я]|$))*";
        private const string uniPattern = @"((?<={0}.*?)[0-9]+(\/[0-9]+)*)" + litterPattern;

        private List<AddressObjectType> objectTypes;

        private Regex buildingRegex;
        private Regex buildingLitterRegex;
        private Regex anyNumberRegex;

        private Regex roomRegex;
        private Regex postalRegex;

        internal AddressTruncator(List<AddressObjectType> objectTypes)
        {
            this.objectTypes = objectTypes;

            string fullHouseTypesPattern = String.Format(uniPattern, GetPatterns(ObjectLevel.House));
            string fullRoomTypesPattern = String.Format(uniPattern, GetPatterns(ObjectLevel.Room));

            buildingRegex = new Regex(fullHouseTypesPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            buildingLitterRegex = new Regex(@"(?<=[0-9]+[^а-я]*)ли.*?(\.| +)(?=[а-я])", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            roomRegex = new Regex(fullRoomTypesPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            anyNumberRegex = new Regex(@"[0-9]+"+ litterPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            postalRegex = new Regex(@"[0-9]{6}", RegexOptions.Compiled);
            // regionRegex = new Regex(@"(?<=\W|^).+?ая(?=((\W)+о))", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
        }


        /// <returns>Возвращает индекс из исходной строки.</returns>
        internal string TruncPostalCode(string source) => postalRegex.Match(source).Value;


        /// <returns>Возвращает AddressObject номера здания/сооружения и AddressObject номера помещения.</returns>
        internal (AddressObject buildingNum, AddressObject roomNum) TruncBuildingAndRoomNum(string source)
        {
            source = buildingLitterRegex.Replace(source, "");
            string buildingNum = buildingRegex.Match(source).Value;
            if (buildingNum!="")
                source = source.Replace(buildingNum, "");

            string roomNum = roomRegex.Match(source).Value;
            if (roomNum != "")
                source = source.Replace(roomNum, "");

            if (buildingNum == "")
            {
                //пока что просто берём самое последнее число в строке, но в будущем нужно будет на всякий сделать проверку
                if (anyNumberRegex.IsMatch(source))
                {
                    var matches = anyNumberRegex.Matches(source);
                    buildingNum = matches[matches.Count - 1].Value;
                    source = source.Replace(buildingNum, "");
                }

            }

            AddressObject bNum = null;
            AddressObject rNum = null;

            if (buildingNum != "")
            {
                bNum = new AddressObject(buildingNum.Replace(" ", ""));
                if (roomNum != "")
                {
                    rNum = new AddressObject(roomNum.Replace(" ", ""));
                }
            }

            return (bNum, rNum);
        }




        private string GetPatterns(ObjectLevel level)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("(");
            foreach (var obj in objectTypes)
            {
                if (obj.Level == (int)level)
                {
                    if (sb.ToString() == "(")
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

