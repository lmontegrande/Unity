using UnityEngine;
using System.Collections;

public class KillTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.GameOver();
        }
        else if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
