using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAnimation : MonoBehaviour
{
    Animator anim;
    const string PRESS_ANIM = "clicked_screen";
    const string CLICKED_HEAD = "hit_head";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsClicked.mary_headshot == true)
        {
            anim.SetBool(CLICKED_HEAD, true);
        }
    }
}
