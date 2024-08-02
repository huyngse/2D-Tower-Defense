using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TowerButton ClickedButton { get; private set; }
    void Update() {
        HandleCancel();
    }

    public void PickTower(TowerButton towerButton) { 
        ClickedButton = towerButton;
        Hover.Instance.Activate(ClickedButton.Sprite);
    }
    public void BuyTower() {
        ClickedButton = null;
        Hover.Instance.Deativate();
    }
    private void HandleCancel() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ClickedButton = null;
            Hover.Instance.Deativate();
        }
    }
}
