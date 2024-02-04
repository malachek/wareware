using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainManger : MonoBehaviour
{

    [SerializeField]
    AudioSource m_CountDownAudioSource;
    
    [SerializeField]
    float m_CountDownBeatTime;

    [SerializeField]
    List<AudioClip> m_Counts;
    
    [SerializeField]
    TextMeshProUGUI m_TextLives;
    
    private void Awake()
    {
        Debug.Log("YO");
        Debug.Log("Time scale - " + Time.timeScale);
        m_TextLives.text = GameStateManager.m_CurrentLives + " <3";
        StartCoroutine(CountDown());
        
    }
    public void OpenGame()
    {
        GameStateManager.LoadMini();
    }
    
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(m_CountDownBeatTime);
        for (int i = 3; i >= 1; i--)
        {
            m_CountDownAudioSource.clip = m_Counts[i];
            m_CountDownAudioSource.Play();
            Debug.Log(i + "...");
            yield return new WaitForSeconds(m_CountDownBeatTime);
        }
        m_CountDownAudioSource.clip = m_Counts[0];
        m_CountDownAudioSource.Play();
        Debug.Log("GO!");
        OpenGame();
    }
}
