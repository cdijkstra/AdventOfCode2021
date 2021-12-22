using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
    public class Grid
    {
        public string[] _lines;
        public List<int> _xs = new List<int>();
        public List<int> _ys = new List<int>();
        public List<List<int>> _grid = new List<List<int>>();

        public Grid(string file)
        {
            _lines = File.ReadAllText(Path.Combine("../../../", file)).Split(Environment.NewLine);
        }

        public void Initialize()
        {
            foreach (var line in _lines)
            {
                var test = line.Split(" -> ");
                foreach (var number in test)
                {
                    var numbers = number.Split(",");
                    _xs.Add(int.Parse(numbers[0]));
                    _ys.Add(int.Parse(numbers[1]));
                }
            }
        }

        public void CreateGrid()
        {
            // xs.Max x ys.Max grid filled with zeros
            for (int idx = 0; idx != _xs.Max() + 1; idx++)
            {
                var subList = new List<int>();
                for (int idy = 0; idy != _ys.Max() + 1; idy++)
                {
                    subList.Add(0);
                }
                _grid.Add(subList);
            }
        }

        public void ReadLines()
        {
            // xs[even] and ys[even] are new commands, xs[odd] and ys[odd] are what they map to.
            for (int index = 0; index < _xs.Count - 1; index += 2)
            {
                if (_xs[index] == _xs[index + 1])
                {
                    var yMin = Math.Min(_ys[index], _ys[index + 1]);
                    var yMax = Math.Max(_ys[index], _ys[index + 1]);
                    foreach(var idy in Enumerable.Range(yMin, yMax - yMin + 1))
                    {
                        _grid[_xs[index]][idy] += 1;
                    }
                }
                else if (_ys[index] == _ys[index + 1])
                {
                    var xMin = Math.Min(_xs[index], _xs[index + 1]);
                    var xMax = Math.Max(_xs[index], _xs[index + 1]);
                    foreach(var idx in Enumerable.Range(xMin, xMax - xMin + 1))
                    {
                        _grid[idx][_ys[index]] += 1;
                    }
                }
                // When going from xMin to xMax there are two options; +45% angle
                else if (_ys[index + 1] - _ys[index] == _xs[index + 1] - _xs[index])
                {
                    var xMin = Math.Min(_xs[index], _xs[index + 1]);
                    var xMax = Math.Max(_xs[index], _xs[index + 1]);
                    var yMin = Math.Min(_ys[index], _ys[index + 1]);

                    for (int diag = 0; diag < xMax - xMin + 1; diag++)
                    {
                        _grid[xMin + diag][yMin + diag] += 1;
                    }
                }
                // And -45 angle
                else if (_ys[index + 1] - _ys[index] == _xs[index] - _xs[index + 1])
                {
                    var xMin = Math.Min(_xs[index], _xs[index + 1]);
                    var xMax = Math.Max(_xs[index], _xs[index + 1]);
                    var yMax = Math.Max(_ys[index], _ys[index + 1]);

                    for (int diag = 0; diag < xMax - xMin + 1; diag++)
                    {
                        _grid[xMin + diag][yMax - diag] += 1;
                    }
                }
            }
        }

        public int ShowSolution()
        {
            var numsBiggerThanOne = _grid.SelectMany(row => row.Where(num => num > 1));
            return numsBiggerThanOne.Count();
        }
    }
}