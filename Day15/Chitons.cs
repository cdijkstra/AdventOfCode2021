using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    class RiskEntry
    {
        public RiskEntry(int value)
        {
            Value = value;
            Risk = Int64.MaxValue;
        }
        
        public long Risk;
        public int Value;
    }
    
    class Chitons
    {
        private List<List<RiskEntry>> _riskLevelGrid = new();
        private Queue<(int x, int y)> _queue = new();
        private readonly int _xMaxIndex;
        private readonly int _yMaxIndex;
        
        public Chitons(string fileName, bool fullMap = false)
        {
            var file = new List<string>(File.ReadAllLines($"../../../Input/{fileName}"));
            if (fullMap == true)
            {
                for (int repeatY = 0; repeatY != 5; repeatY++)
                {
                    foreach (var line in file)
                    {
                        List<RiskEntry> newEntry = new();
                        for (int repeatX = 0; repeatX != 5; repeatX++)
                        {
                            newEntry.AddRange(line.Select(ch =>
                                new RiskEntry(int.Parse(ch.ToString()) + repeatX + repeatY > 9 ? (int.Parse(ch.ToString()) + repeatX + repeatY) % 9 : int.Parse(ch.ToString()) + repeatX + repeatY)).ToList());
                        }

                        _riskLevelGrid.Add(newEntry);
                    }
                }
            }
            else
            {
                foreach (var newEntry in file.Select(line => line.Select(ch => new RiskEntry(int.Parse(ch.ToString()))).ToList()))
                {
                    _riskLevelGrid.Add(newEntry);
                }
            }

            _xMaxIndex = _riskLevelGrid.First().Count - 1;
            _yMaxIndex = _riskLevelGrid.Count - 1;
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

                if (x < _xMaxIndex)
                {
                    ContinueThroughMazeIfRequired(x + 1, y, _riskLevelGrid[x][y].Risk);
                }
                if (y < _xMaxIndex)
                {
                    ContinueThroughMazeIfRequired(x, y + 1, _riskLevelGrid[x][y].Risk);
                }
            }
            
            Console.WriteLine($"The cumulative risk level is {_riskLevelGrid[_xMaxIndex][_yMaxIndex].Risk}");
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
