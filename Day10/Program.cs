using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var pointsForErrors = 0;
            List<Int64> pointsForIncompleteSentences = new List<Int64>();
            foreach (string line in File.ReadLines("../../../Input/data.txt"))
            {
                List<char> expectedClosingChars = new List<char>();
                bool addError = true;
                foreach (char ch in line)
                {
                    switch (ch)
                    {
                        // Opening characters
                        case '(':
                            expectedClosingChars.Add(')');
                            break;
                        case '[':
                            expectedClosingChars.Add(']');
                            break;
                        case '{':
                            expectedClosingChars.Add('}');
                            break;
                        case '<':
                            expectedClosingChars.Add('>');
                            break;
                        // Closing characters
                        case ')':
                            if (expectedClosingChars.Last() == ')')
                            {
                                expectedClosingChars.RemoveAt(expectedClosingChars.Count - 1);
                            }
                            else
                            {
                                pointsForErrors += addError ? 3 : 0;
                                addError = false;
                            }
                            break;
                        case ']':
                            if (expectedClosingChars.Last() == ']')
                            {
                                expectedClosingChars.RemoveAt(expectedClosingChars.Count - 1);
                            }
                            else
                            {
                                pointsForErrors += addError ? 57 : 0;
                                addError = false;
                            }
                            break;
                        case '}':
                            if (expectedClosingChars.Last() == '}')
                            {
                                expectedClosingChars.RemoveAt(expectedClosingChars.Count - 1);
                            }
                            else
                            {
                                pointsForErrors += addError ? 1197 : 0;
                                addError = false;
                            }
                            break;
                        case '>':
                            if (expectedClosingChars.Last() == '>')
                            {
                                expectedClosingChars.RemoveAt(expectedClosingChars.Count - 1);
                            }
                            else
                            {
                                pointsForErrors += addError ? 25137 : 0;
                                addError = false;
                            }
                            break;
                        default:
                            throw new ArgumentException("Unexpected argument");
                    }
                }

                if (!addError || !expectedClosingChars.Any()) continue;
                
                Int64 points = 0;
                for (var idx = expectedClosingChars.Count - 1; idx >= 0; idx--)
                {
                    points *= 5;
                    switch (expectedClosingChars.ElementAt(idx))
                    {
                        case ')':
                            points += 1;
                            break;
                        case ']':
                            points += 2;
                            break;
                        case '}':
                            points += 3;
                            break;
                        case '>':
                            points += 4;
                            break;
                    }
                        
                }
                pointsForIncompleteSentences.Add(points);
            }

            Console.WriteLine($"Total score for corrupt sentences: {pointsForErrors}");
            var middlePointIncompleteSentences = pointsForIncompleteSentences.OrderBy(x => x)
                .ElementAt((pointsForIncompleteSentences.Count - 1) / 2);
            
            Console.WriteLine($"Middle score for incomplete sentences: {middlePointIncompleteSentences}");
        }
    }
}
