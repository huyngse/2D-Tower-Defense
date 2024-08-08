using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    private Tower tower;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tower = transform.parent.gameObject.GetComponent<Tower>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            tower.EnemyEnter(other.GetComponent<Monster>());
        }
    }
    public void OnTriggerExit2D(Collider2D other) {
         if (other.CompareTag("Enemy"))
        {
            tower.EnemyExit(other.GetComponent<Monster>());
        }
    }

    public void ToggleVisible()
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }
}
