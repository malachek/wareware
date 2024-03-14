
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroTimer : MonoBehaviour
{
    public Gambling2GM gm;
    float timeRemaining;
    [SerializeField] TextMeshProUGUI IntroTimerText;
    void Start()
    {
    }

    void Update()
    {
        timeRemaining = gm.time - 10f;
        int roundedTime = Mathf.RoundToInt(timeRemaining);
        IntroTimerText.text = roundedTime.ToString();
    }
}
