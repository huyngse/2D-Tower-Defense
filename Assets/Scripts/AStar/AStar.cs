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
                if (!neighbor.TileRef.IsWalkable)
                    continue;
                neighbor.TileRef.SpriteRenderer.color = Color.black;
                neighbor.TileRef.IsDebugging = true;
            }
        }

        AStarDebugger debugger = GameObject.Find("AStarDebugger").GetComponent<AStarDebugger>();
        debugger.DebugPath(openList);
    }
}
