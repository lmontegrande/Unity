using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

    public int damage = 10;

	void OnCollisionStay2D(Collision2D other)
    {
        {
            if (other.gameObject.tag == "Player")
                other.gameObject.GetComponent<SwordGuy>().GetHit(damage);
        }
    }
}
