using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    private TMP_Text tooltipText;

    [SerializeField]
    private Button nextWaveButton;

    [SerializeField]
    private GameObject gameOverMenu;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject upgradePannel;

    [SerializeField]
    private GameObject statsPannel;

    [SerializeField]
    private TMP_Text sellText;

    [SerializeField]
    private BuyButton upgradeButton;

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

    public int Wave
    {
        get => wave;
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
        HandleInputs();
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

    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ClickedButton != null)
            {
                ClickedButton = null;
                Hover.Instance.Deativate();
            } else {
                ShowPauseMenu();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            ClickedButton = null;
            Hover.Instance.Deativate();
        }
    }

    public void OnCurrencyChanged()
    {
        CurrencyChanged?.Invoke();
    }

    public void PickTower(TowerButton towerButton)
    {
        // if (IsWaveActive)
        //     return;
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
        UpdateUpgradeTooltip();
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

    public void SetTooltipText(string txt)
    {
        tooltipText.text = txt;
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        bool isPaused = pauseMenu.activeSelf;
        if (isPaused) {
            Time.timeScale = 0;
            DeselectTower();
        } else {
            Time.timeScale = 1;
        }
    }

    public void ShowStats()
    {
        statsPannel.SetActive(!statsPannel.activeSelf);
    }

    public void ShowSelectedTowerStats()
    {
        statsPannel.SetActive(!statsPannel.activeSelf);
        UpdateUpgradeTooltip();
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();
        for (int i = 0; i < Mathf.Clamp(wave, 0, 15); i++)
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

    public void StartWave()
    {
        wave++;
        waveText.text = $"Wave: <color=green>{wave}</color>";
        StartCoroutine(SpawnWave());
        nextWaveButton.gameObject.SetActive(false);
    }

    public void UpdateUpgradeTooltip()
    {
        if (selectedTower == null)
            return;
        SetTooltipText(selectedTower.GetStats());
        if (selectedTower.NextUpgrade != null)
        {
            upgradeButton.Text = selectedTower.NextUpgrade.Price + "$";
        }
        else
        {
            upgradeButton.Text = "MAX";
        }
    }

    public void UpgradeTower()
    {
        if (
            selectedTower == null
            || selectedTower.NextUpgrade == null
            || currency < selectedTower.NextUpgrade.Price
        )
            return;
        selectedTower.Upgrade();
        if (selectedTower.NextUpgrade != null)
        {
            upgradeButton.Price = selectedTower.NextUpgrade.Price;
        }
        sellText.text = selectedTower.SellPrice + "$";
        UpdateUpgradeTooltip();
    }
}
