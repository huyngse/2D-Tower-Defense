using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Image content;

    [Header("Attributes")]
    [SerializeField]
    private float fillAmount = 1;

    [SerializeField]
    private float lerpSpeed = 2;

    [SerializeField]
    private Color fullColor = Color.red;

    [SerializeField]
    private Color lowColor = Color.red;

    [SerializeField]
    private bool lerpColor = false;

    public float MaxValue { get; set; }
    public float Value
    {
        get { return fillAmount; }
        set { fillAmount = Map(value, 0, MaxValue, 0, 1); }
    }

    void Start()
    {
        content.color = fullColor;
    }

    void Update()
    {
        content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, lerpSpeed * Time.deltaTime);
        if (lerpColor)
        {
            content.color = Color.Lerp(lowColor, fullColor, fillAmount);
        }
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
