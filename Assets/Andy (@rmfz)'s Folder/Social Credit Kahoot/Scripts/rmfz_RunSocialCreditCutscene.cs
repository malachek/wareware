using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This script is written by Andy (@rmfz)
//Please contact me if you make any changes, or need to discuss something in this script!
public class rmfz_RunSocialCreditCutscene : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    [SerializeField] private int countdownSecs = 5;
    [SerializeField] private string nextSceneName;
    [SerializeField] private Image loadingCircle;

    private void Start()
    {
        StartCoroutine(StartCutsceneSequence(nextSceneName));
    }

    /// <summary>
    /// Begins the countdown, the circle loading thingy, and whatnot.
    /// </summary>
    IEnumerator StartCutsceneSequence(string nextSceneName)
    {
        float loadingCircleStartValue = countdownSecs;

        while(countdownSecs > 0)
        {
            timerText.text = $"{countdownSecs}"; //update text

            //calculate how much to fill the loading circle n shi
            float elapsedTime = Time.deltaTime;
            float startFill = 1 - (countdownSecs / loadingCircleStartValue);
            float endFill = 1 - ((countdownSecs - 1) / loadingCircleStartValue);

            while(elapsedTime < 1)
            {
                elapsedTime += Time.deltaTime;
                loadingCircle.fillAmount = Mathf.Lerp(startFill, endFill, elapsedTime);
                yield return null;
            }

            loadingCircle.fillAmount = 1 - ((float) countdownSecs / loadingCircleStartValue); ;
            countdownSecs--;
        }

        loadingCircle.fillAmount = 1;
        SceneManager.LoadScene(nextSceneName);
    }
}
