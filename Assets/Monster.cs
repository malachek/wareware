using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //private Animator targetAnimator;
    //[SerializeField] private GameObject targetGameObject;
    private Camera mainCamera;
    private Vector3 previousPosition;
    private Animator animator;
    private bool canMove = true;
   // [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip winSound;
    private AudioSource audioSource;
    private int babyCounter = 0;
    private GameObject babySpitObject;
    private GameObject babySpitObject1;
    private GameObject babySpitObject2;
    private bool isActive1 = true;
    // Add a public reference to the Samurai script
    public Samurai samurai;

    private void Start()
    {
      //  targetAnimator = targetGameObject.GetComponent<Animator>();
        mainCamera = Camera.main;
        previousPosition = transform.position;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        babySpitObject = GameObject.FindWithTag("babyspit");
        babySpitObject1 = GameObject.FindWithTag("babyspit1");
        babySpitObject2 = GameObject.FindWithTag("babyspit2");
        StartCoroutine(LoseGame4());
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && canMove)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector3 moveDirection = mousePosition - previousPosition;
            transform.position = mousePosition;
            if (moveDirection.x < 0 && transform.localScale.x > 0) FlipSprite();
            else if (moveDirection.x > 0 && transform.localScale.x < 0) FlipSprite();
            previousPosition = mousePosition;
            animator.SetTrigger("Idle");
            
        }
         
    }

    private void FlipSprite()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Baby")
        {
            Destroy(collision.gameObject);
            babyCounter++;
            if (babyCounter >= 3)
               


            TriggerWin4();
        }
    }

    public void TriggerWin4()
    {
        StartCoroutine(WinGame4());
    }

    private IEnumerator WinGame4()
    {
        canMove = false;
        samurai.MoveOnWin();
        samurai.Samueaiaudio();
        yield return new WaitForSeconds(2);
        
        samurai.playanime();
        if (canMove == false)
        {
            Debug.Log("Attempting to play Monsterdeath animation");
            animator.SetTrigger("Monsterdeath");
            babySpitObject.transform.localScale += new Vector3(1, 1, 1);
            babySpitObject.transform.position += new Vector3(1, -0.8f, 1);
            babySpitObject1.transform.localScale += new Vector3(1, 1, 1);
            babySpitObject1.transform.position += new Vector3(2, -0.8f, 1);
            babySpitObject2.transform.localScale += new Vector3(1, 1, 1);
            babySpitObject2.transform.position += new Vector3(3, -0.8f, 1);
        }
        //animator.SetTrigger("Monsterdeath");
        //targetAnimator.SetTrigger("hitting");
        // animator.SetBool("Win", true);

        // audioSource.PlayOneShot(winSound);
        yield return new WaitForSeconds(winSound.length);

        // Call MoveOnWin on the Samurai instance
        //if (samurai != null) samurai.MoveOnWin();
        //else Debug.LogError("Samurai reference not set in the Monster script.");

        GameStateManager.Win();
    }
    public void TriggerLose4()
    {
        StartCoroutine(LoseGame4()); // Start the LoseGame coroutine
    }

    private IEnumerator LoseGame4()
    {
        //animator.SetBool("Lose", true);
        canMove = false; // Disable movement after losing
        //audioSource.PlayOneShot(loseSound);

        // so it can Wait for the lose sound to play before proceeding
        yield return new WaitForSeconds(10);
        isActive1 = false;
        GameStateManager.Lose(); // Calling the static LoseLife method on GameStateManager
    }
    public void ResetPlayer()
    {
        animator.SetBool("Win", false);
        animator.SetBool("Lose", false);
        canMove = true;
        babyCounter = 0;
    }
}

