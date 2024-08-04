using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebugger : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Sprite whiteSprite;
    private TileScript start;
    private TileScript goal;

    void Update()
    {
        ClickTile();
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
                TileScript tmp = hit.collider.GetComponent<TileScript>();
                if (tmp != null)
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
}
