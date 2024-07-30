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

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
    private Vector3 worldStartPosition;

    void Start()
    {
        worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        CreateLevel();
    }

    void Update() { }

    private void CreateLevel()
    {
        string[] mapData = GetLevelFromFile();
        Vector3 bottomLeftTile = new Vector3(
            worldStartPosition.x + mapData[0].Length * TileSize,
            worldStartPosition.y - mapData.Length * TileSize
        );
        cameraMovement.SetLimits(bottomLeftTile);
        for (int y = 0; y < mapData.Length; y++)
        {
            char[] rows = mapData[y].ToCharArray();
            for (int x = 0; x < mapData[0].Length; x++)
            {
                PlaceTile(rows[x].ToString(), x, y);
            }
        }
    }

    private void PlaceTile(string tileType, float x, float y)
    {
        int tileIndex = int.Parse(tileType);
        Instantiate(
            tilePrefabs[tileIndex],
            new Vector3(worldStartPosition.x + x * TileSize, worldStartPosition.y - y * TileSize),
            Quaternion.identity
        );
    }

    private string[] GetLevelFromFile()
    {
        // "Resources" is name of the Folder
        TextAsset bindData = Resources.Load("Level") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);
        return data.Split("-");
    }
}
