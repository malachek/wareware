using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Gambling1GM : MonoBehaviour
{
    public RectTransform rectTransform;
    public float minX, maxX, minY, maxY;

    public float time = 10f;

    public bool gamewon;

    public Image KObar;

    public SpriteRenderer Boxer;

    public Sprite BoxerDefeated;
    public Sprite BoxerIdle;
    public Sprite BoxerHurt;
    public Sprite BoxerWins;

    public Button btn;

    private AudioSource _audiosource;

    void Start()
    {
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
            Boxer.sprite = BoxerDefeated;
            Boxer.transform.position = new Vector3(0, -5, 0);
            gamewon = true;
            GameStateManager.Win();

        }
        else if (KObar.fillAmount > 0.0f && time <= 0)
        {
            Boxer.sprite = BoxerWins;
            Boxer.transform.position = new Vector3(0, -1.5f, 0);
            gamewon = false;
            GameStateManager.Lose();
        }

    }
    void TaskOnClick()
    {
        if (time > 0)
        {
            KObar.fillAmount -= 0.15f;
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