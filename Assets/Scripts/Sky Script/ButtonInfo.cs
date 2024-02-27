using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfo : MonoBehaviour
{

    public string buttonNumber = "0";

    private NumberManager display;

    private void Start()
    {
        display = GetComponentInParent<NumberManager>();
    }

    public void BruhButton()
    {
        Debug.Log(buttonNumber);
        display.UpdateNumber(buttonNumber);
    }
}
