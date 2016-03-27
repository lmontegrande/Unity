using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour {

	public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
