namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var cave = new Cave("data.txt");
            cave.FindSolutionForSmallCavesOnce();
            cave.FindSolutionForSmallCavesTwice();
        }
    }
}
