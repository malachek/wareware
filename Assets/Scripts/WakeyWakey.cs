using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WakeyWakey : MonoBehaviour
{
    public Image joeBackground;
    public Sprite sleepyJoeSprite;

    public float wakeUpTime = 1f;

    public IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(wakeUpTime);

        joeBackground.sprite = sleepyJoeSprite;
    }
}
