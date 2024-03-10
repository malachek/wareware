using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayToWin : MonoBehaviour
{

    public GameObject tipScreen;

    private GameObject display;

    private void Start()
    {
        display = this.transform.parent.gameObject;
    }

    public void Payment()
    {
        display.SetActive(false);
        tipScreen.SetActive(true);
    }
}
