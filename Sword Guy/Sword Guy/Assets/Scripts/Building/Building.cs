using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

    public string levelToLoad = "Level 0 - 2";

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
            gameManager.LoadLevel(levelToLoad);
    }
}
