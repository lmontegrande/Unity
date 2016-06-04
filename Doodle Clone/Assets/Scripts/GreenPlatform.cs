using UnityEngine;
using System.Collections;

public class GreenPlatform : MonoBehaviour {

    [SerializeField]
    private float horizontalBounceForce = 10f;

    [SerializeField]
    private AudioClip bounceSound;

	void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Rigidbody2D playerBody = other.gameObject.GetComponent<Rigidbody2D>();

            // Only bounce if going down
            if (playerBody.velocity.y <= 0)
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, horizontalBounceForce);
                GetComponent<AudioSource>().PlayOneShot(bounceSound);
            }
        }
    }
}
