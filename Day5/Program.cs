using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText("dummydata.txt");
            var lines = text.Split(Environment.NewLine);

            List<int> xs = new List<int>();
            List<int> ys = new List<int>();
            foreach (var line in lines)
            {
                var test = line.Split(" -> ");
                foreach (var number in test)
                {
                    var numbers = number.Split(",");
                    xs.Add(int.Parse(numbers[0]));
                    ys.Add(int.Parse(numbers[1]));
                }
            }

            List<List<int>> grid = new List<List<int>>();
            for (int idx = 0; idx != xs.Max(); idx++)
            {
                grid.Add(new List<int>());
            }
            
            for (int index = 0; index < xs.Count - 1; index += 2)
            {
                if (xs[index] == xs[index + 1])
                {
                    if (ys[index + 1] > ys[index])
                    {
                        for (int y = ys[index]; y < ys[index + 1]; index++)
                        {
                            grid[xs[index]][y] += 1;
                        }
                    }
                    else
                    {
                        for (int y = ys[index]; y > ys[index + 1]; index--)
                        {
                            grid[xs[index]][y] += 1;
                        }
                    }
                }
                else if (ys[index] == ys[index + 1])
                {
                    if (xs[index + 1] > xs[index])
                    {
                        for (int x = xs[index]; x < ys[index + 1]; index++)
                        {
                            Console.WriteLine(ys[index]);
                            Console.WriteLine(grid[x][ys[index]]);
                        }
                    }
                    else
                    {
                        for (int x = xs[index]; x > ys[index + 1]; index--)
                        {
                            Console.WriteLine(x);
                            grid[x][ys[index]] += 1;
                        }
                    }
                }
            }
            
        }
    }
}
