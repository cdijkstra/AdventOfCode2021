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
        private Queue<(int x, int y)> _queue = new();
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
            _queue.Enqueue((0, 0));
            _riskLevelGrid[0][0].Risk = 0;

            while (_queue.Any())
            {
                var (x, y) = _queue.Dequeue();
                if (x > 0)
                {
                    ContinueThroughMazeIfRequired(x - 1, y, _riskLevelGrid[x][y].Risk);
                }
                if (y > 0)
                {
                    ContinueThroughMazeIfRequired(x, y - 1, _riskLevelGrid[x][y].Risk);
                }

                if (x < _xMax)
                {
                    ContinueThroughMazeIfRequired(x + 1, y, _riskLevelGrid[x][y].Risk);
                }
                if (y < _xMax)
                {
                    ContinueThroughMazeIfRequired(x, y + 1, _riskLevelGrid[x][y].Risk);
                }
            }
            
            Console.WriteLine($"The cumulative risk level is {_riskLevelGrid[_xMax][_yMax].Risk}");
        }

        private void ContinueThroughMazeIfRequired(int newX, int newY, long currentRiskLevel)
        {
            if (_riskLevelGrid[newX][newY].Risk <= _riskLevelGrid[newX][newY].Value + currentRiskLevel) 
                return;
            
            _riskLevelGrid[newX][newY].Risk = _riskLevelGrid[newX][newY].Value + currentRiskLevel;
            _queue.Enqueue((newX, newY));
        }
    }
}
