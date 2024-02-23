using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberManager : MonoBehaviour
{
    public GameObject[] textBoxes;
    public float countdownTime;

    private int tbIndex = 0;
    private float counter = 0f;

    private TextMeshProUGUI currentBox;
    private TextBoxInfo currentBoxInfo;

    private void Awake()
    {
        StartCoroutine(TickTime());
    }

    // Start is called before the first frame update
    void Start()
    {
        currentBox = textBoxes[0].GetComponent<TextMeshProUGUI>();
        currentBoxInfo = currentBox.GetComponent<TextBoxInfo>();
    }

    public void UpdateNumber(string number)
    {
        if (counter >= currentBoxInfo.numberLimit)
        {
            if (tbIndex < textBoxes.Length - 1)
            {
                tbIndex++;

                currentBox = textBoxes[tbIndex].GetComponent<TextMeshProUGUI>();
                currentBoxInfo = currentBox.GetComponent<TextBoxInfo>();

                counter = 0;
            }
            else
            {
                //Won
                GameStateManager.Win();
                return;
            }
        }

        currentBox.text += number;
        counter++;
    }

    IEnumerator TickTime()
    {
        yield return new WaitForSeconds(countdownTime);
        GameStateManager.Lose();
    }
}
