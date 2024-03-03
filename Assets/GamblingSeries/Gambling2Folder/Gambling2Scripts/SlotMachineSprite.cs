using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineSprite : MonoBehaviour
{
    public Gambling2GM gm;
    public Sprite winscreen;
    public Sprite losescreen;
    private SpriteRenderer slotMachineImage;

    void Start()
    {
        slotMachineImage = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (gm.gamewon)
        {
            slotMachineImage.sprite = winscreen;
        }
        else if (!gm.gamewon)
        {
            slotMachineImage.sprite = losescreen;
        }
    }
}
