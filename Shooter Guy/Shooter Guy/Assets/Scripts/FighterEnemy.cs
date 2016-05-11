using UnityEngine;
using System.Collections;
using System;

public class FighterEnemy : MonoBehaviour, Enemy {

    [SerializeField]
    private float
        moveSpeed = 1f,
        knockback = 10f;

    [SerializeField]
    private int
        maxHealth = 10,
        damage = 10;

    private GameObject _player;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private int currentHealth;

	void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
	}
	
	void FixedUpdate() {
        Move();
        LookAt();
        Animate();
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject otherObject = other.gameObject;
        if (otherObject.tag == "Player")
        {
            otherObject.GetComponent<GunGuyController>().GetHit(damage, transform.position, knockback);
        }
    }

    void Move()
    {
        Vector3 moveVector = Vector3.MoveTowards(transform.position, _player.transform.position, moveSpeed);
        moveVector -= transform.position;
        _rigidBody.velocity += new Vector2(moveVector.x, moveVector.y);
    }

    void LookAt()
    {
        // Look at mouse location
        Vector3 diff = _player.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    void Animate()
    {
        _spriteRenderer.color = Color.Lerp(Color.red, Color.white, (float)currentHealth / maxHealth);
    }

    void Die()
    {
        _rigidBody.velocity = Vector2.zero;
        Destroy(gameObject);
    }

    public void GetHit(Vector2 bulletVelocity, float bulletMass, int bulletDamage)
    {
        _rigidBody.velocity += bulletVelocity * (bulletMass/_rigidBody.mass);
        currentHealth -= bulletDamage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}