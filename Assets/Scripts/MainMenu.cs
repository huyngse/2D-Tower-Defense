using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject selectLevelMenu;

    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private GameObject background;
    private float bgSpeed = 20;

    void Start()
    {
        background.GetComponent<Rigidbody2D>().velocity = Vector3.one * bgSpeed;
    }

    void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenu.activeSelf)
            {
                ShowMenu();
            }
            else
            {
                Quit();
            }
        }
    }

    public void Play(int scene)
    {
        SoundManager.Instance.PlayEffect("click");
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        SoundManager.Instance.PlayEffect("click");
        Application.Quit();
    }
    public void ShowSelectLevelMenu()
    {
        menu.SetActive(false);
        selectLevelMenu.SetActive(true);
        SoundManager.Instance.PlayEffect("click");
    }
    public void ShowOptionsMenu()
    {
        menu.SetActive(false);
        optionsMenu.SetActive(true);
        SoundManager.Instance.PlayEffect("click");
    }

    public void ShowMenu()
    {
        optionsMenu.SetActive(false);
        selectLevelMenu.SetActive(false);
        menu.SetActive(!menu.activeSelf);
        SoundManager.Instance.PlayEffect("click");
    }
}
