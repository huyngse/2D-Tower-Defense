public class Node
{
    public Point GridPosition { get; private set; }
    public TileScript TileRef { get; private set; }
    public Node Parent { get; private set; }

    public Node(TileScript tileRef)
    {
        TileRef = tileRef;
        GridPosition = tileRef.GridPosition;
    }

    public void CalculateValues(Node parent) { 
        Parent = parent;
    }
}
