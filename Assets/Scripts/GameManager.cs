using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    [SerializeField]
    private TMP_Text currencyText;

    [SerializeField]
    private TMP_Text waveText;

    [SerializeField]
    private TMP_Text lifeText;

    [SerializeField]
    private Button nextWaveButton;

    [SerializeField]
    private GameObject gameOverMenu;

    [Header("Attributes")]
    [SerializeField]
    private int currency = 100;

    [SerializeField]
    private float spawnCD = 1;

    [SerializeField]
    private int lifes = 5;
    private int wave = 0;
    private bool isGameOver = false;
    private List<Monster> activeMonsters = new();
    private Tower selectedTower;
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

    public int Lifes
    {
        get => lifes;
        set
        {
            lifes = value;
            if (lifes <= 0)
            {
                lifes = 0;
                GameOver();
            }
            lifeText.text = $"{lifes}";
        }
    }

    void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    void Start()
    {
        Lifes += 0;
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

    public void SelectTower(Tower tower)
    {
        selectedTower = tower;
        selectedTower.Select();
    }

    public void DeselectTower()
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
            selectedTower = null;
        }
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
            yield return new WaitForSeconds(spawnCD);
        }
    }

    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);
        if (isGameOver && activeMonsters.Count == 0)
        {
            LevelManager.Instance.GreenPortal.ClosePortal();
            LevelManager.Instance.PurplePortal.ClosePortal();
        }
        if (!IsWaveActive && !isGameOver)
        {
            nextWaveButton.gameObject.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            gameOverMenu.SetActive(true);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
