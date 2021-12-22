using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TreacherousWhales
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbersAsString = File.ReadAllText("../../../data.txt").Split(",");
            var numbers = new List<int>();
            foreach (var numberAsString in numbersAsString)
            {
                numbers.Add(int.Parse(numberAsString));
            }

            Dictionary<int, int> positionToFuel = new Dictionary<int, int>();
            for (var tryPosition = numbers.Min(); tryPosition <= numbers.Max(); tryPosition++)
            {
                var sumFuel = numbers.Sum(number => Math.Abs(number - tryPosition));
                positionToFuel.Add(tryPosition, sumFuel);
            }
            
            var winningKvPair = positionToFuel.Aggregate((l, r) => l.Value < r.Value ? l : r);
            Console.Write($"Fuel spent is {winningKvPair.Value}");
        }
    }
}