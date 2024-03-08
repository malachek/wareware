using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gambling2GM : MonoBehaviour
{
    public Sprite GiveMoney;
    public Sprite HoldMoney;
    public Image GambleBar;

    public float time = 10f;
    public float spriteChangeDuration = 1.0f;
    public bool gamewon;

    public SpriteRenderer slotmachine;
    public Sprite winscreen;
    public Sprite losescreen;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        GambleBar.fillAmount = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            GameStateManager.Lose();
        }

        if (Input.GetKeyDown(KeyCode.A) && time >= 0)
        {
            StartCoroutine(ChangeSpriteForDuration(GiveMoney, spriteChangeDuration));
            if (GambleBar.fillAmount < 1.0f)
            {
                StartCoroutine(FillGambleBar());
            }
        }
    }
    private IEnumerator ChangeSpriteForDuration(Sprite newSprite, float duration)
    {
        spriteRenderer.sprite = newSprite;
        yield return new WaitForSeconds(duration);
        spriteRenderer.sprite = HoldMoney;
    }
    private IEnumerator FillGambleBar()
    {
        yield return new WaitForSeconds(0.5f);
        GambleBar.fillAmount += 0.06f;
    }
}