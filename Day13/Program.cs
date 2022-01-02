using System;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var fold = new Fold("data.txt");
            fold.UnfoldOnce();
            fold.UnfoldAll();
        }
    }
}
