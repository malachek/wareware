using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTimer : MonoBehaviour
{
    public float countdownTime;

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
