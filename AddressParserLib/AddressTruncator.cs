using AddressParserLib.AO;
using AddressParserLib.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static AddressParserLib.AddressObjectType;

namespace AddressParserLib
{
    internal class AddressTruncator
    {
        private const string litterPattern = @"([^а-я]*)([а-я](?=[^а-я]|$))*";
        private const string uniPattern = @"(((д|дом)[^а-я]*?)[0-9]+(\/[0-9]+)*)" + litterPattern;

        private const string buildingLitterPattern = @"(?<=[0-9]+[^а-я]*)ли.*?(\.| +)(?=[а-я])";
        private const string simpleHousePattern = @"[0-9]+" + litterPattern;
        private const string postalPattern = @"[0-9]{6}";
        private const string anyWordPattern = "(?<= |^).*?(?= |$)";

        private string fullHouseTypesPattern;
        private string fullRoomTypesPattern;
        private string AOTypesPattern;
        private string roomTypesMultiPattern;
        private string houseTypesMultiPattern;
        private string regionPattern;
        private string regionTypesMultiPattern;

        private AOTypeDictionary typeDictionary;
        private RegexGroup regexGroup;
        private StringBuilder sb;

        internal AddressTruncator(AOTypeDictionary typeDictionary)
        {
            this.typeDictionary = typeDictionary;
            sb = new StringBuilder();

            houseTypesMultiPattern = typeDictionary.GetRegexMultiPattern((int)ObjectLevel.House);
            roomTypesMultiPattern = typeDictionary.GetRegexMultiPattern((int)ObjectLevel.Room);
            regionTypesMultiPattern = typeDictionary.GetRegexMultiPattern((int)ObjectLevel.Region, GenderNoun.Fiminine);


            fullHouseTypesPattern = String.Format(uniPattern, houseTypesMultiPattern);
            fullRoomTypesPattern = String.Format(uniPattern, roomTypesMultiPattern);

            if (regionTypesMultiPattern != "()")
                regionPattern = String.Format("^[^ ]+ая +{0}.*?(?=[^а-я]+?)", regionTypesMultiPattern);

            AOTypesPattern = String.Format(@"(?<=[^а-я]|^){0}((?=[^а-я]|$))", typeDictionary.GetRegexMultiPattern());

            regexGroup = new RegexGroup(RegexOptions.Compiled | RegexOptions.IgnoreCase);
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
            AddressObject buildingAO = null;
            AddressObject roomAO = null;

            source = regexGroup.Replace(buildingLitterPattern, source, "");
            var buildings = regexGroup.GetInnerMatch(fullHouseTypesPattern, houseTypesMultiPattern, source);
            if (buildings.Count > 0)
            {
                source = source.Replace(buildings[0].outer.Value, "");
                string buildingName = buildings[0].outer.Value.Replace(buildings[0].inner.Value, "").Replace(" ", "");

                sb.Clear();
                bool digitFinded = false;
                for (int i = 0; i < buildingName.Length; i++)
                {
                    if (Char.IsDigit(buildingName[i]))
                        digitFinded = true;
                    if (digitFinded && Char.IsLetterOrDigit(buildingName[i]))
                    {
                        sb.Append(buildingName[i]);
                    }
                }
                string buildingType = buildings[0].inner.Value;
                buildingAO = new AddressObject(sb.ToString(), typeDictionary.GetAOType(buildingType));
            }

            var rooms = regexGroup.GetInnerMatch(fullRoomTypesPattern, roomTypesMultiPattern, source);
            if (rooms.Count > 0)
            {
                source = source.Replace(rooms[0].outer.Value, "");
                string roomName = rooms[0].outer.Value.Replace(rooms[0].inner.Value, "");
                string roomType = rooms[0].inner.Value;
                roomAO = new AddressObject(roomName, typeDictionary.GetAOType(roomType));
            }

            if (buildingAO == null)
            {
                //пока что просто берём самое последнее число в строке и считаем это домом, но в будущем нужно будет на всякий сделать проверку
                //перед этим ещё сделать просто первое число после ул(7 типа обьекта),по паттерну (ул|ш)[^а-я]*[0-9]+, брать его
                //если уж и такого нет тоюзаем этот алгоритм.
                if (regexGroup.IsMatch(simpleHousePattern, source))
                {
                    var matches = regexGroup.GetMatches(simpleHousePattern, source);
                    var buildingMatch = matches[matches.Count - 1];
                    buildingAO = new AddressObject(buildingMatch.Value.Replace(" ", ""), new AddressObjectType(null, null, (int)ObjectLevel.House, GenderNoun.Uknown));
                    source = source.Remove(buildingMatch.Index, buildingMatch.Length);
                }
            }

            return (buildingAO, roomAO);
        }


        /// <summary>
        /// Очищает и возвращает объекты, однозначно распарсенные по позиции.    
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        internal List<AddressObject> TruncByCorrectPos(ref string source)
        {
            var result = new List<AddressObject>();

            var matches = regexGroup.GetInnerMatch(regionPattern, regionTypesMultiPattern, source);

            if (matches?.Count > 0)
            {
                var region = matches[0];
                source = source.Replace(region.outer.Value, "");
                string regionName = region.outer.Value.Remove(region.inner.Index, region.inner.Length);

                result.Add(new AddressObject(regionName, typeDictionary.GetAOType(region.inner.Value)));
            }

            return result;
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
                //Проверка что в строке есть хотя бы 2 буквы
                int countLettersOrDigits = 0;
                foreach (var c in subs)
                {
                    if (Char.IsLetterOrDigit(c))
                        countLettersOrDigits++;
                }
                if (countLettersOrDigits < 2)
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

            int cur;

            for (int i = 0; i < countVariants; i++)
            {
                sb.Clear();

                var newVariant = new Variant();

                for (int j = 0; j < clearSource.Count; j++)
                {
                    cur = i & ((int) Math.Pow(2, clearSource.Count-2-j));
                    if (sb.Length > 0)
                        sb.Append(" ");
                    sb.Append(clearSource[j]);
                    if (j == clearSource.Count - 1 || cur == i)
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

