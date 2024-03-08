using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class G1Button : MonoBehaviour
{
    public Button mybutton;
    public RectTransform rectTransform;
    public float minX, maxX, minY, maxY;

    public float time = 10f;

    public bool gamewon;

    public Image KObar;


    void Start()
    {
        Button btn = mybutton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        KObar.fillAmount = 0;
    }
    private void Update()
    {
        time -= Time.deltaTime;
        if (KObar.fillAmount >= 1.0f && time >= 0)
        {
            gamewon = true;
            GameStateManager.Win();

        }
        else if (KObar.fillAmount < 1.0f && time <= 0)
        {
            gamewon = false;
            GameStateManager.Lose();
        }

    }
    void TaskOnClick()
    {
        KObar.fillAmount += 0.15f;
        Vector2 newPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        rectTransform.anchoredPosition = newPos;
    }
}