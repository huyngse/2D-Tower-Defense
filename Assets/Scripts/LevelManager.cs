using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject tile;

    void Start()
    {
        CreateLevel();
    }

    void Update() { }

    private void CreateLevel()
    {
        float tileSize = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                Instantiate(tile, new Vector3(x * tileSize, y * tileSize, 0), Quaternion.identity);
            }
        }
    }
}
