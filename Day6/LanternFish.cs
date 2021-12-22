using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace Day6
{
    public class LanternFish
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        public List<int> _timers = new();

        public LanternFish(string file)
        {
            var lines = File.ReadAllText(Path.Combine("../../../", file)).Split(Environment.NewLine);
            
            foreach (var line in lines)
            {
                var numbers = line.Split(",");
                foreach (var number in numbers)
                {
                    _timers.Add(int.Parse(number));
                }
            }
        }

        public Int64 CalculateFishForDay(int calculateForDay)
        {
            var listCache = new MyListCache();

            foreach (var unused in Enumerable.Range(1, calculateForDay))
            {
                var fishToAdd = 0;
                for (var fishNumber = 0; fishNumber != _timers.Count; fishNumber++)
                {
                    if (_timers[fishNumber] == 0)
                    {
                        _timers[fishNumber] = 6;
                        fishToAdd++;
                    }
                    else
                    {
                        _timers[fishNumber]--;
                    }
                }
                
                foreach (var newFish in Enumerable.Range(0, fishToAdd))
                {
                    _timers.Add(8);
                }
            }

            return _timers.Count;
        }
    }
}