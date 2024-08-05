using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }
    private float tileSize;
    public bool IsEmpty { get; set; }
    public bool IsWalkable { get; set; }
    private Color32 unavailableColor = new(255, 118, 118, 255);
    private Color32 availableColor = new(96, 255, 90, 255);
    private SpriteRenderer spriteRenderer;
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
        IsWalkable = true;
        IsEmpty = true;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tileSize = spriteRenderer.sprite.bounds.size.x;
    }

    void Update() { }

    public void Setup(Point gridPosition)
    {
        GridPosition = gridPosition;
        LevelManager.Instance.Tiles.Add(gridPosition, this);
    }

    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
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
        ColorTile(Color.white);
    }

    private void PlaceTower()
    {
        GameObject tower = Instantiate(
            GameManager.Instance.ClickedButton.TowerPrefab,
            WorldPosition,
            Quaternion.identity
        );
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
        tower.transform.SetParent(transform);
        GameManager.Instance.BuyTower();
        IsEmpty = false;
        IsWalkable = false;
    }

    private void ColorTile(Color32 newColor)
    {
        spriteRenderer.color = newColor;
    }
}
