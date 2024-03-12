using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveYouClickedToday : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float m_Length;
    
    const string PRESS_ANIM = "clicked_screen";

    void OnMouseDown()
    {
        anim.SetBool(PRESS_ANIM, true);
    }
}
