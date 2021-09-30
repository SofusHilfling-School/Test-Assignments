using System;
using System.Text;

namespace StringUtilities
{
    public static class StringHelper
    {
        private static int ASCIILetterDelta = 'a' - 'A';

        public static string Reverse(this string input)
        {
            _ = input ?? throw new ArgumentNullException(nameof(input));
            if (input == string.Empty)
                return input;

            char[] output = new char[input.Length];
            for (int outputIndex = 0, inputIndex = input.Length - 1; outputIndex < input.Length; outputIndex++, inputIndex--)
            {
                if (isMultipleSpanEmoji(inputIndex))
                {
                    output[outputIndex + 1] = input[inputIndex];
                    output[outputIndex] = input[inputIndex - 1];
                    outputIndex++;
                    inputIndex--;
                }
                else
                    output[outputIndex] = input[inputIndex];
            }

            return new string(output);

            bool isMultipleSpanEmoji(int index)
                => index > 0
                    && input[index] >= 0xDC00 
                    && input[index] <= 0xDFFF
                    && input[index - 1] >= 0xD800 
                    && input[index - 1] <= 0xDBFF;
        }

        public static string Upper(this string text)
        {
            _ = text ?? throw new ArgumentNullException(nameof(text));

            StringBuilder result = new StringBuilder();
            foreach(char c in text)
            {
                if ((c >= 'a' && c <= 'z') || c == 'æ' || c == 'ø' || c == 'å')
                    result.Append((char)(c - ASCIILetterDelta));
                else
                    result.Append(c);
            }
            return result.ToString();
        }

        public static string Lower(this string text)
        {
            _ = text ?? throw new ArgumentNullException(nameof(text));

            char[] outputChars = new char[text.Length];
            for(int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if ((c >= 'A' && c <= 'Z') || c == 'Æ' || c == 'Ø' || c == 'Å')
                    outputChars[i] = (char)(c + ASCIILetterDelta);
                else
                    outputChars[i] = c;
            }
            return new string(outputChars);
        }
    }
}
