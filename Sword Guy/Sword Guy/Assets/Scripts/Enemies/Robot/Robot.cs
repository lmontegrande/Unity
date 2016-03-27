using UnityEngine;
using System.Collections;

public class Robot : Enemy {

    public int myDamage = 10;

    protected bool dead;

    [SerializeField]
    private float 
        moveSpeed = 1f,
        verticalKnockBack = 2f,
        horizontalKnockBack = 1f,
        hitRecoverTime = 1f,
        patrolRange = 5f;

    [SerializeField]
    private int maxHealth = 10;

    [SerializeField, Range(0f, 1f)]
    private float soundEffectVolume;

    [SerializeField]
    private AudioClip getHitSound;

    private Animator anim;
    private GameObject player;
    private Rigidbody2D body;
    private AudioSource audioSource;
    private GameManager gameManager;
    private int currentHealth;
    private float currentRecoveryTime;
    private bool facingLeft;
    private bool attacking;
    private bool gettingHit;
    private bool patrolling;

    void Start()
    {
        if (gameManager != null)
            gameManager = GameManager.instance;
        audioSource = GetComponent<AudioSource>();
        dead = false;
        patrolling = true;
        currentHealth = maxHealth;
        facingLeft = true;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Sword Guy");
    }

    void Update()
    {
        anim.SetFloat("velocityX", Mathf.Abs(body.velocity.x));

        if (patrolling)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < patrolRange)
            {
                patrolling = false;
            } else
            {
                return;
            }
        }

        if (body.velocity.x > 0 && facingLeft)
        {
            facingLeft = false;
            Flip();
        }

        if (body.velocity.x < 0 && !facingLeft)
        {
            facingLeft = true;
            Flip();
        }
    }

    void FixedUpdate()
    {
        if (dead)
        {
            return;
        }
        if (gettingHit && (currentRecoveryTime < hitRecoverTime))
        {
            currentRecoveryTime += Time.deltaTime;
            return;
        }
        currentRecoveryTime = 0f;
        gettingHit = false;
        anim.SetBool("gotHit", false);

        if (patrolling)
            return;

        if (attacking)
        {
            return;
        }
        Vector3 vectorBetween = player.transform.position - transform.position;
        if (player.GetComponent<SwordGuy>().isDead())
        {
            vectorBetween = -vectorBetween;
        }
        body.velocity = (new Vector2(moveSpeed * (vectorBetween.normalized).x, 0));
    }

    void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !dead)
        {
            attacking = true;
            other.gameObject.GetComponent<SwordGuy>().GetHit(myDamage);
            anim.SetBool("attack", true);
        }
    }

    public void DoneAttacking()
    {
        attacking = false;
        anim.SetBool("attack", false);
    }

    public override void GetHit(int damage)
    {
        audioSource.PlayOneShot(getHitSound, soundEffectVolume);
        gettingHit = true;
        anim.SetBool("gotHit", true);
        gettingHit = true;
        currentHealth -= damage;
        float knockback = horizontalKnockBack;
        if (!facingLeft)
        {
            knockback = -knockback;
        }
        body.velocity = new Vector2(knockback, verticalKnockBack);
        if (currentHealth <= 0)
        {
            DestroySelf();
        }
    }

    public override void DestroySelf()
    {
        dead = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 1f); 
    }
}
