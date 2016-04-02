using UnityEngine;
using System.Collections;

public class Bird : Enemy {

    public Vector3 velocity;

    [SerializeField]
    private int
        maxHealth = 10,
        damage = 10;

    [SerializeField]
    private float
        speed = .5f,
        patrolRange = 5f;

    [SerializeField]
    private AudioClip 
        getHitSound;

    private Transform player;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AudioSource audioSource;
    private int currentHealth;
    private bool spottedEnemy;
    private bool dead;
        

	void Start () {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Sword Guy").transform;
        currentHealth = maxHealth;
	}
	
	void Update () {
        // Debugging Code
        Vector3 patrolPoint = Vector3.MoveTowards(transform.position, player.position, patrolRange);
        Debug.DrawLine(transform.position, patrolPoint);
        velocity = body.velocity;

        Move();
        Display();
	}

    void Move()
    {
        if (dead)
            return;

        if (spottedEnemy || Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.position.x, player.position.y)) < patrolRange) {
            spottedEnemy = true;
            animator.SetBool("attacking", true);
            Debug.Log("Chasing");
            Vector3 LerpPosition = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * speed);
            transform.position = LerpPosition;
        } 
    }

    void Display()
    {
        if (spottedEnemy && player.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        } else
        {
            spriteRenderer.flipX = false;
        }
        ShowDamage();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !dead)
        {
            other.gameObject.GetComponent<SwordGuy>().GetHit(damage);
        }
    }

    public override void GetHit(int damage)
    {
        if (dead)
            return;

        currentHealth -= damage;
        audioSource.PlayOneShot(getHitSound);
        if (currentHealth <= 0)
        {
            DestroySelf();
        }
    }

    public override void DestroySelf()
    {
        dead = true;
        Destroy(gameObject, 2f);
        body.isKinematic = false;
    }

    protected override void ShowDamage()
    {
        spriteRenderer.color = Color.Lerp(Color.red, Color.white, (float)currentHealth / maxHealth);
    }
}
