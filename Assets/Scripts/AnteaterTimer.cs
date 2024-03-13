using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnteaterTimer : MonoBehaviour
{
    Animator anim;
    private readonly float ANIMATION_DURATION = 1f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.gameObject.SetActive(false);
        GameStateManager.OnMiniInit += TimerEnabler;
        GameStateManager.OnMiniExit += TimerDisabler;
    }

    // Update is called once per frame

    void TimerEnabler(float miniTime)
    {
        Debug.Log("TimerEnabler");
        anim.gameObject.SetActive(true);
        anim.SetTrigger("StartTimer");
        Debug.Log(miniTime);
        anim.SetFloat("Speed", ANIMATION_DURATION / miniTime);
        //anim.Play("TimerAnimation");
    }
    void TimerDisabler()
    {
        Debug.Log("TimerDisabler");
        anim.gameObject.SetActive(false);
    }
}
