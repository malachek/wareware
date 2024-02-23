using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBoxInfo : MonoBehaviour
{
    public bool isTyping = false;
    public float numberLimit = 1f;

    private TextMeshProUGUI thisText;
    private void Start()
    {
        thisText = GetComponent<TextMeshProUGUI>();
        thisText.text = "";
    }
}
