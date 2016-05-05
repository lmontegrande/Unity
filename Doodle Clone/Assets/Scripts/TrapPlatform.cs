using UnityEngine;
using System.Collections;

public class TrapPlatform : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Rigidbody2D playerBody = other.gameObject.GetComponent<Rigidbody2D>();

            // Only bounce if going down
            if (playerBody.velocity.y <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
