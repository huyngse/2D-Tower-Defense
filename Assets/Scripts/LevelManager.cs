using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;
    public Dictionary<Point, Tilescript> Tiles { get; set; }

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
    private Vector3 worldStartPosition;

    void Awake()
    {
        Tiles = new Dictionary<Point, Tilescript>();
    }

    void Start()
    {
        worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        CreateLevel();
    }

    void Update() { }

    private void CreateLevel()
    {
        string[] mapData = GetLevelFromFile();
        int mapWidth = mapData[0].Length;
        int mapHeight = mapData.Length;

        for (int y = 0; y < mapHeight; y++)
        {
            char[] rows = mapData[y].ToCharArray();
            for (int x = 0; x < mapWidth; x++)
            {
                PlaceTile(rows[x].ToString(), x, y);
            }
        }

        Vector3 bottomLeftTile = new Vector3(worldStartPosition.x + mapWidth * TileSize, worldStartPosition.y - mapHeight * TileSize);
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
        Tilescript tile = newTile.GetComponent<Tilescript>();
        tile.Setup(new Point(x, y));
        Tiles.Add(tile.GridPosition, tile);
    }

    private string[] GetLevelFromFile()
    {
        // "Resources" is name of the Folder
        TextAsset bindData = Resources.Load("Level") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);
        return data.Split("-");
    }
}
