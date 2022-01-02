using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14
{
    class Polymer
    {
        private string _polymer;
        private Dictionary<string, char> _tuples = new();
        
        public Polymer(string fileName)
        {
            var file = new List<string>(File.ReadAllLines($"../../../Input/{fileName}"));
            _polymer = file.First();

            foreach (var line in file.Where(l => l.Contains("->")))
            {
                var input = line.Split("->");
                _tuples.Add(input[0].Trim(), char.Parse(input[1].Trim()));
            }
        }

        public void CalculateSolution(int numberOfRepeats = 1)
        {
            for (int repeat = 1; repeat <= numberOfRepeats; repeat++)
            {
                List<(int index, char newValue)> newInserts = new();
                for (var idx = 0; idx != _polymer.Length - 1; idx++)
                {
                    var pair = (_polymer.ElementAt(idx) + _polymer.ElementAt(idx + 1).ToString());
                    if (_tuples.ContainsKey(pair))
                    {
                        newInserts.Add((idx + 1, _tuples[pair]));
                    }
                }

                foreach (var insert in newInserts.OrderByDescending(x => x.index))
                {
                    _polymer = _polymer.Insert(insert.index, insert.newValue.ToString());
                }
            }

            var biggestQuantity = _polymer.GroupBy(ch => ch).OrderByDescending(ch => ch.Count()).First().Count();
            var smallestQuantity = _polymer.GroupBy(ch => ch).OrderBy(ch => ch.Count()).First().Count();

            Console.Write(biggestQuantity - smallestQuantity);
        }
    }
}
