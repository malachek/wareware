using UnityEngine;
using System.Collections;
public class Bird1 : MonoBehaviour
{
    public float speed = 2.0f; // Speed of the movement
    public bool isMoving = true; // Control flag for the bird's movement

    private void Start()
    {
        StartCoroutine(MoveInPattern());
    }

    private IEnumerator MoveInPattern()
    {
        while (isMoving) // Only continue the loop if isMoving is true
        {
            // Move right for 5 units
            yield return Move(Vector2.right * 13);
            // Move down for 2 units
            yield return Move(Vector2.down * 2);
            // Move left for 5 units
            yield return Move(Vector2.left * 13);
            // Move up for 2 units
            yield return Move(Vector2.up * 2);
        }
    }

    private IEnumerator Move(Vector2 direction)
    {
        float elapsedTime = 0;
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + direction;

        while (elapsedTime < direction.magnitude / speed)
        {
            transform.position = Vector2.Lerp(startPosition, endPosition, elapsedTime / (direction.magnitude / speed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
    }

    // Method to stop the bird's movement
    public void StopMoving()
    {
        isMoving = false;
        // Additional logic to stop the bird immediately, if necessary
    }
}
