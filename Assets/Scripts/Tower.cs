using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TowerRange towerRange;
    private Monster target;
    private readonly List<Monster> monsters = new();

    void Update()
    {
        if (target != null)
        {
            Attack();
        }
        else
        {
            if (monsters.Count > 0)
            {
                target = monsters.First();
            }
        }
    }

    public void Select()
    {
        towerRange.ToggleVisible();
    }

    public void EnemyEnter(Monster monster)
    {
        monsters.Add(monster);
    }

    public void EnemyExit(Monster monster)
    {
        monsters.Remove(monster);
        target = null;
    }

    private void Attack() {
            
    }
}
