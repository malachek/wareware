using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public CoinCount cm;
    public AudioSource audioPlayer;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("coin"))
        {
            cm.CoinAmount++;
            audioPlayer.Play();
            Destroy(other.gameObject);

            if (cm.CoinAmount >= 2)
            {
                GameStateManager.Win();
            }
        }
    }
}
