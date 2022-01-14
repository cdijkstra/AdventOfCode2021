using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Day18.Input
{
    public class Snailfish
    {
        private List<string> _snailFish;
        private char[] _currentSnailFish;
        public Snailfish(string fileName)
        {
            _snailFish = new List<string>(File.ReadAllLines($"../../../Input/{fileName}"));
        }

        public void FindSolution()
        {
            _currentSnailFish = _snailFish.First().ToCharArray();
            while (!ExplodesDone())
            {
                Console.Write("Try again exploding");
            }
            while (!ReplacePairsDone())
            {
                Console.Write("Try again replacing");
            }
        }

        private bool ExplodesDone()
        {
            char[] charsSoFar = {_currentSnailFish[0]};
            
            foreach (var idx in Enumerable.Range(1, _currentSnailFish.Length - 1))
            {
                charsSoFar = charsSoFar.Append(_currentSnailFish[idx]).ToArray();
                if (charsSoFar.Count(x => x == '[') - charsSoFar.Count(x => x == ']') > 3)
                {
                    // Replace left side if applicable
                    int leftInt;
                    var successParseLeft = int.TryParse(_currentSnailFish[idx + 1].ToString(), out leftInt);
                    if (!successParseLeft)
                        continue;
                    var down = idx - 1;
                    bool replaceLeft = false;

                    bool extraAddedLeft = false;
                    
                    while (down != 0 && !replaceLeft)
                    {
                        if (char.IsDigit(_currentSnailFish[down]))
                        {
                            var replaceInt = int.Parse(_currentSnailFish[down].ToString());
                            if (replaceInt + leftInt <= 9)
                            {
                                _currentSnailFish[down] = (char) ('0' + replaceInt + leftInt);
                            }
                            else
                            {
                                var twoChars = (replaceInt + leftInt).ToString().ToCharArray();
                                _currentSnailFish[down] = twoChars[0];
                                _currentSnailFish[down + 1] = twoChars[1];

                                extraAddedLeft = true;
                            }
                            
                            replaceLeft = true;
                        }

                        down--;
                    }
                    
                    // Replace right side if applicable
                    int rightInt;
                    var successParseRight = int.TryParse(_currentSnailFish[idx + 1].ToString(), out rightInt);
                    if (!successParseRight)
                        continue;
                    var up = idx + 3 + Convert.ToInt32(extraAddedLeft);
                    bool replaceRight = false;
                    bool addedExtraRight = false;
                    while (up != _currentSnailFish.Length && !replaceRight)
                    {
                        if (char.IsDigit(_currentSnailFish[up]))
                        {
                            var replaceInt = int.Parse(_currentSnailFish[up].ToString());
                            if (replaceInt + rightInt < 9)
                            {
                                _currentSnailFish[up] = (char) ('0' + replaceInt + rightInt);
                            }
                            else
                            {
                                var twoChars = (replaceInt + rightInt).ToString().ToCharArray();
                                _currentSnailFish[up] = twoChars[0];
                                _currentSnailFish[up + 1] = twoChars[1];
                                addedExtraRight = true;
                            }

                            replaceRight = true;
                        }

                        up++;
                    }
                    
                    _currentSnailFish = new string(_currentSnailFish).Remove(idx + 4 + Convert.ToInt32(extraAddedLeft) + Convert.ToInt32(addedExtraRight), 1).ToCharArray();
                    _currentSnailFish = new string(_currentSnailFish).Remove(idx + Convert.ToInt32(extraAddedLeft), 1).ToCharArray();
                    // Start function again
                    return false;
                }
            }

            // No more explosions
            return true;
        }

        private bool ReplacePairsDone()
        {
            var firstSnailFish = _snailFish.First().ToCharArray();
            foreach (var idx in Enumerable.Range(0, firstSnailFish.Length))
            {
                if (!char.IsDigit(firstSnailFish[idx]) || !char.IsDigit(firstSnailFish[idx + 1])) continue;
                
                var intToBeReplaced = int.Parse(firstSnailFish[idx].ToString() + firstSnailFish[idx + 1]);
                var down = Math.Floor((double) intToBeReplaced / 2).ToString(CultureInfo.InvariantCulture).ToCharArray().First();
                var up = Math.Ceiling((double) intToBeReplaced / 2).ToString(CultureInfo.InvariantCulture).ToCharArray().First();
                firstSnailFish[idx] = '[';
                firstSnailFish[idx + 1] = down;
                firstSnailFish[idx + 2] = ',';
                firstSnailFish[idx + 3] = up;
                firstSnailFish[idx + 4] = ']';

                return false;
            }

            return true;
        }
    }
}