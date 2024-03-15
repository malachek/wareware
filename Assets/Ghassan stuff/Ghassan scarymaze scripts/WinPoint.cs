using UnityEngine;

public class WinPoint : MonoBehaviour
{
    public VillainController villainController; // Assign the villain object with the VillainController script in the inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("runplayer")) // Ensure your player has the "runplayer" tag
        {
            // Stop the villain
            if (villainController != null)
            {
                villainController.StopChasing();
            }

            // Trigger the win condition in the PlayerController
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TriggerWin();
            }
        }
    }
}
