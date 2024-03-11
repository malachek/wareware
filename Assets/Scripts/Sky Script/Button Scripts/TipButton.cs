using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipButton : MonoBehaviour
{
   public void TipRecieved()
    {
        GameStateManager.Win();
    }
}
