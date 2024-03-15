/* using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
public class changecolor : MonoBehaviour
{
    private Animator squareAnimator; // Animator for the enemy square
    private Animator blueGirlAnimator; // Animator for the BlueGirl
    private AudioSource audioSource; // AudioSource component to play audio

    [SerializeField] private AudioClip shootClip; // Audio clip for shooting
    [SerializeField] private AudioClip winClip; // Audio clip for winning
    [SerializeField] private AudioClip backgroundMusicClip; // Background music audio clip

    void Start()
    {
        // Find the enemy square GameObject and get its Animator
        GameObject square = GameObject.FindGameObjectWithTag("bird");
        if (square != null)
        {
            squareAnimator = square.GetComponent<Animator>();
        }

        // Find the BlueGirl GameObject and get its Animator
        GameObject blueGirl = GameObject.FindGameObjectWithTag("BlueGirl");
        if (blueGirl != null)
        {
            blueGirlAnimator = blueGirl.GetComponent<Animator>();
        }

        // Get the AudioSource component from the GameObject this script is attached to
        audioSource = GetComponent<AudioSource>();

        // Play the background music on start, looping
        if (backgroundMusicClip != null)
        {
            audioSource.loop = true;
            audioSource.clip = backgroundMusicClip;
            audioSource.Play();
        }
    }

    void Update()
    {
        FollowMouse();

        // Check for left-click
        if (Input.GetMouseButtonDown(0))
        {
            TriggerAnimationIfOverlapping();
            // Trigger the BlueGirlshoots animation and play shoot sound on left-click
            if (blueGirlAnimator != null)
            {
                blueGirlAnimator.SetTrigger("BlueGirlshoots");
                PlaySound(shootClip);
            }
        }
    }

    private void FollowMouse()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
    }

    private void TriggerAnimationIfOverlapping()
    {
        bool hitEnemy = false; // Flag to check if we hit the enemy

        // Perform a check to see if this GameObject is overlapping the square
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var hit in hits)
        {
            if (hit.tag == "bird")
            {
                squareAnimator.SetTrigger("Dying Enemy");
                hitEnemy = true; // We hit the enemy, set the flag

                // for Accessesing the Bird1 script and call StopMoving to halt movement
                Bird1 birdScript = hit.GetComponent<Bird1>();
                if (birdScript != null)
                {
                    birdScript.StopMoving();
                }

                break; // so it can Exit the loop once the square is found
            }
        }
        private void TriggerWin1()
        {
            StartCoroutine(WinGame1()); // this is to Start the WinGame coroutine
        }

        private IEnumerator WinGame1()
        {

            //canMove = false; // Disable the movement after winning
            if (hitEnemy && blueGirlAnimator != null)
            {
                blueGirlAnimator.SetTrigger("BlueGirlWin");
                // Stop the background music and play win sound
                audioSource.Stop();
                PlaySound(winClip);
            }

            // Wait for the win sound to play before proceeding
            yield return new WaitForSeconds(3);

            GameStateManager.Win(); // Call the static Win method on GameStateManager
        }
        // If we hit the enemy, trigger the BlueGirlWin animation and play win sound
       // if (hitEnemy && blueGirlAnimator != null)
      //  {
           // blueGirlAnimator.SetTrigger("BlueGirlWin");
            // Stop the background music and play win sound
          //  audioSource.Stop();
           // PlaySound(winClip);
       // }
    }

    // this is the Helper method to play a sound
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
*/
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class changecolor : MonoBehaviour
{
    private Animator squareAnimator; // Animator for the enemy square
    private Animator blueGirlAnimator; // Animator for the BlueGirl
    private AudioSource audioSource; // AudioSource component to play audio

    [SerializeField] private AudioClip shootClip; // Audio clip for shooting
    [SerializeField] private AudioClip winClip; // Audio clip for winning
    [SerializeField] private AudioClip backgroundMusicClip; // Background music audio clip
    private bool isActive = true;
    void Start()
    {
        // Find the enemy square GameObject and get its Animator
        GameObject square = GameObject.FindGameObjectWithTag("bird");
        if (square != null)
        {
            squareAnimator = square.GetComponent<Animator>();
        }

        // Find the BlueGirl GameObject and get its Animator
        GameObject blueGirl = GameObject.FindGameObjectWithTag("BlueGirl");
        if (blueGirl != null)
        {
            blueGirlAnimator = blueGirl.GetComponent<Animator>();
        }

        // Get the AudioSource component from the GameObject this script is attached to
        audioSource = GetComponent<AudioSource>();

        // Play the background music on start, looping
        if (backgroundMusicClip != null)
        {
            audioSource.loop = true;
            audioSource.clip = backgroundMusicClip;
            audioSource.Play();
        }
        StartCoroutine(LoseGame1());
    }

    void Update()
    {
        if (!isActive) return; // If the script is not active, do nothing

        FollowMouse();

        // Check for left-click
        if (Input.GetMouseButtonDown(0))
        {
            TriggerAnimationIfOverlapping();
        }
        

    }

    private void FollowMouse()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
    }

    private void TriggerAnimationIfOverlapping()
    {
        bool hitEnemy = false; // Flag to check if we hit the enemy

        // Perform a check to see if this GameObject is overlapping the square
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var hit in hits)
        {
            if (hit.tag == "bird")
            {
                squareAnimator.SetTrigger("Dying Enemy");
                hitEnemy = true; // We hit the enemy, set the flag

                // Accessing the Bird1 script and call StopMoving to halt movement
                Bird1 birdScript = hit.GetComponent<Bird1>();
                if (birdScript != null)
                {
                    birdScript.StopMoving();
                }

                break; // Exit the loop once the square is found
            }
        }

        if (hitEnemy)
        {
            TriggerWin1(); // Trigger the win sequence if we hit the enemy
        }
    }

    private void TriggerWin1()
    {
        StartCoroutine(WinGame1()); // Start the WinGame coroutine
    }

    private IEnumerator WinGame1()
    {
        if (blueGirlAnimator != null)
        {
            isActive = false;
            blueGirlAnimator.SetTrigger("BlueGirlWin");
            // Stop the background music and play win sound
            audioSource.Stop();
            PlaySound(winClip);
        }

        // Wait for the win sound to play before proceeding
        yield return new WaitForSeconds(3);

        // Insert the code to handle game win logic here, for example:
         GameStateManager.Win(); // Call the static Win method on GameStateManager
    }
    public void TriggerLose1()
    {
        StartCoroutine(LoseGame1()); // Start the LoseGame coroutine
    }

    private IEnumerator LoseGame1()
    {
       // animator.SetBool("Lose", true);
        //canMove = false; // Disable movement after losing
        //audioSource.PlayOneShot(loseSound);

        // so it can Wait for the lose sound to play before proceeding
        yield return new WaitForSeconds(10);
        isActive = false;
        GameStateManager.Lose(); // Calling the static LoseLife method on GameStateManager
    }
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
