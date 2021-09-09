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
            => (fahrenheirValue - 32.0d) / 1.8d;
    }
}
