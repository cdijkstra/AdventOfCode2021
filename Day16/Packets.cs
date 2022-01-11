using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    public class Packets
    {
        private Queue<int> _digits = new();
        
        private bool _version = true;
        private bool _id = false;
        private bool _skip = false;

        private int _packetVersion;
        private PacketType _packetType;

        public Packets(string fileName)
        {
            var message = File.ReadLines($"../../../Input/{fileName}").First();
            foreach (char bit in message)
            {
                List<int> newInts = new();
                switch (bit)
                {
                    case '0':
                        newInts.AddRange(new List<int> {0, 0, 0, 0});
                        break;
                    case '1':
                        newInts.AddRange(new List<int> {0, 0, 0, 1});
                        break;
                    case '2':
                        newInts.AddRange(new List<int> {0, 0, 1, 0});
                        break;
                    case '3':
                        newInts.AddRange(new List<int> {0, 0, 1, 1});
                        break;
                    case '4':
                        newInts.AddRange(new List<int> {0, 1, 0, 0});
                        break;
                    case '5':
                        newInts.AddRange(new List<int> {0, 1, 0, 1});
                        break;
                    case '6':
                        newInts.AddRange(new List<int> {0, 1, 1, 0});
                        break;
                    case '7':
                        newInts.AddRange(new List<int> {0, 1, 1, 1});
                        break;
                    case '8':
                        newInts.AddRange(new List<int> {1, 0, 0, 0});
                        break;
                    case '9':
                        newInts.AddRange(new List<int> {1, 0, 0, 1});
                        break;
                    case 'A':
                        newInts.AddRange(new List<int> {1, 0, 1, 0});
                        break;
                    case 'B':
                        newInts.AddRange(new List<int> {1, 0, 1, 1});
                        break;
                    case 'C':
                        newInts.AddRange(new List<int> {1, 1, 0, 0});
                        break;
                    case 'D':
                        newInts.AddRange(new List<int> {1, 1, 0, 1});
                        break;
                    case 'E':
                        newInts.AddRange(new List<int> {1, 1, 1, 0});
                        break;
                    case 'F':
                        newInts.AddRange(new List<int> {1, 1, 1, 1});
                        break;
                }

                foreach (var newint in newInts)
                {
                    _digits.Enqueue(newint);
                }
            }
            
            foreach (var digit in _digits)
            {
                Console.Write(digit);
            }
            Console.WriteLine($"\nFinding {_digits.Count}");
        }

        public void SolvePuzzle()
        {
            while (_digits.Any() &&  _skip == false)
            {
                if (_version)
                {
                    _packetVersion = Convert.ToInt32(_digits.Dequeue().ToString() + _digits.Dequeue() + _digits.Dequeue(), 2);
                    _version = false;
                    _id = true;
                    Console.WriteLine($"Found version {_packetVersion}");
                }
                else if (_id)
                {
                    var packetId = Convert.ToInt32(_digits.Dequeue().ToString() + _digits.Dequeue() + _digits.Dequeue(), 2);
                    _packetType = GetTypeById(packetId);
                    Console.WriteLine($"Found type {_packetType}");
                    _id = false;
                }
                else if (_packetType == PacketType.Operator)
                {
                    int lengthId = _digits.Dequeue();
                    if (lengthId == 0)
                    {
                        string binaryNumberOfBits = "";
                        for (int idx = 0; idx != 15; idx++)
                        {
                            binaryNumberOfBits += _digits.Dequeue().ToString();
                        }

                        var numberOfBits = Convert.ToInt32(binaryNumberOfBits, 2);
                        Console.WriteLine(numberOfBits);
                        // 15 bit number representing number of bits in subpackages
                        
                        // What to do???
                        _skip = true;
                    }
                    else if (lengthId == 1)
                    {
                        // 11 bit number representing number subpackages
                        Console.WriteLine(1);
                        string binaryNumberOfPackages = "";
                        for (int idx = 0; idx != 11; idx++)
                        {
                            binaryNumberOfPackages += _digits.Dequeue().ToString();
                        }

                        var numberOfPackets = Convert.ToInt32(binaryNumberOfPackages, 2);
                        foreach (var repeat in Enumerable.Range(0, numberOfPackets))
                        {
                            string value = "";
                            foreach (var appendDigit in Enumerable.Range(0, 11))
                            {
                                value += _digits.Dequeue().ToString();
                            }
                            int trueValue = Int32.Parse(value);
                            Console.WriteLine(trueValue);
                        }

                        _skip = true;
                    }
                }
                else if (_packetType == PacketType.Literal)
                {
                    // Break into sequences of 5 bits
                    string literalValue = "";
                    foreach (var repeat in Enumerable.Range(0, (int) Math.Floor((decimal) (_digits.Count / 5))))
                    {
                        _digits.Dequeue(); // Skip first
                         literalValue += _digits.Dequeue().ToString() + _digits.Dequeue() + _digits.Dequeue() + _digits.Dequeue();
                    }

                    int litValue =  Convert.ToInt32(literalValue, 2);
                }
            }
        }

        private PacketType GetTypeById(int id)
        {
            return id == 4 ? PacketType.Literal : PacketType.Operator;
        }
    }
}