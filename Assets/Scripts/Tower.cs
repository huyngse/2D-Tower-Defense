using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject rangeRenderer;

    void Start() { }

    void Update() { }
    public void Select() {
        rangeRenderer.SetActive(!rangeRenderer.activeInHierarchy);
    }
}
