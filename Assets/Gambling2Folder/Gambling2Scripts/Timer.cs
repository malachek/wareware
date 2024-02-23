using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    [SerializeField] TextMeshProUGUI timerText;
    void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                int roundedTime = Mathf.RoundToInt(timeRemaining);
                timerText.text = roundedTime.ToString();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
}

