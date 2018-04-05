using AddressParserLib.AO;
using AddressParserLib.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using static AddressParserLib.AddressObjectType;

namespace AddressParserLib
{
    internal class AddressTruncator
    {
        private AOTypeDictionary typeDictionary;
        private RegexGroup regexGroup;
        private StringBuilder sb;

        private const string litterPattern = @"([^а-я]*?)([а-я](?=[^а-я]|$))*";
        private const string typeAndNumber = @"(({0}[^а-я]*?)[0-9]+(\/[0-9]+)*)" + litterPattern;

        private const string buildingLitterPattern = @"(?<=[0-9]+[^а-я]*)ли.*?(\.| +)(?=[а-я])";
        private const string simpleHousePattern = @"[0-9]+(\/[0-9]+)*" + litterPattern;
        private const string postalPattern = @"[0-9]{6}";
        private const string anyWordPattern = "(?<= |^).*?(?= |$)";
        private const string housingNumberPattern = "[^а-я]кор[а-я]*?[^а-я]*?[0-9]+";
        private const string housingLitterPattern = "[^а-я]кор.*?[^а-я]+[а-я]+";

        private string allTypesMultiPattern;
        private string regionTypesMultiPattern;
        private string roomTypesMultiPattern;
        private string streetTypesMultiPattern;
        private string houseTypesMultiPattern;
        
        private string fullHouseTypesPattern;
        private string fullRoomTypesPattern;
        private string regionPattern;
        private string housePredictPattern;

