using System;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var light = new Light("data.txt");
            var amountOfLightsForDay100 = light.CalculateForDay(100);
            Console.WriteLine($"Amount of lights found on day 100: {amountOfLightsForDay100}");
            Console.WriteLine($"Day on which all octopi flashed: {light.RunUntilAllOctopiFlash()}");
        }
    }
}
