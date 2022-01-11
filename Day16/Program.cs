namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            Packets packets = new("data.txt");
            packets.DecomposePacket();
        }
    }
}
