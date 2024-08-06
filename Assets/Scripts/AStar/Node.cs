using System;
using UnityEngine;

public class Node
{
    public Point GridPosition { get; private set; }
    public Vector2 WorldPosition { get; private set; }
    public TileScript TileRef { get; private set; }
    public Node Parent { get; private set; }
    public int GScore { get; private set; }
    public int HScore { get; private set; }
    public int FScore { get; private set; }

    public Node(TileScript tileRef)
    {
        TileRef = tileRef;
        GridPosition = tileRef.GridPosition;
        WorldPosition = tileRef.WorldPosition;
    }

    public void CalculateValues(Node parent, Node goal, int gScore)
    {
        Parent = parent;
        GScore = parent.GScore + gScore;
        HScore = Math.Abs(GridPosition.X - goal.GridPosition.X);
        HScore += Math.Abs(GridPosition.Y - goal.GridPosition.Y);
        HScore *= 10;
        FScore = GScore + HScore;
    }
}
