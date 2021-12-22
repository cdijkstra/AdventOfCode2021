using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Day6;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day5
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void LanternCanBeInitialized()
        {
            var lanternFish = new LanternFish("Input/dummydata.txt");
            lanternFish._timers.Count.Should().Be(5);
        }
        
        [TestMethod]
        public void TestDummyDataDay18()
        {
            var lanternFish = new LanternFish("Input/dummydata.txt");
            lanternFish.CalculateFishForDay(18).Should().Be(26);
        }

        [TestMethod]
        public void TestDummyDataDay80()
        {
            var lanternFish = new LanternFish("Input/dummydata.txt");
            lanternFish.CalculateFishForDay(80).Should().Be(5934);
        }
        
        [TestMethod]
        public void TestDataDay80()
        {
            var lanternFish = new LanternFish("Input/data.txt");
            Console.Write(lanternFish.CalculateFishForDay(256));
        }
    }
}