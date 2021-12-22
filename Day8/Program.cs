using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var sum = 0;
            foreach (string line in File.ReadLines("../../../data.txt"))
            {
                Dictionary<string, int> stringToInt = new Dictionary<string, int>();
                var bitsBeforeDenominator = line.Split("|")[0].Split(" ");
                
                stringToInt.Add(SortString(bitsBeforeDenominator.Single(bits => bits.Length == 2)), 1);
                stringToInt.Add(SortString(bitsBeforeDenominator.Single(bits => bits.Length == 4)), 4);
                stringToInt.Add(SortString(bitsBeforeDenominator.Single(bits => bits.Length == 3)), 7);
                stringToInt.Add(SortString(bitsBeforeDenominator.Single(bits => bits.Length == 7)), 8);

                // Could be 0,6,9
                var lengthSixStrings = bitsBeforeDenominator.Where(bits => bits.Length == 6);
                foreach (var lengthSixString in lengthSixStrings)
                {
                    if (stringToInt.FirstOrDefault(number => number.Value == 4).Key.All(letter => lengthSixString.Contains(letter)))
                    {
                        stringToInt.Add(SortString(lengthSixString), 9);
                    }
                    else if (stringToInt.FirstOrDefault(number => number.Value == 1).Key.All(letter => lengthSixString.Contains(letter)))
                    {
                        stringToInt.Add(SortString(lengthSixString), 0);
                    }
                    else
                    {
                        stringToInt.Add(SortString(lengthSixString), 6);
                    }
                }
                
                //Could be 2,3,5
                var lengthFiveStrings = bitsBeforeDenominator.Where(bits => bits.Length == 5);
                foreach (var lengthFiveString in lengthFiveStrings)
                {
                    if (stringToInt.FirstOrDefault(number => number.Value == 1).Key.All(letter => lengthFiveString.Contains(letter)))
                    {
                        stringToInt.Add(SortString(lengthFiveString), 3);
                    }
                    // Figuring out which one is 2 and 5 is hardest.
                    else
                    {
                        // Check overlap with 9
                        var missingChars = stringToInt.FirstOrDefault(number => number.Value == 9).Key
                            .Count(ch => !lengthFiveString.Contains(ch));
                        switch (missingChars)
                        {
                            case 1:
                                stringToInt.Add(SortString(lengthFiveString), 5);
                                break;
                            case 2:
                                stringToInt.Add(SortString(lengthFiveString), 2);
                                break;
                        }
                    }
                }

                var bitsAfterDenominator = line.Split("|")[1].Split(" ");
                var sb = new System.Text.StringBuilder();
                foreach (var bits in bitsAfterDenominator.Where(x => x.Length > 1))
                {
                    sb.Append(stringToInt[SortString(bits)].ToString());
                }

                var num = int.Parse(sb.ToString());
                sum += num;
            }  
            
            Console.Write($"Sum = {sum}");
        }
        
        static string SortString(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }
    }
}
