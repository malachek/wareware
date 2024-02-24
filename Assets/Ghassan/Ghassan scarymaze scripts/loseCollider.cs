using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("runplayer")) // Player has collided with something
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                if (!gameObject.CompareTag("Wall")) // If it's not a wall, it's a lose condition
                {
                    playerController.TriggerLose(); // Triggering the lose sequence here
                }
            }
        }
    }
}
