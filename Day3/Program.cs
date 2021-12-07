using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("dummydata.txt");
            int amountOfLines = lines.Length;
            int digitsPerLine = lines[0].Length;
            
            List<List<int>> allDigits = new List<List<int>>() {};
            for (var idx = 0; idx < digitsPerLine; idx++)
            {
                allDigits.Add(new List<int>());
            }

            for (int currentLine = 0; currentLine < amountOfLines; currentLine++)
            {
                string readLine = lines[currentLine];
                char[] digits = readLine.ToCharArray();

                for (int readDigit = 0; readDigit < digitsPerLine; readDigit++)
                {
                    // Convert char to int
                    var digit = (int) (digits[readDigit] - '0');
                    allDigits.ElementAt(readDigit).Add(digit);
                }
            }

            int[] gammaRates = new int[digitsPerLine];
            int[] epsilonRates = new int[digitsPerLine];
            for (int idx = 0; idx < digitsPerLine; idx++)
            {
                var average = allDigits[idx].Average();
                gammaRates[idx] = average > 0.5 ? 1 : 0;
                epsilonRates[idx] = average < 0.5 ? 1 : 0;
            }

            double gammaRate = 0;
            double epsilonRate = 0;
            for (var idx = 0; idx < digitsPerLine; idx++)
            {
                Console.WriteLine(gammaRates[idx]);
                gammaRate += gammaRates[idx] * Math.Pow(2, digitsPerLine - idx - 1);
                epsilonRate += epsilonRates[idx] * Math.Pow(2, digitsPerLine - idx - 1);
            }

            Console.WriteLine(gammaRate * epsilonRate);

            List<int> oxygenRate = allDigits.SingleOrDefault(digits => digits.ToArray() == gammaRates);

            if (oxygenRate.Any())
            {
                Console.WriteLine("Yeah");
                foreach(var i in oxygenRate){
                    Console.WriteLine(i);
                }
            }

        }
    }
}
