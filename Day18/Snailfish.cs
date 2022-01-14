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
        private string _currentSnailFish;
        public Snailfish(string fileName)
        {
            _snailFish = new List<string>(File.ReadAllLines($"../../../Input/{fileName}"));
        }

        public void FindSolution()
        {
            _currentSnailFish = _snailFish.First();
            while (!ExplodesDone())
            {
                Console.WriteLine("Try again exploding");
            }
            while (!ReplacePairsDone())
            {
                Console.WriteLine("Try again replacing");
            }

            foreach (var test in _currentSnailFish)
            {
                Console.Write(test);
            }
        }

        private bool ExplodesDone()
        {
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
                    int rightIntIndex = idx + 3 + Convert.ToInt32(extraAddedLeft);
                    var successParseRight = int.TryParse(_currentSnailFish[rightIntIndex].ToString(), out rightInt);
                    if (!successParseRight)
                        continue;
                    var up = rightIntIndex + 1;
                    bool replaceRight = false;
                    bool addedExtraRight = false;
                    while (up != _currentSnailFish.Length && replaceRight == false)
                    {
                        if (char.IsDigit(_currentSnailFish[up]))
                        {
                            var strBuilder = new StringBuilder(_currentSnailFish);
                            var replaceInt = int.Parse(_currentSnailFish[up].ToString());
                            
                            if (replaceInt + rightInt < 9)
                            {
                                strBuilder.Remove(up, 1);
                                strBuilder.Insert(up, (char) ('0' + replaceInt + rightInt));
                            }
                            else
                            {
                                var twoChars = (replaceInt + rightInt).ToString().ToCharArray();
                                
                                strBuilder.Remove(up, 1);
                                strBuilder.Insert(up, twoChars[0]);
                                strBuilder.Insert(up + 1, twoChars[1]);
                            }

                            _currentSnailFish = strBuilder.ToString();
                            replaceRight = true;
                        }

                        up++;
                    }
                    
                    var newStrBuilder = new StringBuilder(_currentSnailFish);
                    newStrBuilder.Remove(rightIntIndex + 2, 1); // ,
                    newStrBuilder.Remove(rightIntIndex + 1, 1); // ]
                    newStrBuilder.Remove(rightIntIndex, 1); // [0-9]
                    
                    newStrBuilder.Remove(rightIntIndex - 2, 1); // Replace number
                    newStrBuilder.Insert(rightIntIndex - 2, "0");
                    newStrBuilder.Remove(rightIntIndex - 3, 1);
                    _currentSnailFish = newStrBuilder.ToString();
                    // Start function again
                    return false;
                }
            }

            // No more explosions
            return true;
        }

        private bool ReplacePairsDone()
        {
            foreach (var idx in Enumerable.Range(0, _currentSnailFish.Length))
            {
                if (!char.IsDigit(_currentSnailFish[idx]) || !char.IsDigit(_currentSnailFish[idx + 1])) continue;
                
                var intToBeReplaced = int.Parse(_currentSnailFish[idx].ToString() + _currentSnailFish[idx + 1]);
                var down = Math.Floor((double) intToBeReplaced / 2).ToString(CultureInfo.InvariantCulture).ToCharArray().First();
                var up = Math.Ceiling((double) intToBeReplaced / 2).ToString(CultureInfo.InvariantCulture).ToCharArray().First();
                
                var strBuilder = new StringBuilder(_currentSnailFish);
                strBuilder.Remove(idx + 4, 1);
                strBuilder.Remove(idx + 3, 1);
                strBuilder.Remove(idx + 2, 1);
                strBuilder.Remove(idx + 1, 1);
                strBuilder.Remove(idx, 1);
                strBuilder.Insert(idx, "[");
                strBuilder.Insert(idx + 1, down);
                strBuilder.Insert(idx + 2, ",");
                strBuilder.Insert(idx + 3, up);
                strBuilder.Insert(idx + 4, "]");
                _currentSnailFish = strBuilder.ToString();
                return false;
            }

            return true;
        }
    }
}