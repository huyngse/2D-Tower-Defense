using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover>
{
    [Header("References")]
    [SerializeField]
    private GameObject rangeRenderer;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (spriteRenderer.enabled)
        {
            FollowMouse();
        }
    }

    private void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void Activate(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
        rangeRenderer.SetActive(true);
        rangeRenderer.transform.localScale =
            5.6f
            * GameManager.Instance.ClickedButton.TowerPrefab.GetComponent<Tower>().Range
            * Vector3.one;
    }

    public void Deativate()
    {
        spriteRenderer.enabled = false;
        rangeRenderer.SetActive(false);
    }
}
