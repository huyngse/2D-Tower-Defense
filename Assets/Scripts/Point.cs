using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override readonly string ToString()
    {
        return $"({X}, {Y})";
    }

    public static bool operator ==(Point a, Point b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Point a, Point b)
    {
        return !(a == b);
    }

    public static Point operator -(Point a, Point b)
    {
        return new Point(a.X - b.X, a.Y - b.Y);
    }

    public override readonly int GetHashCode() => (X, Y).GetHashCode();

#nullable enable
    public override readonly bool Equals(object? obj) => obj is Point other && Equals(other);

#nullable disable

    public readonly bool Equals(Point p) => X == p.X && Y == p.Y;
}
