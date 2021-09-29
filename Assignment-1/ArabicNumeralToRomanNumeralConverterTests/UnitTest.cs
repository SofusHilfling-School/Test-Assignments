using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArabicNumeralToRomanNumeralConverter;
using System;

namespace ArabicNumeralToRomanNumeralConverterTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void ToRoman_AboveMax_OutOfRangeExceptionIsThrown()
        {
            int inputValue = 4000;
            NumeralConverter converter = new();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => converter.ToRoman(inputValue));
        }

        [TestMethod]
        public void ToRoman_Zero_OutOfRangeExceptionIsThrown()
        {
            int inputValue = 0;
            NumeralConverter converter = new();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => converter.ToRoman(inputValue));
        }
        
        public void ToRoman_NegativeValue_OutOfRangeExceptionIsThrown() 
        {
            int inputValue = -1;
            NumeralConverter converter = new();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => converter.ToRoman(inputValue));
        }

        [DataTestMethod]
        [DataRow(1, "I")]
        [DataRow(5, "V")]
        [DataRow(10, "X")]
        [DataRow(50, "L")]
        [DataRow(100, "C")]
        [DataRow(500, "D")]
        [DataRow(1000, "M")]
        public void ToRoman_BaseValue_ReturnRomanNumeralEqualToInput(int inputValue, string expectedValue)
        {
            NumeralConverter converter = new();

            string result = converter.ToRoman(inputValue);

            Assert.AreEqual(expectedValue, result);
        }

        [DataTestMethod]
        [DataRow(82, "LXXXII")]
        [DataRow(123, "CXXIII")]
        [DataRow(989, "CMLXXXIX")]
        [DataRow(3999, "MMMCMXCIX")]
        public void ToRoman_(int inputValue, string expectedValue)
        {
            NumeralConverter converter = new();

            string result = converter.ToRoman(inputValue);

            Assert.AreEqual(expectedValue, result);
        }
    }
}
