using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Day15
{
    class RiskEntry
    {
        public RiskEntry(int value, long risk)
        {
            Value = value;
            Risk = risk;
        }
        
        public long Risk = Int64.MaxValue;
        public int Value = 0;
    }
    
    class Chitons
    {
        private List<List<RiskEntry>> _riskLevelGrid = new();
        private readonly int _xMax;
        private readonly int _yMax;
        public Chitons(string fileName)
        {
            var file = new List<string>(File.ReadAllLines($"../../../Input/{fileName}"));
            foreach (var line in file)
            {
                List<RiskEntry> newEntry = line.Select(ch => new RiskEntry(int.Parse(ch.ToString()), long.MaxValue)).ToList();
                _riskLevelGrid.Add(newEntry);
            }

            _xMax = _riskLevelGrid.First().Count - 1;
            _yMax = _riskLevelGrid.Count - 1;
        }

        public void FindPathWithLowestRisk()
        {
            var queue = new Queue<(int x, int y)>();
            queue.Enqueue((0, 0));
            _riskLevelGrid[0][0].Risk = 0;

            while (queue.Any())
            {
                var (x, y) = queue.Dequeue();
                if (x != 0 && _riskLevelGrid[x - 1][y].Risk > _riskLevelGrid[x - 1][y].Value + _riskLevelGrid[x][y].Risk)
                {
                    _riskLevelGrid[x - 1][y].Risk = _riskLevelGrid[x - 1][y].Value + _riskLevelGrid[x][y].Risk;
                    queue.Enqueue((x - 1, y));
                }

                if (x != _xMax && _riskLevelGrid[x + 1][y].Risk > _riskLevelGrid[x + 1][y].Value + _riskLevelGrid[x][y].Risk)
                {
                    _riskLevelGrid[x + 1][y].Risk = _riskLevelGrid[x + 1][y].Value + _riskLevelGrid[x][y].Risk;
                    queue.Enqueue((x + 1, y));
                }
            
                if (y != 0 && _riskLevelGrid[x][y - 1].Risk > _riskLevelGrid[x][y - 1].Value + _riskLevelGrid[x][y].Risk)
                {
                    _riskLevelGrid[x][y - 1].Risk = _riskLevelGrid[x][y - 1].Value + _riskLevelGrid[x][y].Risk;
                    queue.Enqueue((x, y - 1));
                }
            
                if (y != _xMax && _riskLevelGrid[x][y + 1].Risk > _riskLevelGrid[x][y + 1].Value + _riskLevelGrid[x][y].Risk)
                {
                    _riskLevelGrid[x][y + 1].Risk = _riskLevelGrid[x][y + 1].Value + _riskLevelGrid[x][y].Risk;
                    queue.Enqueue((x, y + 1));
                }
            }
            
            Console.WriteLine(_riskLevelGrid[_xMax][_yMax].Risk);
        }
    }
}
