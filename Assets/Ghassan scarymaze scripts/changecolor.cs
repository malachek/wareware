using UnityEngine;

public class changecolor : MonoBehaviour
{
    private SpriteRenderer squareSpriteRenderer; // To change the color of the square

    void Start()
    {
        // Assuming the square GameObject is tagged as "bird"
        GameObject square = GameObject.FindGameObjectWithTag("bird");
        if (square != null)
        {
            squareSpriteRenderer = square.GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        FollowMouse();

        // Check for left-click
        if (Input.GetMouseButtonDown(0)) // Changed from 1 to 0 for left-click
        {
            ChangeColorIfOverlapping();
        }
    }

    private void FollowMouse()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
    }

    private void ChangeColorIfOverlapping()
    {
        // Perform a check to see if this GameObject is overlapping the square
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f); // Adjust the radius as needed
        foreach (var hit in hits)
        {
            if (hit.tag == "bird") // The tag check is consistent with your setup
            {
                squareSpriteRenderer.color = Color.red; // Change the square's color to red
                break; // Exit the loop once the square is found and colored
            }
        }
    }
}

