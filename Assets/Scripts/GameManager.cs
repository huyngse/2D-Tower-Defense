using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    [SerializeField]
    private TMP_Text currencyText;

    [SerializeField]
    private TMP_Text waveText;

    [SerializeField]
    private Button nextWaveButton;

    [Header("Attributes")]
    [SerializeField]
    private int currency = 100;

    [SerializeField]
    private float enemiesPerSecond = 1;
    private int wave = 0;
    private List<Monster> activeMonsters = new();
    public bool IsWaveActive
    {
        get { return activeMonsters.Count > 0; }
    }
    public TowerButton ClickedButton { get; private set; }
    public ObjectPool Pool { get; private set; }
    public int Currency
    {
        get => currency;
        set
        {
            currency = value;
            currencyText.text = value.ToString() + "<color=orange>$</color>";
        }
    }

    void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    void Start()
    {
        Currency = currency;
    }

    void Update()
    {
        HandleCancel();
    }

    public void PickTower(TowerButton towerButton)
    {
        if (IsWaveActive)
            return;
        if (Currency >= towerButton.Price)
        {
            ClickedButton = towerButton;
            Hover.Instance.Activate(ClickedButton.Sprite);
        }
    }

    public void BuyTower()
    {
        Currency -= ClickedButton.Price;
        ClickedButton = null;
        Hover.Instance.Deativate();
    }

    private void HandleCancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickedButton = null;
            Hover.Instance.Deativate();
        }
    }

    public void StartWave()
    {
        wave++;
        waveText.text = $"Wave: <color=green>{wave}</color>";
        StartCoroutine(SpawnWave());
        nextWaveButton.gameObject.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();
        for (int i = 0; i < wave; i++)
        {
            int monsterIndex = Random.Range(0, 4);
            string type = string.Empty;
            switch (monsterIndex)
            {
                case 0:
                {
                    type = "Pig";
                    break;
                }
                case 1:
                {
                    type = "Rock";
                    break;
                }
                case 2:
                {
                    type = "Slime";
                    break;
                }
                case 3:
                {
                    type = "Trunk";
                    break;
                }
            }
            Monster monster = Pool.GetObject(type).GetComponent<Monster>();
            monster.Spawn();
            activeMonsters.Add(monster);
            yield return new WaitForSeconds(1f / enemiesPerSecond);
        }
    }

    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);
        if (!IsWaveActive)
        {
            nextWaveButton.gameObject.SetActive(true);
        }
    }
}
