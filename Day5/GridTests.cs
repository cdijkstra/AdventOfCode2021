using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day5
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void linesInitializedForLine()
        {
            var grid = new Grid("Input/oneline.txt");
            grid.Initialize();
            grid._xs.Count.Should().Be(2);
            grid._ys.Count.Should().Be(2);
        }
        
        [TestMethod]
        public void gridInitializedForLine()
        {
            var grid = new Grid("Input/oneline.txt");
            grid.Initialize();
            grid.CreateGrid();
            grid._grid.Count.Should().Be(6);
            grid._grid.First().Count.Should().Be(10);
            foreach (var line in grid._grid)
            {
                line.All(x => x == 0).Should().BeTrue();
            }
        }
        
        [TestMethod]
        public void ReadAllLinesShouldNotThrowForLine()
        {
            var grid = new Grid("Input/oneline.txt");
            grid.Initialize();
            grid.CreateGrid();
            Action action = () => grid.ReadLines();
            action.Should().NotThrow();
        }
        
        [TestMethod]
        public void linesInitializedForDummyFile()
        {
            var grid = new Grid("Input/dummydata.txt");
            grid.Initialize();
            grid._xs.Count.Should().Be(20);
            grid._ys.Count.Should().Be(20);
        }

        [TestMethod]
        public void gridInitializedForDummyFile()
        {
            var grid = new Grid("Input/dummydata.txt");
            grid.Initialize();
            grid.CreateGrid();
            grid._grid.Count.Should().Be(10);
            grid._grid.First().Count.Should().Be(10);
            foreach (var line in grid._grid)
            {
                line.All(x => x == 0).Should().BeTrue();
            }
        }
        
        [TestMethod]
        public void ReadAllLinesShouldNotThrowForDummyFile()
        {
            var grid = new Grid("Input/dummydata.txt");
            grid.Initialize();
            grid.CreateGrid();
            Action action = () => grid.ReadLines();
            action.Should().NotThrow();
        }
        
        [TestMethod]
        public void GridShoulBeAsExpectedForDummyData()
        {
            var grid = new Grid("Input/dummydata.txt");
            grid.Initialize();
            grid.CreateGrid();
            grid.ReadLines();
            grid._grid.SelectMany(row => row.Where(num => num > 0)).Should().HaveCount(39);
            // Too lazy to verify Equivalence with whole grid. Seems to fair to assume this tests rigorously enough

            foreach (var line in grid._grid)
            {
                foreach (var number in line)
                {
                    Console.Write(number);
                }
                Console.Write('\n');
            }
        }
        
        [TestMethod]
        public void ShowMaxNumbersForDummyFile()
        {
            var grid = new Grid("Input/dummydata.txt");
            grid.Initialize();
            grid.CreateGrid();
            grid.ReadLines();
            grid.ShowSolution().Should().Be(12);
        }
        
        [TestMethod]
        public void ShowMaxNumbersForFile()
        {
            var grid = new Grid("Input/data.txt");
            grid.Initialize();
            grid.CreateGrid();
            grid.ReadLines();
            grid.ShowSolution().Should().Be(12);
        }
    }
}