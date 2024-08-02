using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TowerButton ClickedButton { get; private set; }

    public void PickTower(TowerButton towerButton) { 
        ClickedButton = towerButton;
        Hover.Instance.Activate(ClickedButton.Sprite);
    }
    public void BuyTower() {
        ClickedButton = null;
    }
}
