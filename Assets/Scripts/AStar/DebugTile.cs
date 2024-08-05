using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugTile : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TMP_Text fText;

    [SerializeField]
    private TMP_Text gText;

    [SerializeField]
    private TMP_Text hText;

    public TMP_Text FText
    {
        get => fText;
        set => fText = value;
    }
    public TMP_Text GText
    {
        get => gText;
        set => gText = value;
    }
    public TMP_Text HText
    {
        get => hText;
        set => hText = value;
    }

    void Start() { }

    void Update() { }
}
