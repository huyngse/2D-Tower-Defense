using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilescript : MonoBehaviour
{
    public Point GridPosition { get; private set; }
    private float tileSize;
    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(
                transform.position.x + tileSize / 2,
                transform.position.y - tileSize / 2
            );
        }
    }

    void Start()
    {
        tileSize = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }

    void Update() { }

    public void Setup(Point gridPosition)
    {
        GridPosition = gridPosition;
        LevelManager.Instance.Tiles.Add(gridPosition, this);
    }

    private void OnMouseOver()
    {
        // Debug.Log($"Hover over tiles: {GridPosition.X}, {GridPosition.Y} ");
        if (Input.GetMouseButtonDown(0)) {
            PlaceTower();
        }
    }


    private void PlaceTower()
    {
        // Debug.Log($"Built a tower at : {GridPosition.X}, {GridPosition.Y} ");
        GameObject tower = Instantiate(GameManager.Instance.TowerPrefab, WorldPosition, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
        tower.transform.SetParent(transform);
    }
}
