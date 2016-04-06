using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BirdBoss : Enemy {

    private enum BirdState { FlyingLeft, FlyingRight, NotFlying };

    [SerializeField]
    private Transform
        leftSide,
        rightSide;

    [SerializeField]
    private AudioClip getHitSound;

    [SerializeField]
    private int
        maxHealth = 100,
        damage = 10, 
        timesHitTillMove = 5;

    [SerializeField]
    private float 
        moveTime = 4f,
        deathDelay = 2f;

    [SerializeField]
    private BirdState birdState = BirdState.NotFlying;

    private Transform currentTransform;
    private Animator animator;
    private SwordGuy player;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;
    private CircleCollider2D circleCollider;
    private int currentHealth;
    private int currentTimesHit = 0;
    private float currentMoveTime = 0;
    private bool isOnLeftSide = true;
    private bool dead = false;
    private bool playerInRange = false;
    public string levelToLoad;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();

        currentTransform = leftSide;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if ((birdState != BirdState.NotFlying) && !dead)
        {
            Move();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !dead && (birdState == BirdState.NotFlying))
        {
            playerInRange = true;
            player = other.gameObject.GetComponent<SwordGuy>();
            animator.SetBool("attacking", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !dead)
        {
            playerInRange = false;
            animator.SetBool("attacking", false);
        }
    }

    void Move()
    {
        Transform startLocation = null;
        Transform moveLocation = null;
        if (birdState == BirdState.FlyingRight)
        {
            startLocation = leftSide;   
            moveLocation = rightSide;
        }
        if (birdState == BirdState.FlyingLeft)
        {
            startLocation = rightSide;
            moveLocation = leftSide;
        }

        if (currentMoveTime <= moveTime)
        {
            currentMoveTime += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(startLocation.transform.position, moveLocation.transform.position, currentMoveTime/moveTime);
        }
        else
        {
            currentMoveTime = 0;
            StopMove();
        }
    }
    
    void SetMove()
    {
        animator.SetTrigger("move");
        animator.SetBool("moving", true);

        if (isOnLeftSide)
            birdState = BirdState.FlyingRight;
        else
            birdState = BirdState.FlyingLeft;
        isOnLeftSide = !isOnLeftSide;

    }

    void StopMove()
    {
        birdState = BirdState.NotFlying;
        animator.SetBool("moving", false);
        Flip();
    }

    void Flip()
    {
        gameObject.transform.Rotate(new Vector3(0, 180, 0));
    }

    public void Attack()
    {
        if (playerInRange)
            player.GetHit(damage);
    }

    public override void GetHit(int damage)
    {
        if (birdState != BirdState.NotFlying)
            return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            DestroySelf();
        }
        audioSource.PlayOneShot(getHitSound);

        currentTimesHit++;
        if (currentTimesHit >= timesHitTillMove)
        {
            currentTimesHit = 0;
            SetMove();
        }

    }

    public override void DestroySelf()
    {
        dead = true;
        animator.SetTrigger("die");
        body.isKinematic = false;
        circleCollider.isTrigger = true;
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(deathDelay);

        if (GameManager.instance != null)
            GameManager.instance.levelThreeUnlocked = true;

        SceneManager.LoadScene(levelToLoad);
        Destroy(gameObject);
    }
}
