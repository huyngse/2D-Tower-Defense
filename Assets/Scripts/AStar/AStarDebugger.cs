using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebugger : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private GameObject debugTilePrefab;
    private TileScript start;
    private TileScript goal;

    void Update()
    {
        ClickTile();
        if (start == null || goal == null)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AStar.GetPath(start.GridPosition, goal.GridPosition);
        }
    }

    private void ClickTile()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero
            );
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent<TileScript>(out var tmp))
                {
                    if (start == null)
                    {
                        start = tmp;
                        CreateDebugTile(start.WorldPosition, new Color32(245, 188, 66, 255));
                    }
                    else if (goal == null)
                    {
                        goal = tmp;
                        CreateDebugTile(goal.WorldPosition, new Color32(245, 66, 66, 255));
                    }
                }
            }
        }
    }

    public void DebugPath(HashSet<Node> openList, HashSet<Node> closedList, Stack<Node> path)
    {
        foreach (Node node in openList)
        {
            if (node.TileRef != start && node.TileRef != goal)
            {
                CreateDebugTile(node.TileRef.WorldPosition, new Color32(66, 114, 245, 255), node);
                PointToParent(node);
            }
        }
        foreach (Node node in closedList)
        {
            if (node.TileRef != start && node.TileRef != goal && !path.Contains(node))
            {
                CreateDebugTile(node.TileRef.WorldPosition, new Color32(122, 52, 235, 255), node);
            }
        }
        while (path.Count > 0)
        {
            Node node = path.Pop();
            if (node.TileRef != start && node.TileRef != goal)
            {
                CreateDebugTile(node.TileRef.WorldPosition, new Color32(48, 252, 3, 255), node);
                PointToParent(node);
            }
        }
    }

    private void PointToParent(Node child)
    {
        Node parent = child.Parent;
        float angle =
            Mathf.Atan2(
                child.TileRef.WorldPosition.y - parent.TileRef.WorldPosition.y,
                child.TileRef.WorldPosition.x - parent.TileRef.WorldPosition.x
            ) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        GameObject arrow = Instantiate(arrowPrefab, child.TileRef.WorldPosition, targetRotation);
        arrow.transform.SetParent(child.TileRef.transform);
    }

    private void CreateDebugTile(Vector2 position, Color32 color, Node node = null)
    {
        GameObject newTile = Instantiate(debugTilePrefab, position, Quaternion.identity);
        newTile.GetComponent<SpriteRenderer>().color = color;
        if (node != null)
        {
            DebugTile debugTile = newTile.GetComponent<DebugTile>();
            debugTile.GText.text = "G:" + node.GScore;
            debugTile.HText.text = "H:" + node.HScore;
            debugTile.FText.text = "F:" + node.FScore;
        }
    }
}
