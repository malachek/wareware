using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberManager : MonoBehaviour
{
    public GameObject[] textBoxes;

    [Header("Pay Button")]

    public GameObject payButton;

    private int tbIndex = 0;
    private float counter = 0f;

    private TextMeshProUGUI currentBox;
    private TextBoxInfo currentBoxInfo;


    // Start is called before the first frame update
    void Start()
    {
        currentBox = textBoxes[0].GetComponent<TextMeshProUGUI>();
        currentBoxInfo = currentBox.GetComponent<TextBoxInfo>();
    }

    public void UpdateNumber(string number)
    {
        currentBox.text += number;
        counter++;

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
                payButton.SetActive(true);
                return;
            }
        }
    }

}
