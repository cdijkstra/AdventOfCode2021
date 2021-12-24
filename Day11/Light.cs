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
            for (var day = 1; day <= lastDay; day++)
            {
                IncreaseGridPoints();
                CalculateFlashes();
                ResetFlashedOctopi();
            }

            return _amountOfTotalLight;
        }

        private void CalculateFlashes()
        {
            for (var idx = 0; idx < _grid.Count; idx++)
            {
                for (var idy = 0; idy < _grid[idx].Count; idy++)
                {
                    if (_grid[idx][idy] <= 9) continue;

                    if (!_octopusLight.Contains((idx, idy)))
                    {
                        FlashEvent(idx, idy);
                    }
                }
            }
            
            _amountOfTotalLight += _amountOflightForDay;
        }

        private void IncreaseGridPoints()
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
        }

        private void ResetFlashedOctopi()
        {
            _octopusLight.Clear();
            foreach (var row in _grid)
            {
                for (var idy = 0; idy < row.Count; idy++)
                {
                    if (row[idy] > 9)
                    {
                        row[idy] = 0;
                    }
                }
            }
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
            ++_grid[idx][idy];
            if (_grid[idx][idy] > 9 && !_octopusLight.Contains((idx, idy)))
            {
                FlashEvent(idx, idy);
            }
        }

        private void FlashEvent(int idx, int idy)
        {
            _amountOflightForDay++;
            _octopusLight.Add((idx, idy));
            IncreaseSurroundingPoints(idx, idy);
        }
    }
}