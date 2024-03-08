using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerTextG1 : MonoBehaviour
{
    public G1Button gm;
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
                    timerText.text = "We have a winner?!?!?!";
                }
                else
                {
                    int roundedTime = Mathf.RoundToInt(timeRemaining);
                    timerText.text = roundedTime.ToString();
                }
            }
            else if (timeRemaining <= 0 && !gm.gamewon)
            {
                timerText.text = "Stand proud, ur bad";
                timerIsRunning = false;
            }
        }
    }
}
