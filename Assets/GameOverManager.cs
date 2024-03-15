using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    GameObject LoseScreen;

    [SerializeField]
    GameObject LoseExitButton;
    [SerializeField]
    TextMeshProUGUI TextGameOver;
    [SerializeField]
    TextMeshProUGUI TextLoseScore;
    
    [SerializeField]
    GameObject WinScreen;
    [SerializeField]
    GameObject WinExitButton;
    [SerializeField]
    TextMeshProUGUI TextGameWin;
    [SerializeField]
    TextMeshProUGUI TextWinScore;

    private int score;
    private void Awake()
    {
        score = GameStateManager.score;

        if(score > 13)
        {
            LoseScreen.SetActive(false);
            WinScreen.SetActive(true);
            StartCoroutine(ShowWinUI());
        }
        else
        {
            LoseScreen.SetActive(true);
            WinScreen.SetActive(false);
            StartCoroutine(ShowLoseUI());
        }
    }


    public void ExitToMainMenu()
    {
        Debug.Log("EXITING");
        GameStateManager.RunItBack();
        SceneManager.LoadScene("START");
    }

    IEnumerator ShowWinUI()
    {
        yield return new WaitForSeconds(1f);
        TextGameWin.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        TextWinScore.gameObject.SetActive(true);
        TextWinScore.text = $"SCORE: {score}";
        yield return new WaitForSeconds(1f);
        WinExitButton.SetActive(true);
    }

    IEnumerator ShowLoseUI()
    {
        yield return new WaitForSeconds(1f);
        TextGameOver.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        TextLoseScore.gameObject.SetActive(true);
        TextLoseScore.text = $"SCORE: {score}";
        yield return new WaitForSeconds(1f);
        LoseExitButton.SetActive(true);
    }
}
