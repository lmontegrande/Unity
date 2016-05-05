using UnityEngine;
using System.Collections;

public class GenerateFrameTrigger : MonoBehaviour {

    private bool isTriggered = false;

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isTriggered)
        {
            GameManager.instance.GenerateFrame();
            isTriggered = true;
        }
    }
}
