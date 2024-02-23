using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public MoneyHand MH;
    public Sprite winscreen;
    public Sprite losescreen;
    private SpriteRenderer slotMachineImage;

    void Start()
    {
        slotMachineImage = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (MH.gamewon)
        {
            slotMachineImage.sprite = winscreen;
        }
        else if (MH.gamewon)
        {
            slotMachineImage.sprite = losescreen;
        }
    }
}
