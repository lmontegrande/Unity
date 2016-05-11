using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private int damage = 1;

    [SerializeField]
    private bool destroyOnContact = false;

    private Rigidbody2D _rigidBody;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.GetComponent<Enemy>().GetHit(_rigidBody.velocity, _rigidBody.mass, damage);
        }

        if (destroyOnContact)
            Destroy(gameObject);
    }
}
