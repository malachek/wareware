using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//This script is written by Andy (@rmfz)
//Please contact me if you make any changes, or need to discuss something in this script!

//this is the most shitty code i've written so far lmfaoooo
//(i got lazy and din't feel like using my brain)
public class rmfz_RunSocialCreditGame : MonoBehaviour
{
    [SerializeField] int timerLength;
    [SerializeField] Image correctAnswerImage;
    [SerializeField] Button correctAnswerButton;

    [SerializeField] Image countdownCircle;
    [SerializeField] TextMeshProUGUI countdownText;

    [Header("Icons for each answer button")]
    [SerializeField] Image triangleImage;
    [SerializeField] Image diamondImage;
    [SerializeField] Image circleImage;
    [SerializeField] Image squareImage;

    [Header("Button references")]
    [SerializeField] Button triangleButton;
    [SerializeField] Button diamondButton;
    [SerializeField] Button circleButton;
    [SerializeField] Button squareButton;

    [Header("Icons for correct/wrong answers")]
    [SerializeField] Image checkmarkImage;
    [SerializeField] Image wrongXImage;

    Coroutine countDownCoroutine;

    private void Start()
    {
        countDownCoroutine = StartCoroutine(CountDown());
    }

    public void SelectTriangle(bool isCorrect)
    {
        if (countDownCoroutine != null)
        {
            StopCoroutine(countDownCoroutine);
        }
        HandleWinOrLoss();
        if(isCorrect == false)
        {
            triangleButton.interactable = true;
            triangleButton.enabled = false;
        }
    }

    public void SelectDiamond(bool isCorrect)
    {
        if(countDownCoroutine != null)
        {
            StopCoroutine(countDownCoroutine);
        }
        HandleWinOrLoss();
        if(isCorrect == false)
        {
            diamondButton.interactable = true;
            diamondButton.enabled = false;
        }
    }

    public void SelectCircle(bool isCorrect)
    {
        if(countDownCoroutine != null)
        {
            StopCoroutine(countDownCoroutine);
        }
        HandleWinOrLoss();
        if(isCorrect == false)
        {
            circleButton.interactable = true;
            circleButton.enabled = false;
        }
    }

    public void SelectSquare(bool isCorrect)
    {
        if(countDownCoroutine != null)
        {
            StopCoroutine(countDownCoroutine);
        }
        HandleWinOrLoss();
        if(isCorrect == false)
        {
            squareButton.interactable = true;
            squareButton.enabled = false;
        }
    }

    /// <summary>
    /// Automatically selected when timer runs out.
    /// </summary>
    public void SelectNothing()
    {
        if(countDownCoroutine != null)
        {
            StopCoroutine(countDownCoroutine);
        }
        HandleWinOrLoss();
    }

    /// <summary>
    /// Handles the winning animations and whatnot
    /// </summary>
    public void WinSequence()
    {

    }

    /// <summary>
    /// Handles the losing stuff and whatnot
    /// </summary>
    public void LoseSequence()
    {

    }

    /// <summary>
    /// Changes the images of the triangle, diamond, circle, or square into wrong/checkmarks. Also handles buttons n shit
    /// </summary>
    public void HandleWinOrLoss()
    {
        triangleImage.sprite = wrongXImage.sprite;
        diamondImage.sprite = wrongXImage.sprite;
        circleImage.sprite = wrongXImage.sprite;
        squareImage.sprite = wrongXImage.sprite;
        correctAnswerImage.sprite = checkmarkImage.sprite;

        triangleButton.interactable = false;
        diamondButton.interactable = false;
        circleButton.interactable = false;
        squareButton.interactable = false;

        correctAnswerButton.interactable = true;
        correctAnswerButton.enabled = false;
    }

    /// <summary>
    /// Runs the timer and makes thing work until an answer is selected
    /// </summary>
    /// <returns></returns>
    IEnumerator CountDown()
    {
        float loadingCircleStartValue = timerLength;

        while (timerLength > 0)
        {
            countdownText.text = $"{timerLength}"; //update text

            //calculate how much to fill the loading circle n shi
            float elapsedTime = Time.deltaTime;
            float startFill = (timerLength / loadingCircleStartValue);
            float endFill = ((timerLength - 1) / loadingCircleStartValue);

            while (elapsedTime < 1)
            {
                elapsedTime += Time.deltaTime;
                countdownCircle.fillAmount = Mathf.Lerp(startFill, endFill, elapsedTime);
                yield return null;
            }

            countdownCircle.fillAmount = ((float)timerLength / loadingCircleStartValue); ;
            timerLength--;
        }

        countdownCircle.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);
        SelectNothing();
    }
}
