using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveYouClickedToday : MonoBehaviour
{
    [SerializeField] Animator anim;
    
    const string PRESS_ANIM = "clicked_screen";

    void Start()
    {
        // targetGameObject.transform.position = Camera.main.ScreenToWorldPoint( Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane) );
    }

    void OnMouseDown()
    {
        anim.SetBool(PRESS_ANIM, true);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null) {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.name == "nuke7")
            {
                IsClicked.mary_headshot = false;
                
                anim.SetBool(PRESS_ANIM, true);
                
                GameStateManager.Lose();
            }
        }
    }
}
