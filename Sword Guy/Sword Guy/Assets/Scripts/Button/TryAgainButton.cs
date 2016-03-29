using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour {

	public void TryLevelAgain()
    {
        GameObject.Find("Game Manager").GetComponent<GameManager>().health = GameObject.Find("Sword Guy").GetComponent<SwordGuy>().getMaxHealth();
        SceneManager.LoadScene(Application.loadedLevel);
    }
}
