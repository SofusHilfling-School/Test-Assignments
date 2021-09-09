using FahrenheirToCelciusConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FahrenheirToCelciusConverterTests
{
    [TestClass]
    public class UnitTest
    {
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
    }
}
