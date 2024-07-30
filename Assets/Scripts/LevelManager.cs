using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject tile;

    public float TileSize
    {
        get { return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
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
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 32; x++)
            {
                PlaceTile(x, y);
            }
        }
    }

    private void PlaceTile(float x, float y)
    {
        Instantiate(
            tile,
            new Vector3(worldStartPosition.x + x * TileSize, worldStartPosition.y - y * TileSize),
            Quaternion.identity
        );
    }
}
