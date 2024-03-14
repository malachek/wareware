using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsClicked : MonoBehaviour
{
    public static bool mary_headshot = false;

    [SerializeField] Animator anim;
    public float m_Length = 5.0f;
    const string PRESS_ANIM = "clicked_screen";
    public bool clicked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        m_Length -= Time.deltaTime;

        if (m_Length <= 0.0f)
        {
            GameStateManager.Lose();
        }
    }

    void OnMouseDown()
    { 
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null) {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.name == "head_hitbox")
            {
                mary_headshot = true;
                
                anim.SetBool(PRESS_ANIM, true);
            }

            if (mary_headshot == false)
            {
                GameStateManager.Lose();
            }
            
            
        }
        
    }
}
