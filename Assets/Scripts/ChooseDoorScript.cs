using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseDoorScript : MonoBehaviour
{
    public Animator anim;
    public Text cardtext;

    private bool canchoose;

    void Start(){
        string[] choices = new string[] {"Your Mom", "Your Dad", "You", "Your dog", "Jeff Bezos", "Jeffery Epstein", "The Queen", "Obama", "Joe Biden", "Josh Hutcherson",
                                        "Your Ex", "Mary", "My grades", "Irvine", "Alex Thornton", "Irene Gassko", "League players", "JFK", "FDR",
                                        "Kim Jong Un", "Putin", "Trump", "bitches", "idfk lmao"};
        int choice  = Random.Range(0, choices.Length);
        cardtext.text = choices[choice];
        canchoose = true;

        StartCoroutine(timer());

    }

    // Update is called once per frame
    void Update()
    {
        if(canchoose && Input.GetKeyDown("left")){
            anim.SetTrigger("left");
            canchoose = false;
            GameStateManager.Win();
        }
        else if(canchoose && Input.GetKeyDown("right")){
            anim.SetTrigger("right");
            canchoose = false;
            GameStateManager.Win();
        }
    }

    private IEnumerator timer(){
        yield return new WaitForSeconds(5);
        GameStateManager.Lose();
    }

}
