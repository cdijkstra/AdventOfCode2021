using System.Text.RegularExpressions;

namespace Beacon;

public class Beacon
{
    public List<string> _totalInput = new();
    public List<List<(int x, int y)>> _scannerInput = new();

    public List<(int x, int y)> _locationScanners = new();
    
    public Beacon(string fileName)
    {
        _totalInput = new List<string>(File.ReadAllLines($"../../../Input/{fileName}"));

        Regex regex = new(@"--- scanner \d+ ---");
        List<ValueTuple<int, int>> scannerInput = new();
        foreach (var line in _totalInput.Where(li => li != ""))
        {
            if (regex.IsMatch(line))
            {
                if (scannerInput.Any())
                {
                    _scannerInput.Add(scannerInput);
                }

                scannerInput = new();
            }
            else
            {
                var nums = line.Split(',');
                ValueTuple<int, int> tuple = (int.Parse(nums[0]), int.Parse(nums[1]));
                
                scannerInput.Add(tuple);
            }
        }
        
        _scannerInput.Add(scannerInput);
        
        var locationFirstScanner = (0, 0);
        _locationScanners.Add(locationFirstScanner);
    }

    public void FindSolution()
    {
        // Check if second scanner's findings can be put on the same map
        // This can be done by putting the first entry on any of the entries found by the first scanner
        var offset = _scannerInput[1][0];

        bool found = false;
        int entry = -1;
        while (!found && ++entry < _scannerInput.First().Count)
        {
            var locationFromScanner0 = _scannerInput.First()[entry];
            var deltaX = offset.x - locationFromScanner0.x;
            var deltaY = offset.y - locationFromScanner0.y;

            if (_scannerInput[1].Count(test => _scannerInput[0].Contains((test.x - deltaX, test.y - deltaY))) >= 3)
            {
                Console.WriteLine($"YEAHHH, using {deltaX} and {deltaY}");
                found = true;
            }
        }

    }

    public List<List<(int x, int y)>> FindOrientations(List<(int x, int y)> scannerInput)
    {
        List<List<(int x, int y)>> possibleOrientations = new();
        
        var newOrientation = 
        
    }

}