using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int health = 0;

    public bool levelZeroUnlocked = true;
    public bool levelOneUnlocked;
    public bool levelTwoUnlocked;
    public bool levelThreeUnlocked;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

	public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
