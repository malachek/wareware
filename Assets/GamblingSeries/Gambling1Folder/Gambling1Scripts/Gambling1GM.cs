using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Gambling1GM : MonoBehaviour, ITimeable
{
    public RectTransform rectTransform;
    public float minX, maxX, minY, maxY;

    public float time = 8.0f;

    public bool gamewon;

    public Image KObar;

    public SpriteRenderer Boxer;

    public Sprite BoxerDefeated;
    public Sprite BoxerIdle;
    public Sprite BoxerHurt;
    public Sprite BoxerWins;

    public Button btn;

    private AudioSource _audiosource;

    public TextMeshProUGUI DeathText;

    public TextMeshProUGUI WinText;

    void Start()
    {
        DeathText.gameObject.SetActive(false);
        WinText.gameObject.SetActive(false);
        btn.onClick.AddListener(TaskOnClick);
        _audiosource = GetComponent<AudioSource>();
        KObar.fillAmount = 1;
        StartCoroutine(TickTime());
    }
    private void Update()
    {
        time -= Time.deltaTime;
        if (KObar.fillAmount <= 0.0f && time >= 0)
        {
            WinText.gameObject.SetActive(true);
            Boxer.sprite = BoxerDefeated;
            Boxer.transform.position = new Vector3(0, -5, 0);
            gamewon = true;
            GameStateManager.Win();

        }
        else if (KObar.fillAmount > 0.0f && time <= 0)
        {
            Boxer.sprite = BoxerWins;
            DeathText.gameObject.SetActive(true);
            Boxer.transform.position = new Vector3(0, -1.5f, 0);
            gamewon = false;
            GameStateManager.Lose();
        }

    }

    public float GetTime()
    {
        return time;
    }
    void TaskOnClick()
    {
        if (time > 0)
        {
            KObar.fillAmount -= 0.22f;
            _audiosource.Play();
            Vector2 newPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            rectTransform.anchoredPosition = newPos;
            StartCoroutine(Boxerdamaged());
        }
    }
    private IEnumerator Boxerdamaged()
    {
        Boxer.sprite = BoxerHurt;
        yield return new WaitForSeconds(0.2f);
        Boxer.sprite = BoxerIdle;
    }
    IEnumerator TickTime()
    {
        yield return new WaitForSeconds(time);
        GameStateManager.Lose();
    }
}