using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [Header("References")]
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

    [SerializeField]
    private GameObject greenPortalPrefab;

    [SerializeField]
    private GameObject purplePortalPrefab;

    [SerializeField]
    private Transform map;
    public Dictionary<Point, TileScript> Tiles { get; set; }
    private Point greenPortalPosition;
    private Point purplePortalPosition;
    private Point mapSize;
    public Portal GreenPortal { get; private set; }
    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
    private Vector3 worldStartPosition;

    void Awake()
    {
        Tiles = new Dictionary<Point, TileScript>();
    }

    void Start()
    {
        worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        CreateLevel();
        SpawnPortals();
    }

    void Update() { }

    private void CreateLevel()
    {
        string[] mapData = GetLevelFromFile();
        mapSize = new(mapData[0].Length, mapData.Length);

        for (int y = 0; y < mapSize.Y; y++)
        {
            char[] rows = mapData[y].ToCharArray();
            for (int x = 0; x < mapSize.X; x++)
            {
                PlaceTile(rows[x].ToString(), x, y);
            }
        }

        Vector3 bottomLeftTile = new Vector3(
            worldStartPosition.x + mapSize.X * TileSize,
            worldStartPosition.y - mapSize.Y * TileSize
        );
        cameraMovement.SetLimits(bottomLeftTile);
    }

    private void PlaceTile(string tileType, int x, int y)
    {
        int tileIndex = int.Parse(tileType);
        GameObject newTile = Instantiate(
            tilePrefabs[tileIndex],
            new Vector3(worldStartPosition.x + x * TileSize, worldStartPosition.y - y * TileSize),
            Quaternion.identity
        );
        TileScript tile = newTile.GetComponent<TileScript>();
        newTile.transform.SetParent(map);
        tile.Setup(new Point(x, y));
    }

    private string[] GetLevelFromFile()
    {
        // "Resources" is name of the Folder
        TextAsset bindData = Resources.Load("Level") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);
        return data.Split("-");
    }

    private void SpawnPortals()
    {
        greenPortalPosition = new Point(1, 2);
        GameObject greenPortal = Instantiate(
            greenPortalPrefab,
            Tiles[greenPortalPosition].transform.position,
            Quaternion.identity
        );
        greenPortal.transform.Translate(Vector3.up * 0.5f);
        GreenPortal = greenPortal.GetComponent<Portal>();
        GreenPortal.name = "GreenPortal";

        purplePortalPosition = new Point(40, 16);
        GameObject purplePortal = Instantiate(
            purplePortalPrefab,
            Tiles[purplePortalPosition].transform.position,
            Quaternion.identity
        );
        purplePortal.transform.Translate(Vector3.up * 0.5f);
    }

    public bool InBounds(Point point)
    {
        return point.X >= 0 && point.Y >= 0 && point.X < mapSize.X && point.Y < mapSize.Y;
    }
}
