using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject[] tilePrefabs;

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
        string[] mapData = new string[]
        {
            "00000000000000000000000000000000",
            "11111111111111111111111111111110",
            "22222222222222222222222222222210",
            "33333333333333333333333333333210",
            "44444444444444444444444444443210",
            "55555555555555555555555555543210",
            "00000000000000000000000000543210",
            "00000000000000000000000000543210",
            "00000000000000000000000000543210",
            "00000000000000000000000000543210",
            "00000000000000000000000000543210",
            "00000000000000000000000000543210",
            "00000000000000000000000000543210",
            "00000000000000000000000000543210",
            "00000000000000000000000000543210",
            "00000000000000000000000000543210",
            "00000000000000000000000000543210",
            "00000000000000000000000000543210",
            "00000000000000000000000000543210",
            "00000000000000000000000000543210",
        };
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
}
