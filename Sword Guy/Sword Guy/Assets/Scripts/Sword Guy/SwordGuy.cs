using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwordGuy : MonoBehaviour
{
    public Vector2 vel;

    [SerializeField]
    private int
        maxHealth = 10,
        myDamage = 10;

    [SerializeField]
    private float
        horizontalSpeed = 5f,
        sprintSpeed = 8f,
        maxHorizontalSpeed = 6f,
        maxSprintSpeed = 10f,
        jumpForce = 5f,
        jumpForceHigh = 10f,
        horizontalGroundDrag = 3f,
        horizontalAirDrag = 1f,
        horizontalAirControlAdjust = 2f,
        horizontalKnockBack = 2f,
        verticalKnockBack = 2f,
        recoveryTimeInSeconds = 5f,
        jumpDurationTime = 1f,
        swingDistance = 1f,
        attackRecoverTime = .5f,
        invulnerabilityTimeMax = .5f;

    [SerializeField, Range(0f, 1f)]
    private float soundEffectVolume;

    [SerializeField]
    private AudioClip 
        swordSoundEffect,
        getHitSound;

    [SerializeField]
    private Transform sword;

    [SerializeField]
    private FloorDetector feet;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private GameObject attackText;
    private GameObject healthText;
    private GameObject mainMenuButton;
    private GameObject reloadLevelButton;
    private GameManager gameManager;
    private AudioSource audioSource;
    private float invulnerabilityTimer = 1f;
    private float currentAttackRecoverTime;
    private float currentRecoveryTime = 0;
    private float currentJumpDurationTime = 0;
    private int currentHealth;
    private bool isGrounded;
    private bool facingLeft;
    private bool attacking;
    private bool jumping;
    private bool gettingHit;
    private bool dead;
    private bool canMove = true;
    private bool invulnerable;

    /// <summary>
    /// Initializing Variables
    /// </summary>
    void Start()
    {
        currentJumpDurationTime = jumpDurationTime;
        mainMenuButton = GameObject.Find("Main Menu Button");
        reloadLevelButton = GameObject.Find("Reload Level Button");
        reloadLevelButton.SetActive(false);
        mainMenuButton.SetActive(false);
        currentAttackRecoverTime = 0;
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        dead = false;
        currentHealth = maxHealth;
        attackText = GameObject.Find("Ready Text");
        healthText = GameObject.Find("Health Text");
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        if (transform.rotation.y < 180)
        {
            facingLeft = false;
        }
        if (gameManager.health <= 0)
        {
            gameManager.health = maxHealth;
        }
        else
        {
            currentHealth = gameManager.health;
        }
    }

    /// <summary>
    /// Update HUD and Handle Input
    /// </summary>
    void Update()
    {
        UpdateHUD();
        SetAnimVars();
        HandleInput();
    }

    // Helper Methods -------------------------------------------------------------

    /// <summary>
    /// Handle Input
    /// </summary>
    void HandleInput()
    {
        if (dead)
            return;
        AttackInput();
        Move();
    }

    /// <summary>
    /// Update HUD
    /// </summary>
    void UpdateHUD()
    {
        Text text = attackText.GetComponent<Text>();
        if (!attacking && !jumping && !gettingHit)
        {
            text.text = "ATTACK READY!";
            text.color = Color.green;
        }
        else
        {
            text.text = "Can't Attack";
            text.color = Color.red;
        }

        if (!dead)
        {
            healthText.GetComponent<Text>().color = Color.green;
            healthText.GetComponent<Text>().text = "HP: " + currentHealth;
        }
        else
        {
            mainMenuButton.SetActive(true);
            reloadLevelButton.SetActive(true);
            text.text = "";
            healthText.GetComponent<Text>().color = Color.red;
            healthText.GetComponent<Text>().text = "Game Over";
        }
    }

    void SetAnimVars()
    {
        if (dead)
        {
            anim.SetBool("gotHit", false);
            return;
        }
        anim.SetFloat("velocityX", Mathf.Abs(body.velocity.x));
        anim.SetFloat("velocityY", Mathf.Abs(body.velocity.y));
    }

    /// <summary>
    /// Handle attack input
    /// </summary>
    void AttackInput()
    {
        Invulnerability();

        // Handle when getting hit
        if (gettingHit)
        {
            if (currentRecoveryTime < recoveryTimeInSeconds)
            {
                // Still getting hit
                currentRecoveryTime += Time.deltaTime;
                canMove = false;
                return;
            }
            else
            {
                // Done getting hit
                currentRecoveryTime = 0;
                gettingHit = false;
                anim.SetBool("gotHit", false);
                canMove = true;
            }
        }

        // Handle Attack
        if (Input.GetButton("Fire1") && !attacking)
        {
            attacking = true;
            anim.SetTrigger("attack");
        }

        if (attacking)
        {
            currentAttackRecoverTime += Time.deltaTime;
            if (currentAttackRecoverTime >= attackRecoverTime)
            {
                // Done attacking
                currentAttackRecoverTime = 0;
                attacking = false;
                canMove = true;
            }
            else
            {
                // Still attacking
                canMove = false;
                return;
            }
        }
    }

    /// <summary>
    /// Handle move input
    /// </summary>
    void Move()
    {

        // Handle horizontal drag
        if (isGrounded)
        {
            float horizontalDrag = Mathf.Lerp(0, horizontalGroundDrag, Mathf.Abs(body.velocity.x) / maxHorizontalSpeed);
            horizontalDrag = horizontalDrag * Time.deltaTime;
            if (body.velocity.x < 0)
                body.velocity += new Vector2(horizontalDrag, 0);
            else
                body.velocity -= new Vector2(horizontalDrag, 0);
        }
        else
        {
            float horizontalDrag = Mathf.Lerp(0, horizontalAirDrag, Mathf.Abs(body.velocity.x) / maxHorizontalSpeed);
            horizontalDrag = horizontalDrag * Time.deltaTime;
            if (body.velocity.x < 0)
                body.velocity += new Vector2(horizontalDrag, 0);
            else
                body.velocity -= new Vector2(horizontalDrag, 0);
        }

        // If player is in a state where he can't move no input is handled
        if (!canMove)
            return;

        JumpingInput();

        if (isGrounded)
            GroundMoveInput();
        else
            AirMoveInput();

        // Update velocity variable to be viewed in inspector.  Mostly for debugging
        vel = body.velocity;
    }

    void GroundMoveInput()
    {
        // Handle moving on ground input
        float adjustedSprintSpeed = sprintSpeed * Time.deltaTime;
        float adjustedHorizontalSpeed = horizontalSpeed * Time.deltaTime;
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            if (Input.GetButton("Sprint"))
            {
                body.velocity -= new Vector2(adjustedSprintSpeed, 0);
                if (body.velocity.x < -maxSprintSpeed)
                {
                    body.velocity = new Vector2(-maxSprintSpeed, body.velocity.y);
                }
            }
            else
            {
                body.velocity -= new Vector2(adjustedHorizontalSpeed, 0);
                if (body.velocity.x < -maxHorizontalSpeed)
                {
                    body.velocity = new Vector2(-maxHorizontalSpeed, body.velocity.y);
                }
            }
            if (!facingLeft)
            {
                Flip();
                facingLeft = true;
            }
        }

        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            if (Input.GetButton("Sprint"))
            {
                body.velocity += new Vector2(adjustedSprintSpeed, 0);
                if (body.velocity.x > maxSprintSpeed)
                {
                    body.velocity = new Vector2(maxSprintSpeed, body.velocity.y);
                }
            }
            else
            {
                body.velocity += new Vector2(adjustedHorizontalSpeed, 0);
                if (body.velocity.x > maxHorizontalSpeed)
                {
                    body.velocity = new Vector2(maxHorizontalSpeed, body.velocity.y);
                }
            }
            if (facingLeft)
            {
                Flip();
                facingLeft = false;
            }
        }
    }

    void AirMoveInput()
    {
        // Handle moving in air input
        float adjustedHorizontalSpeed = horizontalSpeed * Time.deltaTime;
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            body.velocity -= new Vector2(adjustedHorizontalSpeed / horizontalAirControlAdjust, 0);
            if (body.velocity.x < -maxSprintSpeed)
            {
                body.velocity = new Vector2(-maxSprintSpeed, body.velocity.y);
            }
            if (!facingLeft)
            {
                Flip();
                facingLeft = true;
            }
        }

        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            body.velocity += new Vector2(adjustedHorizontalSpeed / horizontalAirControlAdjust, 0);
            if (body.velocity.x > maxSprintSpeed)
            {
                body.velocity = new Vector2(maxSprintSpeed, body.velocity.y);
            }

            if (facingLeft)
            {
                Flip();
                facingLeft = false;
            }
        }
    }

    void JumpingInput()
    {
        // Handle jumping input
        isGrounded = feet.isGrounded;
        if (isGrounded && body.velocity.y <= 0)
        {
            jumping = false;
            currentJumpDurationTime = 0;
            anim.SetBool("jump", false);
        }
        else
        {
            currentJumpDurationTime += Time.deltaTime;
            anim.SetBool("jump", true);
        }

        if (Input.GetButton("Jump") && body.velocity.y >= 0)
        {
            if (currentJumpDurationTime <= jumpDurationTime)
            {
                if (!jumping)
                {
                    anim.SetTrigger("start jump");
                }

                jumping = true;
                isGrounded = false;
                float adjustedJumpForce = Mathf.Lerp(jumpForce, 0, currentJumpDurationTime / jumpDurationTime);

                body.velocity = new Vector2(body.velocity.x, adjustedJumpForce);
            }
        }
    }

    /// <summary>
    /// Check if player is invulnerable and update when he isn't
    /// </summary>
    void Invulnerability()
    {

        if (invulnerabilityTimer < invulnerabilityTimeMax)
        {
            invulnerabilityTimer += Time.deltaTime;
            GetComponent<SpriteRenderer>().color = Color.red;
            invulnerable = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            invulnerable = false;
        }
    }

    /// <summary>
    /// Flips player.  Used Transform.Rotate() instead of SpriteRenderer.FlipX() because of the child gameobject
    /// </summary>
    void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
    }

    void Die()
    {
        dead = true;
        anim.SetTrigger("die");
        return;
    }

    // Get Methods -------------------------------------------------------------

    /// <summary>
    /// Return true if player is dead
    /// </summary>
    /// <returns></returns>
    public bool isDead()
    {
        return dead;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    // Mutator Methods -------------------------------------------------------------

    /// <summary>
    /// Damage the player
    /// </summary>
    /// <param name="damage">how much to damage the player by</param>
    public void GetHit(int damage)
    {
        if (invulnerable || dead)
        {
            return;
        }
        else
        {
            invulnerabilityTimer = 0;
        }

        audioSource.PlayOneShot(getHitSound, soundEffectVolume);
        currentHealth -= damage;
        gameManager.health = currentHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
        gettingHit = true;
        anim.SetBool("gotHit", true);
        float knockback = horizontalKnockBack;
        if (!facingLeft)
        {
            knockback = -knockback;
        }
        body.velocity = new Vector2(knockback, verticalKnockBack);
    }

    /// <summary>
    /// Called by the animator when the sprite of the players sword swing shows
    /// </summary>
    public void AttackSwing()
    {
        audioSource.PlayOneShot(swordSoundEffect, soundEffectVolume);
        Collider2D[] hits = Physics2D.OverlapCircleAll(sword.position, swingDistance, 1 << LayerMask.NameToLayer("Enemies"));
        Collider2D hit = new Collider2D();
        foreach (Collider2D rayHit in hits)
        {
            if (!rayHit.isTrigger)
            {
                hit = rayHit;
                hit.gameObject.GetComponent<Enemy>().GetHit(myDamage);
            }
        }
    }
}
