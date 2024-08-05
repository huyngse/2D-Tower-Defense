public class Node
{
    public Point GridPosition { get; private set; }
    public TileScript TileRef { get; private set; }
    public Node Parent { get; private set; }
    public int GScore { get; set; }

    public Node(TileScript tileRef)
    {
        TileRef = tileRef;
        GridPosition = tileRef.GridPosition;
    }

    public void CalculateValues(Node parent, int gScore) { 
        Parent = parent;
        GScore = parent.GScore + gScore;
    }
}
