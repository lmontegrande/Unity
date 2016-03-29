using UnityEngine;
using System.Collections;

public class ChaseRobot : Robot {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !dead)
        {
            other.gameObject.GetComponent<SwordGuy>().GetHit(myDamage);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !dead)
        {
            other.gameObject.GetComponent<SwordGuy>().GetHit(myDamage);
        }
    }
}
