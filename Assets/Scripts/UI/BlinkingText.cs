using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class BlinkingText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    [SerializeField] 
    private float cycleTime = 1.0f;

    [SerializeField] 
    private Color colorA = Color.black;

    [SerializeField] 
    private Color colorB = Color.white;

    private void OnValidate()
    {
        if (text == null)
        {
            text = GetComponent<TMP_Text>();
        }
    }

    private void Update()
    {
        var timeSinceCycleStart = Time.time % cycleTime;
        var halfCycleTime = cycleTime / 2;
        var lerpTime = (Math.Abs(timeSinceCycleStart - halfCycleTime)) / halfCycleTime;
        
        float smoothTime = Mathf.SmoothStep(0.0f, 1.0f, lerpTime);
        
        text.color = Color.Lerp(colorA, colorB, smoothTime);
    }
}
