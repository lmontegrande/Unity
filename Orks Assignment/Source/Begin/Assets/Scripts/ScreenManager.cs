using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour
{
    public bool pauseTimeEnabled = true;

    private float _originalTimeScale = 0;

    [SerializeField]
    private GameObject _paused;

    //Assume this is a fresh launch and not a resume.
    //Unity will tell us otherwise (ie if its a resume)
    private bool _gameJustLaunched = true;
    private bool _showResume;

    public bool Paused { get; set; }


	void Awake () {
		//Ensures when other scenes are loaded, we keep this class around.
		//Otherwise we'd have to implement a pause screen class for every level.
		//Using a static class is another viable option that would work as well, except 
		//OnApplicationPause that we use below is a MonoBehavior method, so we would need another workaround in that case.
        if (GameObject.FindGameObjectWithTag("ScreenManager") != gameObject)
        {
            Destroy(gameObject);
            return;
        }
		DontDestroyOnLoad(this);

		// unity now supports handling size changed in 4.3
#if UNITY_METRO && !UNITY_EDITOR
		UnityEngine.WSA.Application.windowSizeChanged += WindowSizeChanged;
#endif
	}

	
	/// <summary>
	/// Deal with windows resizing
	/// </summary>
	public void WindowSizeChanged(int width, int height)
	{
		//If user snaps us, go into pause for Windows Store. 
		if (width <= 500)
		{
			Pause();
		}
		else
		{
			Resume();
		}
	}

    // Use this for initialization
    void Start()
    {
        if(_paused == null)
        {
            Debug.LogError("Pause screen was null. Please set the reference in ScreenManager");
        }
        else
        {
            _paused.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // This is a terrible terrible workaround... But oh well!  Just for fun!
        if (_paused == null)
        {
            _paused = GameObject.Find("PauseScreen");
            _paused.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Pause the game
    public void Pause()
    {
        // Stop all audio from playing
        //AudioListener.pause = true;

        // Set the game's timescale to 0
        _originalTimeScale = Time.timeScale;
        if (pauseTimeEnabled)
            Time.timeScale = 0;

        Paused = true;
        _paused.SetActive(Paused);
    }

    // Unpause the game
    public void Resume()
    {
        if (!Paused)
        {
            return;
        }

        // Resume all audio playing
        AudioListener.pause = false;

        // Return game's timescale to original value
        Time.timeScale = _originalTimeScale;
        Paused = false;
        _paused.SetActive(Paused);

        Debug.Log("Test");
    }


    /// <summary>
    /// Raises the application pause event. Called when Unity detects a pause, ie app suspend.
    /// This is called with false when the user switches back to the app. 
    /// </summary>
    /// <param name="paused">If set to <c>true</c> paused.</param>
    private void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            Debug.Log("OnApplicationPause(true)");
            Pause();
        }
        else
        {
            Debug.Log("OnApplicationPause(false)");
            // Don't show resume button when game first loads
            // Unity will call this with OnApplicationPause(false) for a new game and for a resume.
            if (!_gameJustLaunched)
            {
                //If we made it here, we've already set _gameJustLaunched to false so this must be the user switching back to app.
                _showResume = true;
                Debug.Log("Game is already running, user must've just switched back to app. Setting _showResume = true");
            }
            else
            {
                //If we made it here, game must've been just launched. Set it to false for future calls.
                Debug.Log("Game must have just been launched, setting _gameJustLaunched = false");
                _gameJustLaunched = false;
            }
        }
    }
}
