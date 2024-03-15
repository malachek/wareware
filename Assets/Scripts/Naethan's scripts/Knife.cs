    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour, ITimeable
{
    public int count = 0;
    [SerializeField] float m_Length;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TickTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 5)
        {
            //Debug.Log("Win Scene!");
            GameStateManager.Win();
        }
    }

    public float GetTime()
    {
        return m_Length;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.name == "Finger")
        {
            //Debug.Log("End Scene");
            GameStateManager.Lose();
        }

        if (collider.gameObject.name == "Target")
        {
            Debug.Log("HIT");
            count++;
            Destroy(collider.gameObject);
        }
    }

    IEnumerator TickTime()
    {
        yield return new WaitForSeconds(m_Length);
        GameStateManager.Lose();
    }
}
