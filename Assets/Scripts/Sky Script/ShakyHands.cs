using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakyHands : MonoBehaviour
{

    public float shakeAmt = 0f;
    public float shakeSpeed = 1f;
    public bool shaking = true;

    private Vector3 startingPos;
    private Vector3 newPos;
    private RectTransform thisTransform;

    private void Awake()
    {
        thisTransform = GetComponent<RectTransform>();

        startingPos.x = thisTransform.position.x;
        startingPos.y = thisTransform.position.y;
    }

    private void Update()
    {

        if (shaking)
        {
            newPos = startingPos;

            newPos.x = startingPos.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmt;
            newPos.y = startingPos.y + Mathf.Sin(Time.time * shakeSpeed) * shakeAmt;

            thisTransform.position = newPos;
        }
    }
}
