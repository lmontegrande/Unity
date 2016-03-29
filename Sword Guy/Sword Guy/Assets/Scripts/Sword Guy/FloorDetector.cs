using UnityEngine;
using System.Collections;

public class FloorDetector : MonoBehaviour {

    public bool isGrounded;

	void OnTriggerStay2D(Collider2D other)
    {
        isGrounded = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
    }
}
