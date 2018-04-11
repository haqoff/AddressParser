using System;
using System.Text;

namespace AddressSplitterLib.Utils
{
    public static class StringHelper
    {
        private static StringBuilder sb = new StringBuilder();
       
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
                if (digitFinded && (Char.IsLetterOrDigit(source[i]) || source[i]=='/'))
                {
                    sb.Append(source[i]);
                }
            }

            return sb.ToString();
        }

        public static string GetLettersOrNumbersAfterSlash(string source)
        {
            sb.Clear();
            bool numeric = false;

            foreach (char ch in source)
            {
                if (ch == '/')
                {
                    numeric = true;
                    continue;
                }
                if (Char.IsLetter(ch) || numeric) sb.Append(ch);
            }
            return sb.ToString();
        }
    }
}
