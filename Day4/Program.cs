using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var bingo = new Bingo("data.txt");
            bingo.PlayGame();
        }
    }
}
