using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressParserLib.Utils
{
    public static class Helper
    {
        private static StringBuilder sb
        {
            get
            {
                if (sb == null) sb = new StringBuilder();
                return sb;
            }
            set
            {
                sb = value;
            }

        }

        /// <summary>
        /// Возвращает строку из исходной, начиная с первой найденной цифры, в которой только буквы и цифры.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetOnlyDigitsAndLetters(string source)
        {
            sb.Clear();

            bool digitFinded = false;
            for (int i = 0; i < source.Length; i++)
            {
                if (Char.IsDigit(source[i]))
                    digitFinded = true;
                if (digitFinded && Char.IsLetterOrDigit(source[i]))
                {
                    sb.Append(source[i]);
                }
            }

            return sb.ToString();
        }

    }
}
