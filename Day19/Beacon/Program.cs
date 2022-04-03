
namespace Beacon;
    
public class Program
{
    public static void Main(string[] args)
    {
        var beacon = new Beacon("twoScanners.txt");
        beacon.FindSolution();
    }
}