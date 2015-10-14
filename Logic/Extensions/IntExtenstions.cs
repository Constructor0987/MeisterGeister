using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Logic.Extensions
{
    public static class IntExtenstions
    {
        public static string ToRoman(this int number, bool allowNegative = false)
        {
            bool negative = false;
            if (number < 0 && allowNegative)
            {
                number *= -1;
                negative = true;
            }

            if (number < 0 || number > 3999)
            {
                throw new ArgumentException("Value must be in the range 0 – 3,999.");
            }
            if (number == 0) return "N";

            int[] values = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] numerals = new string[] { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            StringBuilder result = new StringBuilder();

            // Loop through each of the values to diminish the number
            for (int i = 0; i < 13; i++)
            {
                // If the number being converted is less than the test value, append
                // the corresponding numeral or numeral pair to the resultant string
                while (number >= values[i])
                {
                    number -= values[i];
                    result.Append(numerals[i]);
                }
            }
            if (negative)
            {
                number *= -1;
                result.Insert(0, "-");
            }
            return result.ToString();
        }

        public static int ParseRoman(this string romanNumber)
        {
            if (String.IsNullOrWhiteSpace(romanNumber))
                return 0;
            int result = 0;
            romanNumber = romanNumber.ToUpperInvariant();
            romanNumber = romanNumber.Replace("CM", "DCCCC");
            romanNumber = romanNumber.Replace("CD", "CCCC");
            romanNumber = romanNumber.Replace("XC", "LXXXX");
            romanNumber = romanNumber.Replace("XL", "XXXX");
            romanNumber = romanNumber.Replace("IX", "VIIII");
            romanNumber = romanNumber.Replace("IV", "IIII");
            int i = 0;
            char nextChar = romanNumber.ElementAtOrDefault(i);
            while (nextChar == 'I') { result += 1; nextChar = romanNumber.ElementAtOrDefault(++i); }
            while (nextChar == 'V') { result += 5; nextChar = romanNumber.ElementAtOrDefault(++i); }
            while (nextChar == 'X') { result += 10; nextChar = romanNumber.ElementAtOrDefault(++i); }
            while (nextChar == 'L') { result += 50; nextChar = romanNumber.ElementAtOrDefault(++i); }
            while (nextChar == 'C') { result += 100; nextChar = romanNumber.ElementAtOrDefault(++i); }
            while (nextChar == 'D') { result += 500; nextChar = romanNumber.ElementAtOrDefault(++i); }
            while (nextChar == 'M') { result += 1000; nextChar = romanNumber.ElementAtOrDefault(++i); }
            return result;
        }
    }
}
