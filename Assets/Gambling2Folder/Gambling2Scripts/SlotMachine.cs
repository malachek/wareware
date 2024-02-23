using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public Gambler gambler;
    public Sprite winscreen;
    public Sprite losescreen;
    private SpriteRenderer slotMachineImage;

    void Start()
    {
        slotMachineImage = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (gambler.gamewon)
        {
            slotMachineImage.sprite = winscreen;
        }
        else if (!gambler.gamewon)
        {
            slotMachineImage.sprite = losescreen;
        }
    }
}
