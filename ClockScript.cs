using UnityEngine;
using UnityEngine.UI;
using System;

public class Clock : MonoBehaviour
{
    public Text clockText;

    void Update()
    {
        clockText.text = DateTime.Now.ToString("HH:mm:ss");
    }
}
