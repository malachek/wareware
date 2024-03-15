using UnityEngine;

public class runplayer : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 lastMousePosition;
    private Animator animator;

    private void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Mouse button pressed
        {
            lastMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) // While left mouse button is held down
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Set to 0 for 2D games

            // Calculate direction
            Vector3 direction = mousePosition - lastMousePosition;
            HandleAnimation(direction); // Handle the animation based on direction

            transform.position = mousePosition; // Move player
            lastMousePosition = mousePosition; // Update last position for next frame
        }
    }

    private void HandleAnimation(Vector3 direction)
    {
        if (direction.x < 0) // Moving left
        {
            animator.SetFloat("Direction", -1);
        }
        else if (direction.x > 0) // Moving right
        {
            animator.SetFloat("Direction", 1);
        }
        // Add more conditions for up and down if needed

        // Reset to idle if needed, by setting Direction to 0 when there's no movement
    }
}
