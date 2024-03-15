using UnityEngine;

public class VillainController : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 2.0f;

    private void Update()
    {
        if (this.enabled && playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("runplayer")) // Assuming your player has the tag "runplayer"
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TriggerLose(); // Trigger the loss condition
            }
        }
    }

    public void StopChasing()
    {
        this.enabled = false;
    }
}
