using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AStar
{
    private static Dictionary<Point, Node> nodes;

    private static void CreateNodes()
    {
        nodes = new();
        foreach (TileScript tile in LevelManager.Instance.Tiles.Values)
        {
            nodes.Add(tile.GridPosition, new Node(tile));
        }
    }

    public static void GetPath(Point start, Point goal)
    {
        if (nodes == null)
        {
            CreateNodes();
        }
        HashSet<Node> openList = new();
        HashSet<Node> closedList = new();
        Stack<Node> path = new();
        Node currentNode = nodes[start];
        openList.Add(currentNode);
        while (currentNode.GridPosition != goal && openList.Count > 0)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Point neighborPos =
                        new(currentNode.GridPosition.X + x, currentNode.GridPosition.Y + y);
                    if (
                        !LevelManager.Instance.InBounds(neighborPos)
                        || neighborPos == currentNode.GridPosition
                    )
                        continue;
                    Node neighbor = nodes[neighborPos];
                    if (!neighbor.TileRef.IsWalkable || closedList.Contains(neighbor))
                        continue;
                    int gScore;
                    if (Math.Abs(x - y) == 1)
                    {
                        gScore = 10;
                    }
                    else
                    {
                        if (!ConnectedDiagonally(currentNode, neighbor))
                            continue;
                        gScore = 14;
                    }
                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                        neighbor.CalculateValues(currentNode, nodes[goal], gScore);
                    }
                    else if (currentNode.GScore + gScore < neighbor.GScore)
                    {
                        neighbor.CalculateValues(currentNode, nodes[goal], gScore);
                    }
                }
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);
            if (openList.Count > 0)
            {
                currentNode = openList.OrderBy(n => n.FScore).First();
            }
        }
        if (currentNode.GridPosition == goal)
        {
            while (currentNode.Parent != null)
            {
                path.Push(currentNode);
                currentNode = currentNode.Parent;
            }
        }
        AStarDebugger debugger = GameObject.Find("AStarDebugger").GetComponent<AStarDebugger>();
        debugger.DebugPath(openList, closedList, path);
    }

    private static bool ConnectedDiagonally(Node currentNode, Node neighborNode)
    {
        Point direction = neighborNode.GridPosition - currentNode.GridPosition;
        Point first = new(currentNode.GridPosition.X + direction.X, currentNode.GridPosition.Y);
        Point second = new(currentNode.GridPosition.X, currentNode.GridPosition.Y + direction.Y);
        if (nodes[first].TileRef.IsWalkable && nodes[second].TileRef.IsWalkable)
        {
            return true;
        }
        return false;
    }
}
