using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }
    private float tileSize;
    public bool IsEmpty { get; set; }
    private Color32 unavailableColor = new(255, 118, 118, 255);
    private Color32 availableColor = new(96, 255, 90, 255);
    public bool IsDebugging { get; set; }

    public SpriteRenderer SpriteRenderer { get; private set; }
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

    void Awake()
    {
        IsDebugging = false;
        IsEmpty = true;
    }

    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        tileSize = SpriteRenderer.sprite.bounds.size.x;
    }

    void Update() { }

    public void Setup(Point gridPosition)
    {
        GridPosition = gridPosition;
        LevelManager.Instance.Tiles.Add(gridPosition, this);
    }

    private void OnMouseOver()
    {
        if (IsDebugging || EventSystem.current.IsPointerOverGameObject())
            return;
        // Debug.Log($"Hover over tiles: {GridPosition.X}, {GridPosition.Y} ");
        if (GameManager.Instance.ClickedButton == null)
        {
            ColorTile(Color.white);
            return;
        }

        if (IsEmpty)
        {
            ColorTile(availableColor);
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
        else
        {
            ColorTile(unavailableColor);
        }
    }

    private void OnMouseExit()
    {
        if (IsDebugging)
            return;
        ColorTile(Color.white);
    }

    private void PlaceTower()
    {
        // Debug.Log($"Built a tower at : {GridPosition.X}, {GridPosition.Y} ");
        GameObject tower = Instantiate(
            GameManager.Instance.ClickedButton.TowerPrefab,
            WorldPosition,
            Quaternion.identity
        );
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
        tower.transform.SetParent(transform);
        GameManager.Instance.BuyTower();
        IsEmpty = false;
    }

    public void ColorTile(Color newColor)
    {
        SpriteRenderer.color = newColor;
    }
}
