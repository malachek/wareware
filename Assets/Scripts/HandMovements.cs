using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandMovements : MonoBehaviour
{

    public float rotationSpeed = 50f;
    public float fastRotationSpeed = 100f;

    public float pullBackForce = 2f;

    [Space(10)]
    [Header("Right Hand Only")]
    public Sprite clapSprite;
    public GameObject emphasisEffect;
    public GameObject spaceBarDisplay;

    [Space(10)]
    [Header("Angle Triggers Win")]
    public float minWinAngle;
    public float maxWinAngle;

    [Space(10)]
    [Header("Angle Triggers Pause Movement")]
    public float minStopAngle;
    public float maxStopAngle;

    [Space(10)]
    [Header("Angle Triggers Clap Effects")]
    public float minClappedAngle;
    public float maxClappedAngle;

    [Space(20)]
    public bool allowRotate = true;
    public bool isRightHand = false;

    private Image handImage;
    private WakeyWakey wakeUpScript;

    private bool isClapping = false;
    private float zRotation = 0f;

    private AudioQueue audioController;

    private void Start()
    {
        handImage = GetComponentInChildren<Image>();

        audioController = GetComponentInParent<AudioQueue>();
    }

    // Update is called once per frame
    void Update()
    {
        zRotation = transform.rotation.eulerAngles.z;
        Debug.Log(zRotation);

        if (allowRotate)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            if ( ((zRotation <= minStopAngle) || (zRotation > maxStopAngle)) 
                && !isClapping)
            {
                allowRotate = false;
            }
            else if (isClapping &&
                ((zRotation >= minClappedAngle) && (zRotation <= maxClappedAngle)) )
            {
                Clap();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isClapping)
        {
            //Debug.Log("spaced");
            PullBack();
        }
    }

    private void PullBack()
    {
        Quaternion rota = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + pullBackForce);
        transform.rotation = rota;

        allowRotate = true;

        if ( (zRotation >= minWinAngle) && (zRotation <= maxWinAngle) )
        {
            StartClap();
        }
    }

    private void StartClap()
    {
        isClapping = true;
        rotationSpeed = fastRotationSpeed;

        if (isRightHand) spaceBarDisplay.SetActive(false);
    }

    private void Clap()
    {
        allowRotate = false;

        if (isRightHand)
        {
            handImage.sprite = clapSprite;

            audioController.ClapSound();
            emphasisEffect.SetActive(true);
            
            wakeUpScript = GetComponent<WakeyWakey>();
            StartCoroutine(wakeUpScript.WakeUp());
        }
    }

}