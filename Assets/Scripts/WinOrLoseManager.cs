using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinOrLoseGane : MonoBehaviour
{
    [SerializeField]
    float m_Length;
    private void Awake()
    {
        StartCoroutine(TickTime());
    }
    public static void Win()
    {
        GameStateManager.Win();
    }

    public static void Lose()
    {
        GameStateManager.Lose();
    }

    //PUT THIS METHOD IN YOUR MINI GAMES
    IEnumerator TickTime()
    {
        yield return new WaitForSeconds(m_Length);
        GameStateManager.Lose();
    }
}
