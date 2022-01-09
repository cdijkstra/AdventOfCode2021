namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            var chitonSmallMap = new Chitons("data.txt");
            chitonSmallMap.FindPathWithLowestRisk();
            
            var chitonBigMap = new Chitons("data.txt", true);
            chitonBigMap.FindPathWithLowestRisk();
        }
    }
}
