namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var cave = new Cave("dummydata.txt");
            cave.FindSolutionForSmallCavesOnce();
            cave.FindSolutionForSmallCavesTwice();
        }
    }
}
