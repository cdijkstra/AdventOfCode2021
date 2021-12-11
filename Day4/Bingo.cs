using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    public class Bingo
    {
        private List<List<int>> _boards = new();
        private List<int> _board = new();
        private string[] _lines;
        private string[] _numbers;
        private readonly List<int> _numbersUntilNow = new List<int>();
        private Dictionary<List<int>, bool> _wonBoards = new();

        private bool _end = false;
        private int _numberOfWonBoards = 0;

        public Bingo(string textFile)
        {
            string text = File.ReadAllText(textFile);
            _lines = text.Split(Environment.NewLine);
            _numbers = _lines[0].Split(",");
        }

        public void PlayGame()
        {
            InitializeBoards();
            
            while (_end == false)
            {
                foreach (var number in _numbers)
                {
                    int num = int.Parse(number);
                    _numbersUntilNow.Add(num);
                    foreach (var boardToCheck in _boards)
                    {
                        if (_wonBoards[boardToCheck])
                        {
                            continue;
                        }
                        
                        if (CheckIfColumnWins(boardToCheck) || CheckIfRowsWins(boardToCheck))
                        {
                            if (_numberOfWonBoards == _boards.Count)
                            {
                                var result = boardToCheck.Where(num => !_numbersUntilNow.Contains(num)).Sum();
                                Console.WriteLine($"Result = {result * int.Parse(number)}");
                            }
                        }
                    }
                }

                _end = true;
            }
        }
        
        private void InitializeBoards()
        {
            for (var li = 2; li < _lines.Length; li++)
            {
                if (_lines[li] == "")
                {
                    _boards.Add(_board);
                    _board = new List<int>();
                }
                else
                {
                    var readline = Regex.Split(_lines[li], @"[ ]+");
                    _board.AddRange(from number in readline where number != "" select int.Parse(number));
                }
            }
            
            _boards.Add(_board);

            foreach (var board in _boards)
            {
                _wonBoards.Add(board, false);
            }
        }

        private bool CheckIfRowsWins(List<int> boardToCheck)
        {
            // Check rows
            for (var idx = 0; idx < _board.Count; idx += 5)
            {
                List<int> numbersToCheck = new List<int>();
                for (var idy = 0; idy < 5; idy++)
                {
                    numbersToCheck.Add(boardToCheck[idx + idy]);
                }

                if (CheckIfNumberCombinationWins(numbersToCheck))
                {
                    _wonBoards[boardToCheck] = true;
                    _numberOfWonBoards++;
                    return true;
                }
            }

            return false;
        }

        private bool CheckIfColumnWins(List<int> boardToCheck)
        {
            // Check columns
            for (var idx = 0; idx != 5; idx++)
            {
                List<int> numbersToCheck = new List<int>();
                for (var idy = 0; idy < _board.Count; idy += 5)
                {
                    numbersToCheck.Add(boardToCheck[idx + idy]);
                }

                if (CheckIfNumberCombinationWins(numbersToCheck))
                {
                    _wonBoards[boardToCheck] = true;
                    _numberOfWonBoards++;
                    return true;
                }
            }

            return false;
        }

        private bool CheckIfNumberCombinationWins(List<int> numbersToCheck)
        {
            return numbersToCheck.All(x => _numbersUntilNow.Any(y => y == x));
        }
    }
}