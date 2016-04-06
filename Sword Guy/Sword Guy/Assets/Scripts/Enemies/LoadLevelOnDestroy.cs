using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelOnDestroy : MonoBehaviour {

    public string levelToLoad;

	void OnDestroy()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
