using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour {

    [SerializeField]
    private string levelToLoad = "scene01";

    [SerializeField]
    private ScreenManager _screenManager;

    public void Awake()
    {
        if (_screenManager == null)
        {
            _screenManager = GameObject.FindGameObjectWithTag("ScreenManger").GetComponent<ScreenManager>();
        }
    }

    public void ReloadCurrentLevel()
    {
        ResetTimeScale();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
    }

	public void LoadLevel()
    {
        ResetTimeScale();
        SceneManager.LoadScene(levelToLoad);
    }

    public void QuitLevel()
    {
        ResetTimeScale();
        Application.Quit();
    }

    private void ResetTimeScale()
    {
        _screenManager.Resume();
    }
}
