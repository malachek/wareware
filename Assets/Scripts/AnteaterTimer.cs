using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnteaterTimer : MonoBehaviour
{
    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.GetComponent<Animator>();
        anim.SetFloat("Speed", 1f);
        anim.gameObject.SetActive(false);
        GameStateManager.OnMiniInit += TimerEnabler;
        GameStateManager.OnMiniExit += TimerDisabler;
        Debug.Log(anim.name);
        Debug.Log(anim.GetType());

    }

    // Update is called once per frame
  
    void TimerEnabler()
    {
        Debug.Log("TimerEnabler");
        anim.gameObject.SetActive(true);
        anim.Play("TimerAnimation");
    }
    void TimerDisabler()
    {
        Debug.Log("TimerDisabler");
        anim.gameObject.SetActive(false);
    }
}
