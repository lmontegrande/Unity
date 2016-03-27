using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    [SerializeField]
    private float laserSpeed = 10f;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private int damage;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<SwordGuy>().GetHit(damage);
        }
    }

	public void FireLaser(bool fireLeft, int damageSent)
    {
        damage = damageSent;
        if (fireLeft)
        {
            body.velocity = new Vector2(-laserSpeed, 0);
        }
        else
        {
            spriteRenderer.flipX = true;
            body.velocity = new Vector2(laserSpeed, 0);
        }

        Destroy(gameObject, 1f);
    }

}
