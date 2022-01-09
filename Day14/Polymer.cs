using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace Day14
{
    public interface IMemoryCacheRepository
    {
        bool TryGetValue<T>(string key, out T value);
        void SetValue<T>(string key, T value);
        void Remove(string key);
    }
   
    public class MemoryCacheRepository : IMemoryCacheRepository
    {
        // We will hold 1024 cache entries
        private static readonly int _SIZELIMIT = 1024;
        // A cache entry expire after 15 minutes
        private static readonly int _ABSOLUTEEXPIRATION = 90;
      
        private MemoryCache Cache { get; set; }
      
        public MemoryCacheRepository()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = _SIZELIMIT
            });
        }
  
        // Try getting a value from the cache.
        public bool TryGetValue<T>(string key, out T value)
        {
            value = default(T);
  
            if (Cache.TryGetValue(key, out T result))
            {
                value = result;
                return true;
            }
  
            return false;
        }
  
        // Adding a value to the cache. All entries
        // have size = 1 and will expire after 15 minutes
        public void SetValue<T>(string key, T value)
        {
            Cache.Set(key, value, new MemoryCacheEntryOptions()
                .SetSize(1)
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(_ABSOLUTEEXPIRATION))
            );
        }
  
        // Remove entry from cache
        public void Remove(string key)
        {
            Cache.Remove(key);
        }
    }
    
    class Polymer
    {
        private readonly MemoryCacheRepository _objectCache = new MemoryCacheRepository();

        private readonly string _cacheName = "Polymer";
        private Dictionary<string, char> _tuples = new();

        public Polymer(string fileName)
        {
            var file = new List<string>(File.ReadAllLines($"../../../Input/{fileName}"));
            var polymer = file.First();
            _objectCache.SetValue(_cacheName, polymer);

            foreach (var line in file.Where(l => l.Contains("->")))
            {
                var input = line.Split("->");
                _tuples.Add(input[0].Trim(), char.Parse(input[1].Trim()));
            }
        }

        public void CalculateSolution(int numberOfRepeats = 1)
        {
            for (int repeat = 1; repeat <= numberOfRepeats; repeat++)
            {
                Console.Write(repeat);
                
                List<(int index, char newValue)> newInserts = new();
                _objectCache.TryGetValue(_cacheName, out string polymer);
                
                for (var idx = 0; idx != polymer.Length - 1; idx++)
                {
                    var pair = (polymer.ElementAt(idx) + polymer.ElementAt(idx + 1).ToString());
                    if (_tuples.ContainsKey(pair))
                    {
                        newInserts.Add((idx + 1, _tuples[pair]));
                    }
                }

                foreach (var insert in newInserts.OrderByDescending(x => x.index))
                {
                    polymer = polymer.Insert(insert.index, insert.newValue.ToString());
                }
                _objectCache.SetValue(_cacheName, polymer);
            }

            _objectCache.TryGetValue(_cacheName, out string result);
            var biggestQuantity = result.GroupBy(ch => ch).OrderByDescending(ch => ch.Count()).First().Count();
            var smallestQuantity = result.GroupBy(ch => ch).OrderBy(ch => ch.Count()).First().Count();

            Console.Write(biggestQuantity - smallestQuantity);
        }
    }
}
