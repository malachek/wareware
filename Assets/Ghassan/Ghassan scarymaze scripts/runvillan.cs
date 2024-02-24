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
}
