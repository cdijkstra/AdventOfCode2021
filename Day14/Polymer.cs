using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14
{
    class Polymer
    {
        private char[] _polymer;
        private Dictionary<string, char> _tuples = new();
        
        public Polymer(string fileName)
        {
            var file = new List<string>(File.ReadAllLines($"../../../Input/{fileName}"));
            _polymer = file.First().ToCharArray();

            foreach (var line in file.Where(l => l.Contains("->")))
            {
                var input = line.Split("->");
                _tuples.Add(input[0].Trim(), char.Parse(input[1].Trim()));
            }
        }

        public void CalculateSolution()
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

            string polymer = new string(_polymer);
            foreach (var insert in newInserts.OrderByDescending(x => x.index))
            {
                polymer = polymer.Insert(insert.index, insert.newValue.ToString());
            }
            
            _polymer = polymer.ToCharArray();
            
            Console.Write(_polymer);
        }
    }
}
