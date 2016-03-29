using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    public string nextScene;
    public bool sceneTrigger = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && sceneTrigger)
            if (!other.gameObject.GetComponent<SwordGuy>().isDead())
                SceneManager.LoadScene(nextScene);
    }    
}
