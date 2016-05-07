using UnityEngine;
using System.Collections;

public class MonsterPlatform : MonoBehaviour {

    [SerializeField]
    private float 
        moveSpeed = 1f,
        outerBound;

    private bool isGoingRight = true;

    void FixedUpdate()
    {
        Move();
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.GameOver();
        }
    }

    void Move()
    {
        if (isGoingRight)
        {
            if (transform.position.x >= outerBound)
            {
                isGoingRight = false;
            }
            else
            {
                transform.position += new Vector3(moveSpeed, 0, 0);
            }
        }
        else
        {
            if (transform.position.x <= -outerBound)
            {
                isGoingRight = true;
            }
            else
            {
                transform.position -= new Vector3(moveSpeed, 0, 0);
            }
        }
    }
}