        internal AddressTruncator(AOTypeDictionary typeDictionary)
        {
            this.typeDictionary = typeDictionary;
            sb = new StringBuilder();

            houseTypesMultiPattern = typeDictionary.GetRegexMultiPattern((int)ObjectLevel.House);
            roomTypesMultiPattern = typeDictionary.GetRegexMultiPattern((int)ObjectLevel.Room);
            regionTypesMultiPattern = typeDictionary.GetRegexMultiPattern((int)ObjectLevel.Region, GenderNoun.Fiminine);
            streetTypesMultiPattern = typeDictionary.GetRegexMultiPattern((int)ObjectLevel.Street);

            fullHouseTypesPattern = String.Format(typeAndNumber, houseTypesMultiPattern);
            fullRoomTypesPattern = String.Format(typeAndNumber, roomTypesMultiPattern);

            regionPattern = String.Format("^[^ ]+ая +{0}(?=[^а-я]+?)", regionTypesMultiPattern);
            allTypesMultiPattern = String.Format(@"(?<=[^а-я]|^){0}(\.+?|(?=[^а-я]|$))", typeDictionary.GetRegexMultiPattern());
            //allTypesMultiPattern = String.Format(@"(?<=[^а-я]|^)(к).*?((?=[^а-я]|$))", typeDictionary.GetRegexMultiPattern());
            housePredictPattern = String.Format("(?<={0}[^а-я]*)[0-9]+", streetTypesMultiPattern) + litterPattern;


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
        /// Очищает и возвращает варианты AddressObject номера здания/сооружения и AddressObject номера помещения.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        internal List<Variant> TruncBuildingAndRoomNum(ref string source)
        {
            List<Variant> variants = new List<Variant>();

            AddressObject buildingAO = null;
            AddressObject roomAO = null;

            int buildingIndex=-1;

            //парсим строение по 8 паттерну
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
                buildingIndex = buildings[0].outer.Index;
                buildingAO = new AddressObject(sb.ToString(), typeDictionary.GetAOType(buildingType));
            }


            //парсим помещение по 9 паттерну
            var rooms = regexGroup.GetInnerMatch(fullRoomTypesPattern, roomTypesMultiPattern, source);
            if (rooms.Count > 0)
            {
                source = source.Remove(rooms[0].outer.Index, rooms[0].outer.Length);
                string roomName = rooms[0].outer.Value.Replace(rooms[0].inner.Value, "");

                sb.Clear();
                bool digitFinded = false;
                for (int i = 0; i < roomName.Length; i++)
                {
                    if (Char.IsDigit(roomName[i]))
                        digitFinded = true;
                    if (digitFinded && Char.IsLetterOrDigit(roomName[i]))
                    {
                        sb.Append(roomName[i]);
                    }
                }
                string roomType = rooms[0].inner.Value;
                roomAO = new AddressObject(sb.ToString(), typeDictionary.GetAOType(roomType));
            }

            //ищем дом по патерну 7:(ул|ш)[^а-я]*[0-9]+
            if (buildingAO == null)
            {

                var housesPredict = regexGroup.GetMatch(housePredictPattern, source);
                if (housesPredict.Success)
                {
                    source = source.Remove(housesPredict.Index, housesPredict.Length);
                    buildingIndex = housesPredict.Index;
                    buildingAO = new AddressObject(housesPredict.Value, new AddressObjectType(null, null, (int)ObjectLevel.House));
                }
            }

            //если по паттернам не получилось, то просто берём самое последнее число;
            if (buildingAO == null)
            {
                var lastNumber = regexGroup.GetMatch(simpleHousePattern, source);
                if (lastNumber.Success)
                {
                    source = source.Remove(lastNumber.Index, lastNumber.Length);
                    buildingIndex = lastNumber.Index;
                    buildingAO = new AddressObject(lastNumber.Value, new AddressObjectType(null, null, (int)ObjectLevel.House));
                }
            }

            //проверяем корпус  цифру
            var housingNumber = regexGroup.GetInnerMatch(housingNumberPattern, "[0-9]+", source);
            if(housingNumber.Count>0)
            {
                source = source.Remove(housingNumber[0].outer.Index, housingNumber[0].outer.Length);

                var buildingWithHousing = new Variant();
                buildingWithHousing.Add(new AddressObject(buildingAO.Name + "/" + housingNumber[0].inner.Value,
                    new AddressObjectType(null,null,(int)ObjectLevel.House)),3);
                buildingWithHousing.Add(roomAO);
                variants.Add(buildingWithHousing);

                if(roomAO==null)
                {
                    var housingAsRoom = new Variant();
                    housingAsRoom.Add(buildingAO);
                    housingAsRoom.Add(new AddressObject(housingNumber[0].inner.Value,new AddressObjectType(null,null,(int)ObjectLevel.Room)),2);
                    variants.Add(housingAsRoom);
                }
            }

            //проверяем корпус букву
            var housingLitter = regexGroup.GetMatch(housingLitterPattern, source);
            if (housingLitter.Success)
            {
                source = source.Remove(housingLitter.Index, housingLitter.Length);

                var buildingWithLitter = new Variant();
                buildingWithLitter.Add(new AddressObject
                    (buildingAO.Name + housingLitter.Value[housingLitter.Value.Length-1], 
                                                new AddressObjectType(null, null, (int)ObjectLevel.House)),3);
                buildingWithLitter.Add(roomAO);
                variants.Add(buildingWithLitter);
            }

            var defVar = new Variant();
            defVar.Add(buildingAO);
            defVar.Add(roomAO);
            variants.Add(defVar);

            if(buildingIndex!=-1&&buildingIndex<source.Length-1)
            {
                source = source.Remove(buildingIndex);
            }

            return variants;
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
                string regionName = region.outer.Value.Remove(region.inner.Index, region.inner.Length).Replace(" ","");

                result.Add(new AddressObject(regionName, typeDictionary.GetAOType(region.inner.Value)));
            }

            return result;
        }


        /// <summary>
        /// Очищает строку от всех названий типов обьектов.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string ClearString(string source) => regexGroup.Replace(allTypesMultiPattern, source, "");

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

                var AOTypes = new List<Match>();

                var clearedString = subs;

                ///TODO:  ужно очстить от всех типов обьектов

                clearedString = clearedString.Trim(' ').Trim('.').Replace("  "," ").Replace("  "," ");

                if (AOTypes.Count == 1 && regexGroup.GetMatches(anyWordPattern, clearedString).Count == 1)
                {
                    //возвращаем clearedString так как там точно один обьект
                    Variant newVar = new Variant();
                    newVar.Add(new AddressObject(clearedString, typeDictionary.GetAOType(AOTypes[0].Value)),1);

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

                n.Add(new AddressObject(clearSource[0]));
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
                    int mask = ((int)Math.Pow(2, clearSource.Count - 2 - j));
                    cur = i & mask;
                    if (sb.Length > 0)
                        sb.Append(" ");
                    sb.Append(clearSource[j]);
                    if (j == clearSource.Count - 1 || cur == mask)
                    {
                        var address = new AddressObject(sb.ToString());
                        newVariant.Add(address);
                        sb.Clear();
                    }
                }
                result.Add(newVariant);
            }

            return result;
        }
    }
}

