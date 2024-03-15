using UnityEngine;

public class Winpoint : MonoBehaviour
{
    public Transform scaryImageTransform; // Assign this in the inspector
    public Vector3 targetPosition;       // Set this to the desired position

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("runplayer")) // Make sure your player has the "Player" tag
        {
            scaryImageTransform.position = targetPosition;
        }
    }
}
