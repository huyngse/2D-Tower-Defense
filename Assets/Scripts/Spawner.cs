using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class Spawner : Singleton<Spawner>
{
    [Header("References")]
    [SerializeField]
    private GameObject enemyUIPrefab;
    [SerializeField]
    private GameObject enemyUIContainer;
    private const float BaseHealthMultiplier = 1.0f;
    private const float GrowthFactor = 0.05f;
    private readonly List<WaveEnemy> monsters = new() {
        new WaveEnemy() {
            Name = "Pig",
            Color = Color.red,
        },
        new WaveEnemy() {
            Name = "Trunk",
            Color = new Color32(186, 92, 34, 255),
        },
        new WaveEnemy(){
            Name = "Slime",
            Color = Color.green,
        },
        new WaveEnemy() {
            Name = "Rock",
            Color = new Color32(143, 143, 143, 255),
        },
        new WaveEnemy() {
            Name = "Bat",
            Color = new Color32(162, 0, 255, 255),
        },
       };
    private Stack<WaveEnemy> miniWave = new();
    private Stack<WaveEnemy> enemies = new();
    public Stack<WaveEnemy> Enemies { get => enemies; set => enemies = value; }

    void Start()
    {
    }

    void Update()
    {

    }
    public void DisplayEnemies()
    {
        // Clear previous UI elements
        foreach (Transform child in enemyUIContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var enemy in enemies)
        {
            GameObject enemyUI = Instantiate(enemyUIPrefab, enemyUIContainer.transform);
            enemyUI.GetComponent<EnemyIcon>().SetColor(enemy.Color);
        }
    }

    public void GenerateWave()
    {
        miniWave.Clear();
        while (enemies.Count() < Mathf.Clamp(GameManager.Instance.Wave, 0, 15))
        {
            if (miniWave.Count == 0)
            {
                int numberOfEnemy = UnityEngine.Random.Range(1, 6);
                int enemyId = UnityEngine.Random.Range(0, monsters.Count);
                var enemyToSpawn = (WaveEnemy)monsters.ElementAt(enemyId).Clone();
                enemyToSpawn.HealthMultiplier = BaseHealthMultiplier + (GrowthFactor * GameManager.Instance.Wave * GameManager.Instance.Wave);
                for (int i = 0; i < numberOfEnemy; i++)
                {
                    miniWave.Push(enemyToSpawn);
                }
            }
            enemies.Push(miniWave.Pop());
        }
        DisplayEnemies();
    }
}
