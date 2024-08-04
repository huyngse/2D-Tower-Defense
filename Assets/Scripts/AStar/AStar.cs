using System.Collections.Generic;
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

    public static void GetPath(Point start)
    {
        if (nodes == null)
        {
            CreateNodes();
        }
        HashSet<Node> openList = new();
        Node currentNode = nodes[start];
        openList.Add(currentNode);

        AStarDebugger debugger = GameObject.Find("AStarDebugger").GetComponent<AStarDebugger>();
        debugger.DebugPath(openList);
    }
}
