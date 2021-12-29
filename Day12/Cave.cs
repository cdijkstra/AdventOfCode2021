using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    public class Cave
    {
        private readonly Dictionary<string, List<string>> _caveConnections = new();
        private int _totalCount = 0;

        public Cave(string text)
        {
            foreach (string line in File.ReadLines($"../../../Input/{text}"))
            {
                var newLine = line.Split("-");
                var key = newLine[0];
                var value = newLine[1];

                if (_caveConnections.ContainsKey(key))
                {
                    _caveConnections[key].Add(value);
                }
                else
                {
                    _caveConnections.Add(key, new List<string> {value});
                }
                
                if (_caveConnections.ContainsKey(value))
                {
                    _caveConnections[value].Add(key);
                }
                else
                {
                    _caveConnections.Add(value, new List<string> {key});
                }
            }
        }

        public void FindSolution()
        {
            var visited = new List<string>();
            var beginPoint = "start";
            GoThroughCave(beginPoint, visited);
            Console.Write("Number of ways through the cave = " + _totalCount);
            _totalCount = 0;
        }

        private void GoThroughCave(string entryPoint, ICollection<string> visited, string points = "")
        {
            points += $", {entryPoint}";
            if (entryPoint.Any(char.IsLower)) // Capital caves are excluded
            {
                visited.Add(entryPoint);
            }
            
            foreach (var newPoint in _caveConnections[entryPoint].Where(entry => !visited.Contains(entry)))
            {
                if (newPoint == "end")
                {
                    _totalCount++;
                    // Console.WriteLine(points);
                }
                else
                {
                    GoThroughCave(newPoint, visited, points);
                }
            }

            visited.Remove(entryPoint);
        }
    }
}