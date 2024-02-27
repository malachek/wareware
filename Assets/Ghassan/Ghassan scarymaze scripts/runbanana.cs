using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 previousPosition;
    private Animator animator; // Reference to the Animator component
    private bool canMove = true; // Flag to control player movement
    [SerializeField] private AudioClip loseSound; 
    [SerializeField] private AudioClip winSound; 
    private AudioSource audioSource;

    private void Start()
    {
        mainCamera = Camera.main;
        previousPosition = transform.position;
        animator = GetComponent<Animator>(); // Initializing the Animator reference here
        audioSource = GetComponent<AudioSource>(); // Ensure there is an AudioSource component attached to it
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && canMove) // Check canMove flag before moving
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Set to 0 for 2D games
            Vector3 moveDirection = mousePosition - previousPosition;

            // Only move the player if canMove is true
            transform.position = mousePosition;

            // Determine the direction of movement for sprite flipping
            if (moveDirection.x < 0 && transform.localScale.x < 0)
            {
                FlipSprite();
            }
            else if (moveDirection.x > 0 && transform.localScale.x > 0)
            {
                FlipSprite();
            }

            // Updating previousPosition for the next frame
            previousPosition = mousePosition;
        }
    }

    private void FlipSprite()
    {
        // Flip the sprite 
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void TriggerWin()
    {
        StartCoroutine(WinGame()); // this is to Start the WinGame coroutine
    }

    private IEnumerator WinGame()
    {
        animator.SetBool("Win", true);
        canMove = false; // Disable the movement after winning
        audioSource.PlayOneShot(winSound);

        // Wait for the win sound to play before proceeding
        yield return new WaitForSeconds(3);

        GameStateManager.Win(); // Call the static Win method on GameStateManager
    }

    public void TriggerLose()
    {
        StartCoroutine(LoseGame()); // Start the LoseGame coroutine
    }

    private IEnumerator LoseGame()
    {
        animator.SetBool("Lose", true);
        canMove = false; // Disable movement after losing
        audioSource.PlayOneShot(loseSound);

        // Wait for the lose sound to play before proceeding
        yield return new WaitForSeconds(3);

        GameStateManager.Lose(); // Calling the static Lose method on GameStateManager
    }

    // Method to reset player movement and animation states
    public void ResetPlayer()
    {
        animator.SetBool("Win", false);
        animator.SetBool("Lose", false);
        canMove = true; // Re-enable movement
        // Additional logic to reset the player's state as needed
    }
}
