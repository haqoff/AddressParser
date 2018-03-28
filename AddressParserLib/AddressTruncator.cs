using AddressParserLib.AO;
using AddressParserLib.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AddressParserLib
{
    /// <summary>
    /// 
    /// </summary>
    internal class AddressTruncator
    {
        private const string litterPattern = @"\s*([а-я](?=[^а-я]|$))*";
        private const string uniPattern = @"((?<={0}.*?)[0-9]+(\/[0-9]+)*)" + litterPattern;

        private const string buildingLitterPattern = @"(?<=[0-9]+[^а-я]*)ли.*?(\.| +)(?=[а-я])";
        private const string simpleHousePattern = @"[0-9]+" + litterPattern;
        private const string postalPattern = @"[0-9]{6}";
        private const string anyWordPattern = "(?<= |^).*?(?= |$)";

        private string fullHouseTypesPattern;
        private string fullRoomTypesPattern;
        private string AOTypesPattern;

        private AOTypeDictionary typeDictionary;
        private RegexGroup regexGroup;
        private StringBuilder sb;

        internal AddressTruncator(AOTypeDictionary typeDictionary)
        {
            this.typeDictionary = typeDictionary;
            sb = new StringBuilder();

            fullHouseTypesPattern = String.Format(uniPattern, typeDictionary.GetRegexMultiPattern(ObjectLevel.House));
            fullRoomTypesPattern = String.Format(uniPattern, typeDictionary.GetRegexMultiPattern(ObjectLevel.Room));

            AOTypesPattern = String.Format(@"(?<=[^а-я]|^){0}.*?((?=[^а-я]|$))", typeDictionary.GetRegexMultiPattern());

            regexGroup = new RegexGroup(RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // regionRegex = new Regex(@"(?<=\W|^).+?ая(?=((\W)+о))", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.RightToLeft);


        }




        /// <summary>
        /// Очищает и возвращает индекс из исходной строки, если он найден.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        internal string TruncPostalCode(ref string source)
        {
            string value = regexGroup.GetMatch(postalPattern, source).Value;
            if (value != "")
                source = source.Replace(value, "");

            return value;
        }

        /// <summary>
        /// Очищает и возвращает AddressObject номера здания/сооружения и AddressObject номера помещения.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        internal (AddressObject buildingNum, AddressObject roomNum) TruncBuildingAndRoomNum(ref string source)
        {
            source = regexGroup.Replace(buildingLitterPattern, source, "");
            string buildingNum = regexGroup.GetMatch(fullHouseTypesPattern, source).Value;
            if (buildingNum != "")
                source = source.Replace(buildingNum, "");


            var roomMatch = regexGroup.GetMatch(fullRoomTypesPattern, source);
            string roomNum = roomMatch.Value;
            if (roomNum != "")
                source = source.Remove(roomMatch.Index, roomMatch.Length);

            if (buildingNum == "")
            {
                //пока что просто берём самое последнее число в строке и считаем это домом, но в будущем нужно будет на всякий сделать проверку
                if (regexGroup.IsMatch(simpleHousePattern, source))
                {
                    var matches = regexGroup.GetMatches(simpleHousePattern, source);
                    var buildingMatch = matches[matches.Count - 1];
                    buildingNum = buildingMatch.Value;
                    source = source.Remove(buildingMatch.Index, buildingMatch.Length);
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


        /// <summary>
        /// Очищает строку от всех названий типов обьектов.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string ClearString(string source) => regexGroup.Replace(AOTypesPattern, source, "");

        public List<Variant> Split(string source)
        {
            var res = new List<Variant>();

            foreach (var subs in source.Split(',', ';'))
            {
                //Здесь должна проверка на то, что в строке есть хотя бы одна буква
                if (subs == "" || subs == " ")
                    continue;

                var curVariants = new List<Variant>();

                MatchCollection AOTypes = regexGroup.GetMatches(AOTypesPattern, subs);
                var clearedString = ClearString(subs).Trim(' ').Trim('.').Replace("  ", " ");
                if (AOTypes.Count == 1 && regexGroup.GetMatches(anyWordPattern, clearedString).Count == 1)
                {
                    //возвращаем clearedString так как там точно один обьект
                    Variant newVar = new Variant();
                    newVar.AObjects.Add(new AddressObject(clearedString));

                    curVariants.Add(newVar);
                }
                else
                {
                    //перебираем clearstring по вариантам
                    curVariants = GetVariants(clearedString);
                }

                res = Variant.Combine(res, curVariants);
            }
            return res;
        }

        public List<Variant> GetVariants(string source)
        {
            var result = new List<Variant>();

            source = source.Trim(' ');

            char[] splitters = { ' ' };
            var clearSource = new List<String>(source.Split(splitters));

            int ubound = clearSource.Count - 1;
            for (int i = 0; i <= ubound; i++)
            {
                if (clearSource[i] == "" && clearSource[i] == " ")
                {
                    clearSource.RemoveAt(i);
                    ubound--;
                }
            }

            if (clearSource.Count == 1)
            {
                var n = new Variant();

                n.AObjects.Add(new AddressObject(clearSource[0]));
                result.Add(n);
            }

            int countVariants = (clearSource.Count - 1) * 2;
            string binaryMask;
            string additZero;

            for (int i = 0; i < countVariants; i++)
            {
                sb.Clear();
                binaryMask = Convert.ToString(i, 2);
                if (binaryMask.Length < clearSource.Count - 1)
                {
                    additZero = new string('0', clearSource.Count - binaryMask.Length - 1);
                    binaryMask = additZero + binaryMask;
                }

                var newVariant = new Variant();

                for (int j = 0; j < clearSource.Count; j++)
                {
                    if (sb.Length > 0)
                        sb.Append(" ");
                    sb.Append(clearSource[j]);
                    if (j == clearSource.Count - 1 || binaryMask[j] == '1')
                    {
                        var address = new AddressObject(sb.ToString());
                        newVariant.AObjects.Add(address);
                        sb.Clear();
                    }
                }
                result.Add(newVariant);
            }

            return result;
        }
    }
}

