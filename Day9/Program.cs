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
            List<int> sizesBasins = new();
            foreach (string line in File.ReadLines("../../../Input/data.txt"))
            {
                grid.Add(line.ToCharArray());
            }

            var riskLevel = 0;
            
            for (int idx = 0; idx < grid.Count; idx++)
            {
                for (int idy = 0; idy < grid[idx].Length; idy++)
                {
                    var surroundingPoint = FindSurroundingPoints(grid, idx, idy);

                    if (surroundingPoint.All(x => x > grid[idx][idy]))
                    {
                        // Low point. Calculate risklevel and determine size basin
                        riskLevel += int.Parse(grid[idx][idy].ToString()) + 1;
                        var basin = CalculateBasin(grid, idx, idy);
                        var sizeBasin = basin.Count();
                        sizesBasins.Add(sizeBasin);
                    }
                }
            }
            
            Console.WriteLine($"Risklevel = {riskLevel}");
            var threeBiggestBasins = sizesBasins.OrderByDescending(x => x).Take(3);
            var productThreeBiggestBasins = Enumerable.Aggregate(threeBiggestBasins, (x,y) => x * y);
            Console.Write($"Product of three biggest basin sizes = {productThreeBiggestBasins}");
        }

        // Recursive function
        private static IEnumerable<(int x, int y)> CalculateBasin(List<char[]> grid, int idx, int idy)
        {
            // Add new entry to basin
            List<(int x, int y)> coordinatesInBasin = new List<(int x, int y)>
            {
                (idx, idy)
            };
            
            var value = int.Parse(grid[idx][idy].ToString());
            var riskLevel = (value + 1).ToString();

            if (riskLevel == "9")
            {
                return coordinatesInBasin.Distinct();
            }

            // This is the lowest point in a basin.
            if (idx > 0)
            {
                if (grid[idx - 1][idy].ToString() == riskLevel)
                {
                    // Repeat for idx - 1, idy
                    coordinatesInBasin.AddRange(CalculateBasin(grid, idx - 1, idy));
                }
            }

            if (idy > 0)
            {
                if (grid[idx][idy - 1].ToString() == riskLevel)
                {
                    // Repeat for idx, idy - 1
                    coordinatesInBasin.AddRange(CalculateBasin(grid, idx, idy - 1));
                }
            }

            if (idx < grid.Count - 1)
            {
                if (grid[idx + 1][idy].ToString() == riskLevel)
                {
                    // Repeat for idx + 1, idy
                    coordinatesInBasin.AddRange(CalculateBasin(grid, idx + 1, idy));
                }
            }

            if (idy < grid[idx].Length - 1)
            {
                if (grid[idx][idy + 1].ToString() == riskLevel)
                {
                    // Repeat for idx, idy + 1
                    coordinatesInBasin.AddRange(CalculateBasin(grid, idx, idy + 1));
                }
            }

            return coordinatesInBasin.Distinct();
        }

        private static List<int> FindSurroundingPoints(List<char[]> grid, int idx, int idy)
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

            return surroundingPoint;
        }
    }
}
