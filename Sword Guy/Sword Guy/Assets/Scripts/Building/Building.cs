using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

    private GameManager gameManager;

    void Start()
    {
        if (GameManager.instance != null)
        {
            gameManager = GameManager.instance;
        }
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            gameManager.LoadLevel("Level 0 - 2");
    }
}
