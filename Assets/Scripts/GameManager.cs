using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public delegate void CurrencyChangeHandler();

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

    [SerializeField]
    private GameObject upgradePannel;

    [SerializeField]
    private TMP_Text sellText;

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
    private int enemyHealth = 12;
    public bool IsWaveActive
    {
        get { return activeMonsters.Count > 0; }
    }
    public TowerButton ClickedButton { get; private set; }
    public ObjectPool Pool { get; private set; }
    public event CurrencyChangeHandler CurrencyChanged;
    public int Currency
    {
        get => currency;
        set
        {
            currency = value;
            currencyText.text = value.ToString() + "<color=orange>$</color>";
            OnCurrencyChanged();
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

    public void BuyTower()
    {
        Currency -= ClickedButton.Price;
        ClickedButton = null;
        Hover.Instance.Deativate();
        DeselectTower();
    }

    public void DeselectTower()
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
            selectedTower = null;
            upgradePannel.SetActive(false);
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

    private void HandleCancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickedButton = null;
            Hover.Instance.Deativate();
        }
    }

    public void OnCurrencyChanged() {
        CurrencyChanged?.Invoke();
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

    public void Quit()
    {
        Application.Quit();
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

    public void Restart()
    {
        Time.timeScale = 1;
        AStar.ClearNode();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SelectTower(Tower tower)
    {
        selectedTower = tower;
        selectedTower.Select();
        upgradePannel.SetActive(true);
        sellText.text = selectedTower.SellPrice + "$";
    }

    public void SellTower()
    {
        Currency += selectedTower.SellPrice;
        TileScript tile = selectedTower.GetComponentInParent<TileScript>();
        tile.IsEmpty = true;
        GameObject particle = Pool.GetObject("Death Particle");
        particle.transform.position = tile.WorldPosition;
        Destroy(selectedTower.gameObject);
        selectedTower = null;
        upgradePannel.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        if (wave % 2 == 0)
        {
            enemyHealth += 5;
        }
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
            monster.Spawn(enemyHealth);
            activeMonsters.Add(monster);
            yield return new WaitForSeconds(spawnCD);
        }
    }

    public void StartWave()
    {
        wave++;
        waveText.text = $"Wave: <color=green>{wave}</color>";
        StartCoroutine(SpawnWave());
        nextWaveButton.gameObject.SetActive(false);
    }
}
