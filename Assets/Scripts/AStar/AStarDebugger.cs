using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebugger : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Sprite whiteSprite;

    [SerializeField]
    private GameObject arrowPrefab;
    private TileScript start;
    private TileScript goal;

    void Update()
    {
        ClickTile();
        if (start == null || goal == null)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AStar.GetPath(start.GridPosition);
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
                        start.IsDebugging = true;
                        start.ColorTile(new Color32(245, 188, 66, 255));
                        start.SpriteRenderer.sprite = whiteSprite;
                    }
                    else if (goal == null)
                    {
                        goal = tmp;
                        goal.IsDebugging = true;
                        goal.ColorTile(new Color32(245, 66, 66, 255));
                        goal.SpriteRenderer.sprite = whiteSprite;
                    }
                }
            }
        }
    }

    public void DebugPath(HashSet<Node> openList)
    {
        foreach (Node node in openList)
        {
            if (node.GridPosition != start.GridPosition)
            {
                node.TileRef.IsDebugging = true;
                node.TileRef.ColorTile(new Color32(66, 114, 245, 255));
                node.TileRef.SpriteRenderer.sprite = whiteSprite;
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
}
