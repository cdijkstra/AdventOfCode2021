using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Beacon;

[TestClass]
public class BeaconTests
{
    [TestMethod]
    public void ReadInputForScanner()
    {
        var beacon = new Beacon("singleScanner.txt");
        beacon._scannerInput.Count.Should().Be(1);
        beacon._scannerInput.First().Count.Should().Be(3);
        beacon._scannerInput.First().ElementAt(0).Should().Be((0,2));
        beacon._scannerInput.First().ElementAt(1).Should().Be((4,1));
        beacon._scannerInput.First().ElementAt(2).Should().Be((3,3));
    }
    
    [TestMethod]
    public void ReadInputForMultipleScanners()
    {
        var beacon = new Beacon("twoScanners.txt");
        beacon._scannerInput.Count.Should().Be(2);
        beacon._scannerInput.All(scanner => scanner.Count == 3).Should().BeTrue();
        
        beacon._scannerInput.First().ElementAt(0).Should().Be((0,2));
        beacon._scannerInput.First().ElementAt(1).Should().Be((4,1));
        beacon._scannerInput.First().ElementAt(2).Should().Be((3,3));
        
        beacon._scannerInput.ElementAt(1).ElementAt(0).Should().Be((-1,-1));
        beacon._scannerInput.ElementAt(1).ElementAt(1).Should().Be((-5,0));
        beacon._scannerInput.ElementAt(1).ElementAt(2).Should().Be((-2,1));
    }
}