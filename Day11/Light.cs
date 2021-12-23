using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    public class Light
    {
        private List<List<int>> _grid = new();
        private List<(int x, int y)> _octopusLight = new();
        private int _amountOflightForDay = 0;
        private int _amountOfTotalLight = 0;

        public Light(string textFile)
        {
            foreach (string line in File.ReadLines($"../../../Input/{textFile}"))
            {
                List<int> intList = new();
                foreach (char ch in line)
                {
                    intList.Add(ch - '0');
                }

                _grid.Add(intList);
            }
        }

        public int CalculateForDay(int lastDay)
        {
            for (int day = 1; day <= lastDay; day++)
            {
                _amountOflightForDay = 0;
                // Increase all points in grid
                for (int idx = 0; idx < _grid.Count; idx++)
                {
                    for (int idy = 0; idy < _grid[idx].Count; idy++)
                    {
                        _grid[idx][idy]++;
                    }
                }
                
                for (int idx = 0; idx < _grid.Count; idx++)
                {
                    for (int idy = 0; idy < _grid[idx].Count; idy++)
                    {
                        if (_grid[idx][idy] > 9)
                        {
                            if (!_octopusLight.Contains((idx, idy)))
                            {
                                _amountOflightForDay++;
                                _octopusLight.Add((idx, idy));
                                Console.Write($"Octopus {idx},{idy} is shining");
                            
                            }
                            
                            IncreaseSurroundingPoints(idx, idy);
                        }
                    }
                }
                
                _octopusLight.Clear();
                for (int idx = 0; idx < _grid.Count; idx++)
                {
                    for (int idy = 0; idy < _grid[idx].Count; idy++)
                    {
                        if (_grid[idx][idy] > 9)
                        {
                            _grid[idx][idy] = 0;
                        }
                    }
                }
                
                _amountOfTotalLight += _amountOflightForDay;
            }

            return _amountOfTotalLight;
        }
        
        private void IncreaseSurroundingPoints(int idx, int idy)
        {
            if (idy > 0)
            {
                IncreaseAndSeeIfFlashes(idx, idy - 1);
            }

            if (idy < _grid[idx].Count - 1)
            {
                IncreaseAndSeeIfFlashes(idx, idy + 1);
            }
            
            if (idx > 0)
            {
                IncreaseAndSeeIfFlashes(idx - 1, idy);
                if (idy > 0)
                {
                    IncreaseAndSeeIfFlashes(idx - 1, idy - 1);
                }
                if (idy < _grid.Count - 1)
                {
                    IncreaseAndSeeIfFlashes(idx - 1, idy + 1);
                }
            }

            if (idx < _grid.Count - 1)
            {
                IncreaseAndSeeIfFlashes(idx + 1, idy);
                if (idy > 0)
                {
                    IncreaseAndSeeIfFlashes(idx + 1, idy - 1);
                }
                if (idy < _grid.Count - 1)
                {
                    IncreaseAndSeeIfFlashes(idx + 1, idy + 1);
                }
            }
        }

        private void IncreaseAndSeeIfFlashes(int idx, int idy)
        {
            if (++_grid[idx][idy] > 9 && !_octopusLight.Contains((idx, idy)))
            {
                _amountOflightForDay++;
                _octopusLight.Add((idx, idy));
                Console.Write($"Octopus {idx},{idy} is shining");
            }
        }
    }
}