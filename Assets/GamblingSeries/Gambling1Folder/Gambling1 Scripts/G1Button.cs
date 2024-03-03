using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class G1Button : MonoBehaviour
{
    public Button mybutton;
    public RectTransform rectTransform;
    public float minX, maxX, minY, maxY;

    void Start()
    {
        Button btn = mybutton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Vector2 newPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        rectTransform.anchoredPosition = newPos;
    }
}