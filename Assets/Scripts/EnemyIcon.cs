using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIcon : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Image image;
    void Start()
    {

    }

    void Update()
    {

    }
    public void SetColor(Color color)
    {
        image.color = color;
    }
}
