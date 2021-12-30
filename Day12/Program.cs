namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var cave = new Cave("data.txt");
            cave.FindSolutionForCave(true);
            cave.FindSolutionForCave(false);
        }
    }
}
