using FahrenheirToCelciusConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FahrenheirToCelciusConverterTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void ToCelcius_LowerThanAbsoluteZero_ThrowOutOfRangeException()
        {
            double temperatureInFahrenheir = -459.68d;
            TemperatureConverter converter = new();

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => converter.ToCelcius(temperatureInFahrenheir));
        }

        [TestMethod]
        public void ToCelcius_AbsoluteZero_MatchExpectedCelciusValue()
        {
            double temperatureInFahrenheir = -459.67d;
            double expectedResult = -273.15d;

            TemperatureConverter converter = new();
            double celciusTempResult = converter.ToCelcius(temperatureInFahrenheir);

            Assert.AreEqual(expectedResult, celciusTempResult);
        }

        [TestMethod]
        public void ToCelcius_WaterFrezzing_MatchExpectedCelciusValue()
        {
            double temperatureInFahrenheir = 32.0d;
            double expectedResult = 0.0d;

            TemperatureConverter converter = new();
            double celciusTempResult = converter.ToCelcius(temperatureInFahrenheir);

            Assert.AreEqual(expectedResult, celciusTempResult);
        }

        [TestMethod]
        public void ToCelcius_50FToC_MatchExpectedCelciusValue()
        {
            double temperatureInFahrenheir = 50.0d;
            double expectedResult = 10.0d;

            TemperatureConverter converter = new();
            double celciusTempResult = converter.ToCelcius(temperatureInFahrenheir);

            Assert.AreEqual(expectedResult, celciusTempResult);
        }

        [TestMethod]
        public void ToCelcius_BodyTemperature_MatchExpectedCelciusValue()
        {
            double temperatureInFahrenheir = 98.6d;
            double expectedResult = 37.0d;

            TemperatureConverter converter = new();
            double celciusTempResult = converter.ToCelcius(temperatureInFahrenheir);

            Assert.AreEqual(expectedResult, celciusTempResult, 0.1);
        }

        [TestMethod]
        public void ToCelcius_100F_MatchExpectedCelciusValue()
        {
            double temperatureInFahrenheir = 100.0d;
            double expectedResult = 37.78d;

            TemperatureConverter converter = new();
            double celciusTempResult = converter.ToCelcius(temperatureInFahrenheir);

            Assert.AreEqual(expectedResult, celciusTempResult, 0.01);
        }

        [TestMethod]
        public void ToCelcius_DoubleMax_MatchExpectedCelciusValue()
        {
            double temperatureInFahrenheir = double.MaxValue;
            double expectedResult = 9.98718408256842E+307;

            TemperatureConverter converter = new();
            double celciusTempResult = converter.ToCelcius(temperatureInFahrenheir);

            Assert.AreEqual(expectedResult, celciusTempResult, 0.01);
        }

        [TestMethod]
        public void ToFahrenheit_LowerThanAbsoluteZero_ThrowOutOfRangeException()
        {
            double temperatureInCelcius = -273.16d;
            TemperatureConverter converter = new();
            
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => converter.ToFahrenheit(temperatureInCelcius));
        }

        [TestMethod]
        public void ToFahrenheit_AbsoluteZero_MatchExpectedFahrenheitValue()
        {
            double temperatureInCelcius = -273.15d;
            double expectedResult = -459.67d;
            TemperatureConverter converter = new();

            double fahrenheitTempResult = converter.ToFahrenheit(temperatureInCelcius);

            Assert.AreEqual(expectedResult, fahrenheitTempResult, 0.01);
        }

        [TestMethod]
        public void ToFahrenheit_WaterFrezzing_MatchExpectedFahrenheitValue()
        {
            double temperatureInCelcius = 0d;
            double expectedResult = 32d;
            TemperatureConverter converter = new();

            double fahrenheitTempResult = converter.ToFahrenheit(temperatureInCelcius);

            Assert.AreEqual(expectedResult, fahrenheitTempResult);
        }
        
        [TestMethod]
        public void ToFahrenheit_BoilingWater_MatchExpectedFahrenheitValue()
        {
            double temperatureInCelcius = 100d;
            double expectedResult = 212d;
            TemperatureConverter converter = new();

            double fahrenheitTempResult = converter.ToFahrenheit(temperatureInCelcius);

            Assert.AreEqual(expectedResult, fahrenheitTempResult);
        }

        [TestMethod]
        public void ToFahrenheit_DoubleMax_ThrowOutOfRangeException()
        {
            double temperatureInCelcius = double.MaxValue;
            TemperatureConverter converter = new();

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => converter.ToFahrenheit(temperatureInCelcius));
        }

        [TestMethod]
        public void ToFahrenheit_MaxCelciusValueSupported_MatchExpectedFahrenheitValue()
        {
            double temperatureInCelcius = 9.98718408256842E+307;
            double expectedResult = double.MaxValue;
            TemperatureConverter converter = new();

            double fahrenheitTempResult = converter.ToFahrenheit(temperatureInCelcius);

            Assert.AreEqual(expectedResult, fahrenheitTempResult);
        }

        [TestMethod]
        public void ToFahrenheit_AboveMaxCelciusValueSupported_ThrowOutOfRangeException()
        {
            double temperatureInCelcius = 9.99E+307;
            TemperatureConverter converter = new();

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => converter.ToFahrenheit(temperatureInCelcius));
        }

    }
}
