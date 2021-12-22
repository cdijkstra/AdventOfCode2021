using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char[]> grid = new();
            foreach (string line in File.ReadLines("../../../Input/data.txt"))
            {
                grid.Add(line.ToCharArray());
            }

            var riskLevel = 0;
            
            for (int idx = 0; idx < grid.Count; idx++)
            {
                for (int idy = 0; idy < grid[idx].Length; idy++)
                {
                    List<int> surroundingPoint = new();
                    if (idx > 0)
                    {
                        surroundingPoint.Add(grid[idx - 1][idy]);
                    }
                    if (idy > 0)
                    {
                        surroundingPoint.Add(grid[idx][idy - 1]);
                    }
                    if (idx < grid.Count - 1)
                    {
                        surroundingPoint.Add(grid[idx + 1][idy]);
                    }
                    if (idy < grid[idx].Length - 1)
                    {
                        surroundingPoint.Add(grid[idx][idy + 1]);
                    }

                    if (surroundingPoint.All(x => x > grid[idx][idy]))
                    {
                        riskLevel += (int.Parse(grid[idx][idy].ToString()) + 1);
                    }
                }
            }
            
            Console.Write($"Risklevel = {riskLevel}");
        }
    }
}
