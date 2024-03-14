
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerText : MonoBehaviour
{
    public Gambling2GM gm;
    float timeRemaining;
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
            timeRemaining = gm.time;
            if (timeRemaining > 0)
            {
                if (gm.gamewon)
                {
                    timerText.text = "Nice job, goober";
                }
                else
                {
                    int roundedTime = Mathf.RoundToInt(timeRemaining);
                    timerText.text = roundedTime.ToString();
                }
            }
            else if (timeRemaining <= 0 && !gm.gamewon)
            {
                timerText.text = "No bazillion bucks for you, goober";
                timerIsRunning = false;
            }
        }
    }
}
