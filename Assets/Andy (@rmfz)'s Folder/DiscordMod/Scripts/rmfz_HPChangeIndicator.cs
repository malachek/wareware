using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class rmfz_HPChangeIndicator : MonoBehaviour
{
    public GameObject indicatorObject;
    public TextMeshProUGUI indicatorText;
    public Coroutine fadeCoroutine;

    public rmfz_HPChangeIndicator(GameObject indicatorObject, TextMeshProUGUI indicatorText, Coroutine fadeCoroutine)
    {
        indicatorObject = this.indicatorObject;
        indicatorText = this.indicatorText;
        fadeCoroutine = this.fadeCoroutine;
    }
}
