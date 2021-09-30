using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringUtilities;
using System;

namespace StringUtilitiesTests
{
    [TestClass]
    public class StringHelperTests
    {
        [TestMethod]
        public void Reverse_InputStringNull_ThrowsArgumentNullException()
        {
            string value = null;

            Assert.ThrowsException<ArgumentNullException>(() => value.Reverse());
        }

        [TestMethod]
        public void Reverse_WithSmallChars_StringIsReturnedInReverseOrder()
        {
            string value = "hello";
            string expectedResult = "olleh";

            string actualResult = value.Reverse();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Reverse_WithUpperChars_StringIsReturnedInReverseOrder()
        {
            string value = "UPPERCASE STING";
            string expectedResult = "GNITS ESACREPPU";

            string actualResult = value.Reverse();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [DataTestMethod]
        [DataRow("This is a test", "tset a si sihT")]
        [DataRow("aBc", "cBa")]
        public void Reverse_StringWithSmallAndLargeLetters_StringIsReturnedInReverseOrder(string value, string expectedResult)
        {
            string actualResult = value.Reverse();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Reverse_StringNumbers_StringIsReturnedInReverseOrder()
        {
            string value = "12fba aA";
            string expectedResult = "Aa abf21";

            string actualResult = value.Reverse();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Reverse_SpecialCharacters_StringIsReturnedInReverseOrder()
        {
            string value = "@$jk )@|";
            string expectedResult = "|@) kj$@";

            string actualResult = value.Reverse();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Reverse_SpecialLetters_StringIsReturnedInReverseOrder()
        {
            string value = "ø aådæa w";
            string expectedResult = "w aædåa ø";

            string actualResult = value.Reverse();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Reverse_StringWithEmoji_StringIsReturnedInReverseOrder()
        {
            string value = "❤🐨";
            string expectedResult = "🐨❤";

            string actualResult = value.Reverse();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Reverse_WithEscapeChar_StringIsReturnedInReverseOrder()
        {
            string value = "text with new\n line";
            string expectedResult = "enil \nwen htiw txet";

            string actualResult = value.Reverse();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [DataTestMethod]
        [DataRow("aBc", "ABC")]
        [DataRow("this is a test", "THIS IS A TEST")]
        [DataRow("12fba aA", "12FBA AA")]
        [DataRow("@$jk )@|", "@$JK )@|")]
        [DataRow("❤🐨", "❤🐨")]
        [DataRow("text with new\n line", "TEXT WITH NEW\n LINE")]
        public void Upper_Everything_ReturnStringWithAllLettersUppercase(string value, string expectedResult)
        {
            string actualResult = value.Upper();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Upper_InputStringNull_ThrowsArgumentNullException()
        {
            string value = null;

            Assert.ThrowsException<ArgumentNullException>(() => value.Upper());
        }

        [DataTestMethod]
        [DataRow("abc", "abc")]
        [DataRow("this is a test", "this is a test")]
        [DataRow("12fba aA", "12fba aa")]
        [DataRow("@$jk )@|", "@$jk )@|")]
        [DataRow("❤🐨", "❤🐨")]
        [DataRow("text with new\n line", "text with new\n line")]
        public void Lower_Everything_ReturnStringWithAllLettersUppercase(string value, string expectedResult)
        {
            string actualResult = value.Lower();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Lower_InputStringNull_ThrowsArgumentNullException()
        {
            string value = null;

            Assert.ThrowsException<ArgumentNullException>(() => value.Upper());
        }
    }
}
