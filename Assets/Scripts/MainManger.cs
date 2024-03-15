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
    AudioSource m_BGMusic;
    
    [SerializeField]
    float m_CountDownBeatTime;

    [SerializeField]
    List<AudioClip> m_Counts;

    //[SerializeField]
    //TextMeshProUGUI m_TextLives;

    [SerializeField]
    List<GameObject> m_BloodSplats;

    [SerializeField]
    TextMeshProUGUI m_TextHints;
    
    [SerializeField]
    string[] m_RandomHints;

    [SerializeField]
    HealthHints[] m_LifeBasedHints;

    [SerializeField]
    Image m_Background;

    [SerializeField]
    List<Sprite> m_Backgrounds;

    
    private void Awake()
    {
        m_BGMusic.pitch = Time.timeScale;
        int lives = GameStateManager.m_CurrentLives;
        Debug.Log("Time scale - " + Time.timeScale);
        for(int i = 4 - lives; i >= 0; i--)
        {
            Debug.Log("health " + i);
            m_BloodSplats[i].SetActive(true);
            
        }

        m_Background.sprite = m_Backgrounds[Random.Range(0, m_Backgrounds.Count)];

        RandomHint(lives);

        if (lives <= 0)
        {
            StartCoroutine(NoLives());
            return;
        }

        StartCoroutine(CountDown());
        
    }
    public void OpenGame()
    {
        GameStateManager.LoadMini();
    }

    private void RandomHint(int lives)
    {
        bool lifeSensitive = Random.Range(0, (lives+1)/2 + 1) == 0; //more likely to be curated to lives when at lower health

        if(lifeSensitive)
        {
            m_TextHints.text = m_LifeBasedHints[lives].GetRandomHint();
        }
        else
        {
            m_TextHints.text = m_RandomHints[Random.Range(0, m_RandomHints.Length)];
        }


        //else

    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(m_CountDownBeatTime/2);
        for (int i = 3; i >= 1; i--)
        {
            m_CountDownAudioSource.clip = m_Counts[i];
            m_CountDownAudioSource.Play();
            Debug.Log(i + "...");
            yield return new WaitForSeconds(m_CountDownBeatTime);
        }
        m_CountDownAudioSource.clip = m_Counts[0];
        m_CountDownAudioSource.Play();
        yield return new WaitForSeconds(m_CountDownBeatTime);
        Debug.Log("GO!");
        OpenGame();
    }

    IEnumerator NoLives()
    {
        yield return new WaitForSeconds(3f);
        GameStateManager.GameOver();
    }
}

[System.Serializable]
public class HealthHints
{
    public string[] hints;
    
    public string GetRandomHint()
    {
        return hints[Random.Range(0, hints.Length)];
    }
}
