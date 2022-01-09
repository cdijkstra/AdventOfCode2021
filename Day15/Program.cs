namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            var chitons = new Chitons("data.txt");
            chitons.FindPathWithLowestRisk();
        }
    }
}
