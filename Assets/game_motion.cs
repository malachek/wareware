using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_motion : MonoBehaviour
{

    [SerializeField] float scaleability = 1f;
    [SerializeField] float displacement_scale_y = 1.2f;
    [SerializeField] float displacement_scale_x = 1.15f;
    [SerializeField] float m_Length;

    private float halfed_width;
    private float halfed_height;
    private float pos_x;
    private float pos_y;
    

    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        halfed_height = Screen.height / 2;
        halfed_width = Screen.width / 2;

        pos_y = (Random.Range(0,Screen.height) - halfed_height * displacement_scale_y ) /  halfed_height * scaleability;
        pos_x = (Random.Range(0,Screen.width) - halfed_width * displacement_scale_x) / halfed_width * scaleability * Screen.width/Screen.height;
        
        // need to store the value so it goes in a certain direction
        // the moving will happen in the update
        // need to create a bool that updates if head_hitbox is clicked on

        transform.position = new Vector3(pos_x, pos_y, 0);
        Debug.Log(pos_x + " .. " + pos_y);
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = new Vector3(pos_x, pos_y, 0);
    }

    IEnumerator TickTime()
    {
        yield return new WaitForSeconds(m_Length);
        GameStateManager.Lose();
    }
}
