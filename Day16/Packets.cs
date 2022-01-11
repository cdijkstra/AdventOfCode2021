using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    public class Packets
    {
        private Queue<int> _digits = new();
        private List<int> _packetVersions = new();
        
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

        public void DecomposePacket()
        {
            while (_digits.Any() && _digits.Count > 10)
            {
                AddPacketVersion();
                var packetId = Convert.ToInt32(_digits.Dequeue().ToString() + _digits.Dequeue() + _digits.Dequeue(), 2); 
                var packetType = GetTypeById(packetId);
                Console.WriteLine($"Found type {packetType}");

                switch (packetType)
                {
                    case PacketType.Literal:
                    {
                        // Break into sequences of 5 bits
                        while (_digits.Dequeue() != 0)
                        {
                            DequeueFourTimes();
                        }

                        DequeueFourTimes();
                        break;
                    }
                    case PacketType.Operator:
                    {
                        int lengthId = _digits.Dequeue();
                        switch (lengthId)
                        {
                            case 0:
                            {
                                string binaryNumberOfBits = "";
                                for (int idx = 0; idx != 15; idx++)
                                {
                                    binaryNumberOfBits += _digits.Dequeue().ToString();
                                }

                                var numberOfBits = Convert.ToInt32(binaryNumberOfBits, 2);
                                Console.WriteLine(numberOfBits);
                                // 15 bit number representing number of bits in subpackages
                                break;
                            }
                            case 1:
                            {
                                // 11 bit number representing number subpackages
                                Console.WriteLine(1);
                                string binaryNumberOfPackages = "";
                                for (int idx = 0; idx != 11; idx++)
                                {
                                    binaryNumberOfPackages += _digits.Dequeue().ToString();
                                }
                                
                                var expectedNumberOfRepeats = Convert.ToInt32(binaryNumberOfPackages, 2);
                                break;
                            }
                        }

                        break;
                    }
                }
            }
            
            Console.Write(_packetVersions.Sum());
        }

        private void DequeueFourTimes()
        {
            _digits.Dequeue();
            _digits.Dequeue();
            _digits.Dequeue();
            _digits.Dequeue();
        }

        private void AddPacketVersion()
        {
            var packetVersion = Convert.ToInt32(_digits.Dequeue().ToString() + _digits.Dequeue() + _digits.Dequeue(), 2);
            _packetVersions.Add(packetVersion);
        }

        private PacketType GetTypeById(int id)
        {
            return id == 4 ? PacketType.Literal : PacketType.Operator;
        }
    }
}