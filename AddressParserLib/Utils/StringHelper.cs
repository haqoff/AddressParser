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
            bool hasSlash = false;
            bool hasNumberAfterSlash = false;
            bool digitFinded = false;

            for (int i = 0; i < source.Length; i++)
            {
                if (Char.IsDigit(source[i]))
                    digitFinded = true;
                if (digitFinded && (Char.IsLetterOrDigit(source[i]) || source[i] == '/'))
                {
                    if (source[i] == '/') hasSlash = true;
                    if (hasSlash && Char.IsDigit(source[i])) hasNumberAfterSlash = true;
                    if (!(hasNumberAfterSlash && Char.IsLetter(source[i]))) sb.Append(source[i]);
                }
            }

            return sb.ToString();
        }

        public static string GetLettersOrNumbersAfterSlash(string source, out int firstIndex, out int length)
        {
            sb.Clear();
            bool numeric = false;
            firstIndex = -1;
            length = 0;

            for (int i = 0; i < source.Length; i++)
            {
                char ch = source[i];
                if (ch == '/')
                {
                    numeric = true;
                    continue;
                }
                if (Char.IsLetter(ch) || numeric)
                {
                    sb.Append(ch);
                    if (firstIndex == -1) firstIndex = i;
                    length++;
                }
            }
            return sb.ToString();
        }
    }
}
