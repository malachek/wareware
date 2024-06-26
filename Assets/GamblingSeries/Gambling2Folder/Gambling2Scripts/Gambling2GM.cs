using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gambling2GM : MonoBehaviour, ITimeable
{
    public Sprite GiveMoney;
    public Sprite HoldMoney;
    public Image GambleBar;

    private float time = 8.0f;
    public bool gamewon;

    public SpriteRenderer slotmachine;
    public Sprite winscreen;
    public Sprite losescreen;

    public SpriteRenderer playerhand;

    private AudioSource _audiosource;

    public SpriteRenderer speechBubble;

    void Start()
    {
        GambleBar.fillAmount = 0;
        _audiosource = GetComponent<AudioSource>();
        _audiosource.Play();
        StartCoroutine(FlashSpeechBubble());
        StartCoroutine(TickTime());
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (GambleBar.fillAmount >= 1.0f && time >= 0)
        {
            gamewon = true;
            slotmachine.sprite = winscreen;
            GameStateManager.Win();

        }
        else if (GambleBar.fillAmount < 1.0f && time <= 0)
        {
            gamewon = false;
            slotmachine.sprite = losescreen;
        }

        if (Input.GetKeyDown(KeyCode.A) && time >= 0)
        {
            if (GambleBar.fillAmount < 1.0f)
            {
                StartCoroutine(FillGambleBar());
                StartCoroutine(ChangeSpriteForDuration());
            }
        }
    }

    public float GetTime()
    {
        return time;
    }
    private IEnumerator ChangeSpriteForDuration()
    {
        playerhand.sprite = GiveMoney;
        yield return new WaitForSeconds(1.0f);
        playerhand.sprite = HoldMoney;
    }
    private IEnumerator FillGambleBar()
    {
        yield return new WaitForSeconds(0.5f);
        GambleBar.fillAmount += 0.06f;
    }
    private IEnumerator FlashSpeechBubble()
    {
        while (true)
        {
            speechBubble.enabled = !speechBubble.enabled;

            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator TickTime()
    {
        yield return new WaitForSeconds(time);
        GameStateManager.Lose();
    }
}