using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static GameStateManager;

public class GameStateManager : MonoBehaviour
{
    [Header("CSGO_Snipe")]
    [Tooltip("If you have a sequential series of minigames, only put the first as '{name} 0'")]
    
    [SerializeField]
    List<string> m_Minis = new List<string>();

    //================================//

    [Header("Stylistic Stuff")]
    
    [SerializeField]
    int m_StartingLives;

    [SerializeField]
    float m_TimeScalingFactor;
    float m_CurrentTimeScale = 1.0f;

    [SerializeField]
    List<AudioClip> m_WinSound, m_LoseSound, m_GameOverGoodSound, m_GameOverBadSound;

    //================================//

    [Header("Keep off !!")]
    
    [SerializeField]
    AudioSource m_SFXAudioSource;

    [SerializeField]
    AudioSource m_MusicAudioSource;

    [SerializeField]
    string m_TitleSceneName, m_MainSceneName;

    //================================//

    public static int score = 0;

    static int m_MiniCount;
    public static int m_CurrentLives;

    public static int m_CurrentMiniIndex;
    public static string m_CurrentMiniName;

    static GameStateManager _instance;

    enum GAMESTATE
    {
        MENU,
        MAIN,
        LOADING,
        PLAYING,
        PAUSED,
        GAMEOVER
    }

    static GAMESTATE m_State;

    public delegate void MiniInit();
    public delegate IEnumerator GameOverDelegate();

    public static GameOverDelegate OnGameOver;
    public static MiniInit OnMiniInit;

    private void Awake()
    {
        Debug.Log("Awake method called in " + gameObject.name);
        if(_instance == null)
        {
            _instance = this;
            m_CurrentLives = 0;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this);
        }
    }

    
    public static void NewGame()
    {
        Debug.Log("NEW GAME");

        _instance.m_CurrentTimeScale = 1.0f;
        Time.timeScale = 1.0f;
        _instance.m_SFXAudioSource.Stop();
        _instance.m_SFXAudioSource.pitch = 1.0f;
        _instance.m_MusicAudioSource.pitch = 1.0f;

        m_State = GAMESTATE.MAIN;
        m_MiniCount = _instance.m_Minis.Count;
        m_CurrentLives = _instance.m_StartingLives;
        if (_instance.m_Minis.Count > 0)
        {
            SceneManager.LoadScene(_instance.m_MainSceneName);
            if (OnMiniInit != null)
            {
                OnMiniInit();
            }
        }
    }

    public static void LoadMini()
    {
        m_CurrentMiniIndex = Random.Range(0, m_MiniCount);
        m_CurrentMiniName = _instance.m_Minis[m_CurrentMiniIndex];

        Debug.Log($"Loading Mini Game # {m_CurrentMiniIndex} - {m_CurrentMiniName}");

        m_State = GAMESTATE.LOADING;
        //coroutine with animation
        SceneManager.LoadScene(m_CurrentMiniName);
        m_State = GAMESTATE.PLAYING;
    }

  
    public static void Win()
    {
        Debug.Log("WIN");

        //checks if game is part of a series
        int nameLength = m_CurrentMiniName.Length;
        char lastChar = m_CurrentMiniName[nameLength - 1];
        if (lastChar >= '1' && lastChar <= '9')
        {
            int lastDigit = (int)lastChar - 48;
            if (lastDigit != 1)
            {
                _instance.m_Minis[m_CurrentMiniIndex] = m_CurrentMiniName.Substring(0, nameLength - 1) + (lastDigit - 1);
            }
        }

        score++;

        AudioClip randomClip = _instance.m_WinSound[Random.Range(0,_instance.m_WinSound.Count)];
        _instance.m_SFXAudioSource.clip = randomClip;
        _instance.m_SFXAudioSource.Play();

        _instance.m_CurrentTimeScale *= (1 + _instance.m_TimeScalingFactor);
        Time.timeScale = _instance.m_CurrentTimeScale;
        _instance.m_MusicAudioSource.pitch = _instance.m_CurrentTimeScale; ;
        _instance.m_SFXAudioSource.pitch = _instance.m_CurrentTimeScale;

        m_State = GAMESTATE.LOADING;
        //coroutine with animation
        m_State = GAMESTATE.MAIN;

        SceneManager.LoadScene(_instance.m_MainSceneName);
    }

    public static void Lose()
    {
        Debug.Log("LOSE");

        AudioClip randomClip = _instance.m_LoseSound[Random.Range(0, _instance.m_LoseSound.Count)];
        _instance.m_SFXAudioSource.clip = randomClip;
        _instance.m_SFXAudioSource.Play();

        m_State = GAMESTATE.LOADING;
        //coroutine with animation
        m_State = GAMESTATE.MAIN;

        SceneManager.LoadScene(_instance.m_MainSceneName);

        LoseLife();
    }

    public static void GameOver()
    {
        Debug.Log("GAME OVER");

        AudioClip randomClip = null;
        if (score > 10)
        {
            randomClip = _instance.m_GameOverGoodSound[Random.Range(0, _instance.m_GameOverGoodSound.Count)];
        }
        else
        {
            randomClip = _instance.m_GameOverBadSound[Random.Range(0, _instance.m_GameOverBadSound.Count)];
        }
        _instance.m_SFXAudioSource.clip = randomClip;
        _instance.m_SFXAudioSource.Play();

        m_State = GAMESTATE.GAMEOVER;
        SceneManager.LoadScene(_instance.m_TitleSceneName);
    }

    public static void LoseLife()
    {
        m_CurrentLives--;
        if (m_CurrentLives <= 0)
        {
            GameOver();
        }
    }

    public static void TogglePause()
    {
        if (m_State == GAMESTATE.PLAYING)
        {
            m_State = GAMESTATE.PAUSED;
            Time.timeScale = 0;
        }
        else if (m_State == GAMESTATE.PAUSED)
        {
            m_State = GAMESTATE.PLAYING;
            Time.timeScale = _instance.m_CurrentTimeScale;
        }
    }
}
