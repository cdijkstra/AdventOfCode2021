using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    class Fold
    {
        private List<List<bool>> _grid = new();
        private List<(int x, int y)> _tuples = new();
        private List<(string axis, int num)> _folds = new();
        private int _xMax;
        private int _yMax;
        
        public Fold(string fileName)
        {
            var file = new List<string>(File.ReadAllLines($"../../../Input/{fileName}"));
            foreach (var line in file.Where(l => l != string.Empty))
            {
                if (!line.StartsWith("fold"))
                {
                    var input = Array.ConvertAll(line.Split(','), int.Parse);
                    _tuples.Add((input[0], input[1]));
                }
                else
                {
                    var input = line.Split(' ')[2].Split('=');
                    _folds.Add((input[0], int.Parse(input[1])));
                }
            }

            _xMax = _tuples.Select(tup => tup.y).Max() + 1;
            _yMax = _tuples.Select(tup => tup.x).Max() + 1;

            foreach (var idx in Enumerable.Range(0, _yMax))
            {
                List<bool> entry = Enumerable.Repeat(false, _xMax).ToList();
                _grid.Add(entry);
            }
            
            foreach (var entry in _tuples)
            {
                _grid[entry.x][entry.y] = true;
            }
        }

        public void UnfoldOnce()
        {
            FoldPaper(_folds.First());
            ShowSolution();
        }
        
        public void UnfoldAll()
        {
            foreach (var fold in _folds)
            {
                FoldPaper(fold);
            }

            ShowSolution();
        }

        private void FoldPaper((string axis, int num) fold)
        {
            switch (fold.axis)
            {
                // Assumption for x,y: always fold in middle
                case "x":
                    foreach (var idx in Enumerable.Range(1, (_grid.Count - 1) / 2))
                    {
                        foreach (var idy in Enumerable.Range(0, _grid.ElementAt(idx).Count))
                        {
                            _grid[(_grid.Count - 1) / 2 - idx][idy] |= _grid[(_grid.Count - 1) / 2 + idx][idy];
                        }
                    }

                    _grid.RemoveRange((_grid.Count - 1) / 2, (_grid.Count + 1) / 2);
                    break;

                case "y":
                    foreach (var row in _grid)
                    {
                        foreach (var idy in Enumerable.Range(1, (row.Count - 1) / 2))
                        {
                            row[(row.Count - 1) / 2 - idy] |= row[(row.Count - 1) / 2 + idy];
                        }

                        row.RemoveRange((row.Count - 1) / 2, (row.Count + 1) / 2);
                    }

                    break;

                default:
                    throw new ArgumentException("Unexpected entry");
            }
        }

        private void ShowSolution()
        {
            var numberOfTrue = _grid.SelectMany(x => x.Where(x => x)).Count();
            Console.WriteLine($"Number of digits found = {numberOfTrue}.");
            
            Console.WriteLine("The full solution looks like:");
            foreach (var column in Enumerable.Range(0, _grid.First().Count))
            {
                foreach (var row in Enumerable.Range(0, _grid.Count))
                {
                    Console.Write(Convert.ToInt32(_grid[row][column]));
                    if ((row + 1) % 5 == 0)
                    {
                        Console.Write(' ');
                    }
                }
                Console.Write('\n');
            }
        }
    }
}
