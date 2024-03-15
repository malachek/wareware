using UnityEngine;

public class Samurai : MonoBehaviour
{
    private Animator animator;
    private AudioSource au;
    // This method moves the Samurai 5 units to the left
    public void MoveOnWin()
    {

        // Moves the Samurai 5 units to the left

        transform.position += Vector3.left * 4;


    }
    public void playanime()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("hitting");
    }
    public void Samueaiaudio()
    {
        au = GetComponent<AudioSource>();
        // Check if the AudioSource is not currently playing
        if (au != null && !au.isPlaying)
        {
            au.Play();
        }
    }
}
