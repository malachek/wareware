using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTimer : MonoBehaviour, ITimeable
{
    public float countdownTime;

    public float GetTime()
    {
        return countdownTime;
    }

    private void Awake()
    {
        StartCoroutine(TickTime());
    }

    IEnumerator TickTime()
    {
        yield return new WaitForSeconds(countdownTime);
        GameStateManager.Lose();
    }
}
