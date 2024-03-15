using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WakeyWakey : MonoBehaviour
{
    public Image joeBackground;
    public Sprite sleepyJoeSprite;
    public GameObject zzzAnimation;

    [Space(10)]
    [Header("Timers xD")]

    public float wakeUpTime = 1f;
    public float endGameTime = 0.5f;

    private AudioQueue audioController;

    private void Start()
    {
        audioController = GetComponentInParent<AudioQueue>();
    }

    public IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(wakeUpTime);

        audioController.JoeSound();
        joeBackground.sprite = sleepyJoeSprite;

        zzzAnimation.SetActive(false);

        yield return new WaitForSeconds(endGameTime);

        GameStateManager.Win();
    }
}
