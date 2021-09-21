using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ArabicNumeralToRomanNumeralConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class NumeralConverter
    {
        private static SortedDictionary<int, string> baseValues = 
            new() {
                [1000] = "M",
                [900] = "CM",
                [500] = "D",
                [400] = "CD",
                [100] = "C",
                [90] = "XC",
                [50] = "L",
                [40] = "XL",
                [10] = "X",
                [9] = "IX",
                [5] = "V",
                [4] = "IV",
                [1] = "I"
            };

        public string ToRoman(int arabicValue) 
        {
            if(arabicValue > 3999)
                throw new ArgumentOutOfRangeException(nameof(arabicValue), "Roman numeral cannot have a larger value than 3999.");
            if(arabicValue < 1)
                throw new ArgumentOutOfRangeException(nameof(arabicValue), "Roman numeral cannot have a lower value than 1.");

            if(baseValues.TryGetValue(arabicValue, out string baseValue))
                return baseValue;

            StringBuilder builder = new();
            int remainingArabicValue = arabicValue;
            foreach((int baseArabicValue, string romanValue) in baseValues.OrderByDescending(x => x.Key))
            {
                int baseArabicValueCount = remainingArabicValue / baseArabicValue;
                if(baseArabicValueCount > 0)
                {
                    remainingArabicValue -= baseArabicValue * baseArabicValueCount;
                    for(int i = 0; i < baseArabicValueCount; i++)
                        builder.Append(romanValue);
                }
            }
            return builder.ToString();
        }
    }
}
