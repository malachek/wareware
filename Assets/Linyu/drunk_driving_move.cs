using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drunk_driving_move : MonoBehaviour
{
    public float velocity = 1;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("d"))
        {
            rb.velocity = Vector2.down * velocity * 2;
        }
        if(Input.GetKeyDown("w"))
        {
            rb.velocity = Vector2.left * velocity * 2;
        }
        if(Input.GetKeyDown("a"))
        {
            rb.velocity = Vector2.right * velocity * 2;
        }
        if(Input.GetKeyDown("s"))
        {
            rb.velocity = Vector2.up * velocity * 2;
        }
    }
    
}
