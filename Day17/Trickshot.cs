using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day17;

public class Trickshot
{
    private int _x;
    private int _y;
    private int _vx;
    private int _vy;
    private bool _succeeded;
    
    private readonly int _xMin;
    private readonly int _xMax;
    private readonly int _yMin;
    private readonly int _yMax;

    private List<(int xs, int ys)> _trajectory; 
    private List<(int vx, int vy)> _solutions = new();
    private List<int> _highestYs = new();
    
    public Trickshot(string fileName)
    {
        var input = File.ReadLines($"../../../Input/{fileName}").First().Split();
        var xInput = input[2].Trim('x', '=').Split("..");
        var yInput = input[3].Trim('y', '=').Split("..");
        _xMin = Math.Min(int.Parse(xInput[0]), int.Parse(xInput[1].Trim(',')));
        _xMax = Math.Max(int.Parse(xInput[0]), int.Parse(xInput[1].Trim(',')));
        _yMin = Math.Min(int.Parse(yInput[0]), int.Parse(yInput[1]));
        _yMax = Math.Max(int.Parse(yInput[0]), int.Parse(yInput[1]));
    }

    public void FindSolutions()
    {
        foreach (var possibleVx in Enumerable.Range(1, _xMax))
        {
            foreach (var possibleVy in Enumerable.Range(_yMin, Math.Abs(_yMin * 2)))
            {
                SetParameters(possibleVx, possibleVy);
                while (!_succeeded && _y > _yMin)
                {
                    UpdatePosition();
                    CalculateVelocity();
                    CheckIfTrickShot(possibleVx, possibleVy);
                }

                ResetParameters();
            }
        }
        
        Console.WriteLine($"Found highest y: {_highestYs.Max()}");
        Console.WriteLine($"Amount of solutions: {_solutions.Count}");
    }

    private void UpdatePosition()
    {
        _x += _vx;
        _y += _vy;
        
        _trajectory.Add((_x, _y));
    }

    private void CalculateVelocity()
    {
        // Gravity
        _vy--;

        // Friction; converge to zero
        if (_vx > 0)
        {
            _vx--;
        }
    }

    private void CheckIfTrickShot(int possibleVx, int possibleVy)
    {
        if (_x < _xMin || _x > _xMax || _y < _yMin || _y > _yMax) 
            return;
        
        var highestY = _trajectory.Select(x => x.ys).Max();
        _highestYs.Add(highestY);
            
        _solutions.Add((possibleVx, possibleVy));
        _succeeded = true;
    }

    private void SetParameters(int possibleVx, int possibleVy)
    {
        _succeeded = false;
        _vx = possibleVx;
        _vy = possibleVy;
        
        _trajectory = new List<(int xs, int ys)>
        {
            (_x, _y)
        };
    }
    private void ResetParameters()
    {
        _succeeded = false;
        _x = 0;
        _y = 0;
        _vx = 0;
        _vy = 0;

        _trajectory = new List<(int xs, int ys)>();
    }
}