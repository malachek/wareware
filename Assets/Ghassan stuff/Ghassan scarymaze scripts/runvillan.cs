using TMPro;
using UnityEngine;

public class VillainController : MonoBehaviour
{
    public Transform playerTransform; // Assign this in the editor
    public float moveSpeed = 2.0f;

    private void Update()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("runplayer")) // Make sure your player has the "runplayer" tag
        {
            

            // Assuming the PlayerController script is attached to the player object
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TriggerLose(); // Call the TriggerWin method instead of Wingame
            }
        }
    }
}



