using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Day18.Input
{
    public class Snailfish
    {
        private List<string> _snailFish;
        private string _currentReadFish;
        private string _nextReadSnailFish;

        private bool _explodesDone = false;
        private bool _splitsDone = false;
        
        private string _currentSnailFish;
        public Snailfish(string fileName)
        {
            _snailFish = new List<string>(File.ReadAllLines($"../../../Input/{fileName}"));
        }

        public void FindSolution()
        {
            _currentReadFish = _snailFish.ElementAt(0);
            _nextReadSnailFish = _snailFish.ElementAt(1);
            
            AddSnailFish();
            
            Console.WriteLine($"Adding up resulting in {_currentSnailFish}");
            
            Console.WriteLine("Start exploding");

            while(!_explodesDone || !_splitsDone)
            {
                while (!_explodesDone)
                {
                    Exploding();
                }
                Splitting();
            }
        }

        private void AddSnailFish()
        {
            var strBuilder = new StringBuilder(_currentReadFish);
            strBuilder.Insert(0, '[');
            strBuilder.Append($",{_nextReadSnailFish}]");
            _currentSnailFish = strBuilder.ToString();
        }
        
        private void Exploding()
        {
            Console.WriteLine("Exploding");
            
            char[] charsSoFar = {_currentSnailFish[0]};
            
            foreach (var idx in Enumerable.Range(1, _currentSnailFish.Length - 1))
            {
                charsSoFar = charsSoFar.Append(_currentSnailFish[idx]).ToArray();
                if (charsSoFar.Count(x => x == '[') - charsSoFar.Count(x => x == ']') > 4)
                {
                    // Replace left side if applicable
                    int leftInt;
                    int leftIntIndex = idx + 1;
                    var successParseLeft = int.TryParse(_currentSnailFish[leftIntIndex].ToString(), out leftInt);
                    if (!successParseLeft)
                        continue;
                    var down = leftIntIndex - 1;
                    bool replaceLeft = false;

                    bool extraAddedLeft = false;
                    
                    while (down != 0 && replaceLeft == false)
                    {
                        if (char.IsDigit(_currentSnailFish[down]))
                        {
                            var strBuilder = new StringBuilder(_currentSnailFish);
                            
                            var replaceInt = int.Parse(_currentSnailFish[down].ToString());
                            if (replaceInt + leftInt <= 9)
                            {
                                strBuilder.Remove(down, 1);
                                strBuilder.Insert(down, (char) ('0' + replaceInt + leftInt));
                            }
                            else
                            {
                                var twoChars = (replaceInt + leftInt).ToString().ToCharArray();
                                
                                strBuilder.Remove(down, 1);
                                strBuilder.Insert(down, twoChars[0]);
                                strBuilder.Insert(down + 1, twoChars[1]);

                                extraAddedLeft = true;
                            }
                            
                            _currentSnailFish = strBuilder.ToString();
                            replaceLeft = true;
                        }

                        down--;
                    }
                    
                    // Replace right side if applicable
                    int rightInt;
                    int rightIntIndex = idx + 3 + Convert.ToInt32(extraAddedLeft); // Compared to position of '['
                    var successParseRight = int.TryParse(_currentSnailFish[rightIntIndex].ToString(), out rightInt);
                    if (!successParseRight)
                    {
                        continue;
                    }
                    
                    var up = rightIntIndex + 1;
                    bool replaceRight = false;
                    while (up != _currentSnailFish.Length && replaceRight == false)
                    {
                        if (char.IsDigit(_currentSnailFish[up]))
                        {
                            var strBuilder = new StringBuilder(_currentSnailFish);
                            var replaceInt = int.Parse(_currentSnailFish[up].ToString());
                            
                            strBuilder.Remove(up, 1);
                            if (replaceInt + rightInt <= 9)
                            {
                                strBuilder.Insert(up, (char) ('0' + replaceInt + rightInt));
                            }
                            else
                            {
                                var twoChars = (replaceInt + rightInt).ToString().ToCharArray();
                                strBuilder.Insert(up, $"{twoChars[0]}{twoChars[1]}");
                            }

                            _currentSnailFish = strBuilder.ToString();
                            replaceRight = true;
                        }

                        up++;
                    }
                    
                    var newStrBuilder = new StringBuilder(_currentSnailFish);
                    newStrBuilder.Remove(rightIntIndex - 3, 5); // [A,B] ,
                    newStrBuilder.Insert(rightIntIndex - 3, "0");
                    _currentSnailFish = newStrBuilder.ToString();
                    // Start function again
                    Console.WriteLine(_currentSnailFish);
                    
                    _explodesDone = false;
                    return;
                }
            }

            // No more explosions
            Console.WriteLine("Returning true");
            _explodesDone = true;
        }

        private void Splitting()
        {
            Console.WriteLine("Splitting");
            
            foreach (var idx in Enumerable.Range(0, _currentSnailFish.Length))
            {
                if (!char.IsDigit(_currentSnailFish[idx]) || !char.IsDigit(_currentSnailFish[idx + 1])) continue;
                
                var intToBeReplaced = int.Parse(_currentSnailFish[idx].ToString() + _currentSnailFish[idx + 1]);
                var down = Math.Floor((double) intToBeReplaced / 2).ToString(CultureInfo.InvariantCulture).ToCharArray().First();
                var up = Math.Ceiling((double) intToBeReplaced / 2).ToString(CultureInfo.InvariantCulture).ToCharArray().First();
                
                var strBuilder = new StringBuilder(_currentSnailFish);
                strBuilder.Remove(idx, 2);
                strBuilder.Insert(idx, $"[{down},{up}]");
                _currentSnailFish = strBuilder.ToString();
                Console.WriteLine(_currentSnailFish);

                _explodesDone = false; // Start exploding again
                _splitsDone = false;
            }
            
            _splitsDone = true;
        }
    }
}