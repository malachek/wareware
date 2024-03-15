using System.Collections;
using UnityEngine;

public class TheGrinch : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    public bool canMove = true;
    private int babyCounter = 0;
    private Animator animator;
    public Samurai samurai;
    private AudioSource audioSource;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    private bool isActive = true;
    private GameObject babySpitObject;
    private GameObject babySpitObject1;
    private GameObject babySpitObject2;
    private float lastPositionX; // Added to keep track of the last x position

    void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        babySpitObject = GameObject.FindGameObjectWithTag("babyspit");
        babySpitObject1 = GameObject.FindGameObjectWithTag("babyspit1");
        babySpitObject2 = GameObject.FindGameObjectWithTag("babyspit2");
        lastPositionX = transform.position.x; // Initialize lastPositionX with the starting x position
        ResetOrStartLoseGameTimer();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0; // Ensure we don't move the monster in Z-axis
            offset = transform.position - mouseWorldPos;
        }

        if (Input.GetMouseButton(0) && canMove)
        {
            DragMonster();
        }
        animator.SetTrigger("Idle");
    }

    void DragMonster()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) + offset;
        mousePosition.z = 0; // Keep monster on the same plane
        transform.position = mousePosition;

        // Flip sprite based on the direction of movement
        if (transform.position.x < lastPositionX)
        {
            // Moving left
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.position.x > lastPositionX)
        {
            // Moving right
            transform.localScale = new Vector3(1, 1, 1);
        }
        lastPositionX = transform.position.x; // Update lastPositionX to the new position for the next frame
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Baby"))
        {
            Destroy(other.gameObject); // Optionally, play a sound or animation here
            babyCounter++;
            if (babyCounter >= 3) // Assuming you need to collect 3 babies to win
            {
                StartCoroutine(TriggerWinCondition());
            }
        }
    }

    IEnumerator TriggerWinCondition()
    {
        canMove = false;
        //ebug.Log("we have " + babyCounter);
        // Uncomment the next lines if you have these set up
        //audioSource.PlayOneShot(winSound);
        samurai.Samueaiaudio();
        
        yield return new WaitForSeconds(3);//ssuming your samurai script has this method for win condition
        samurai.MoveOnWin();
        animator.SetTrigger("Monsterdeath");
        samurai.playanime();
        
        yield return new WaitForSeconds(0.1f);
        ScaleAndMoveBabySpitObjects();
        GameStateManager.Win(); // Make sure GameStateManager is properly implemented
    }

    void ScaleAndMoveBabySpitObjects()
    {
        if (babySpitObject != null)
        {
            babySpitObject.transform.localScale += new Vector3(1, 1, 1);
            babySpitObject.transform.position += new Vector3(1, -0.8f, 0);
        }

        if (babySpitObject1 != null)
        {
            babySpitObject1.transform.localScale += new Vector3(1, 1, 1);
            babySpitObject1.transform.position += new Vector3(2, -0.8f, 0);
        }

        if (babySpitObject2 != null)
        {
            babySpitObject2.transform.localScale += new Vector3(1, 1, 1);
            babySpitObject2.transform.position += new Vector3(3, -0.8f, 0);
        }
    }

    private IEnumerator LoseGameTimer()
    {
        yield return new WaitForSeconds(10); // Lose after 10 seconds, adjust as necessary
        if (isActive)
        {
            canMove = false;
            // Uncomment the next line if you have a lose animation
            //animator.SetTrigger("Lose");
            audioSource.PlayOneShot(loseSound);
            GameStateManager.Lose(); // Call your game state manager's lose method
        }
    }

    public void ResetOrStartLoseGameTimer()
    {
      //  StopCoroutine(LoseGameTimer());
        StartCoroutine(LoseGameTimer());
    }

    public void ResetGame()
    {
        babyCounter = 0;
        canMove = true;
        isActive = true;
        animator.ResetTrigger("Win");
        animator.ResetTrigger("Lose");
        lastPositionX = transform.position.x; // Reset lastPositionX
        // Reset any other necessary states here
    }
}
