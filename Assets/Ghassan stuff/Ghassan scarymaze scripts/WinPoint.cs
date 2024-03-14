using UnityEngine;

public class WinPoint : MonoBehaviour
{
    public Transform scaryImageTransform; // Assign this in the inspector
    public Vector3 targetPosition;       // Set this to the desired position

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("runplayer")) // Make sure your player has the "runplayer" tag
        {
            scaryImageTransform.position = targetPosition;

            // Assuming the PlayerController script is attached to the player object
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TriggerWin(); // Call the TriggerWin method instead of Wingame
            }
        }
    }
}
