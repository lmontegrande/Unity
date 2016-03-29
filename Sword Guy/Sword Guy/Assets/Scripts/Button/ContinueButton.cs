using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour {

    public string nextScene;

	public void LoadScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
