using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private bool isWalkable = true;

    [SerializeField]
    private bool isBuildable = true;
    public Point GridPosition { get; private set; }
    private float tileSize;
    public bool IsWalkable
    {
        get => isWalkable;
        set => isWalkable = value;
    }
    public bool IsEmpty { get; set; }
    private Color32 unavailableColor = new(255, 118, 118, 255);
    private Color32 availableColor = new(96, 255, 90, 255);
    private SpriteRenderer spriteRenderer;
    private Tower tower;
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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        IsEmpty = true;
        tileSize = spriteRenderer.sprite.bounds.size.x;
        spriteRenderer.enabled = false;
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
            //NOT BUYING TOWER
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.DeselectTower();

                if (tower != null)
                {
                    GameManager.Instance.SelectTower(tower);
                }
            }
            // ColorTile(Color.white);
            spriteRenderer.enabled = false;
            return;
        }
        else
        {
            //BUYING TOWER
            spriteRenderer.enabled = true;
            bool canBuild = true;
            if (!isBuildable || !IsEmpty)
            {
                canBuild = false;
            }
            else
            {
                if (isBuildable && isWalkable)
                {
                    if (GameManager.Instance.IsWaveActive)
                    {
                        ColorTile(Color.yellow);
                        canBuild = false;
                    }
                    else
                    {
                        ColorTile(Color.cyan);
                        canBuild = true;
                    }
                }
                else
                {
                    canBuild = true;
                }
            }
            if (canBuild)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceTower();
                }
            }
        }
    }

    private void OnMouseExit()
    {
        spriteRenderer.enabled = false;
        // ColorTile(Color.white);
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
        this.tower = tower.GetComponent<Tower>();
        IsEmpty = false;
        IsWalkable = false;
    }

    private void ColorTile(Color32 newColor)
    {
        spriteRenderer.color = newColor;
    }
}
