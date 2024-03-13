using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTimer : MonoBehaviour
{
    public float countdownTime;

    public bool wonGame = false;

    private void Awake()
    {
        StartCoroutine(TickTime());
    }

    IEnumerator TickTime()
    {
        yield return new WaitForSeconds(countdownTime);
        if (!wonGame) GameStateManager.Lose();
    }

    public void GameWon()
    {
        wonGame = true;
    }
}
