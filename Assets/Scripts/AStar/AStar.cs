using System.Collections.Generic;

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
}
