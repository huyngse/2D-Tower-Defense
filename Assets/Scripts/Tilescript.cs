using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilescript : MonoBehaviour
{
    public Point GridPosition { get; private set; }

    void Start() { }

    void Update() { }

    public void Setup(Point gridPosition)
    {
        GridPosition = gridPosition;
    }
}
