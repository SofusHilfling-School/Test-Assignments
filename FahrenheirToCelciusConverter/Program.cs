using System;

namespace FahrenheirToCelciusConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class TemperatureConverter
    {
        public double ToCelcius(double fahrenheirValue)
        {
            if (fahrenheirValue < -459.67d)
                throw new ArgumentOutOfRangeException(nameof(fahrenheirValue), "Cannot be below the value of absolute zero.");
            return (fahrenheirValue - 32.0d) / 1.8d;
        }

        public double ToFahrenheit(double celciusValue)
        {
            if (celciusValue < -273.15d)
                throw new ArgumentOutOfRangeException(nameof(celciusValue), "Cannot be below the value of absolute zero.");
            else if (celciusValue > 9.98718408256842E+307)
                throw new ArgumentOutOfRangeException(nameof(celciusValue), "Converted value is higher than the size of a double.");
            return celciusValue * 1.8d + 32.0d;
        }
    }
}
