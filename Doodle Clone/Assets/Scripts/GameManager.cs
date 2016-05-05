using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [SerializeField]
    private GameObject
        frame,
        platform,
        monster,
        trap,
        killTrigger,
        currentTop,
        currentFrame,
        scoreText,
        retryButton;

    [SerializeField]
    private float
        cameraHeightAdjust = 0f,
        killTriggerAdjust = 0f;

    private GameObject _mainCamera;
    private GameObject _previousFrame = null;
    private float highestPoint = 0f;
    private int level = 1;

    void Start()
    {
        // Set up static reference
        if (instance == null)
        {
            GameManager.instance = this;
        }

        // Set up references
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        GeneratePlatforms();
    }

    public void AdjustCamera(float height)
    {
        if (height > highestPoint)
        {
            highestPoint = height;
            Transform cameraTransform = _mainCamera.transform;
            cameraTransform.position = new Vector3(cameraTransform.position.x, height + cameraHeightAdjust, cameraTransform.position.z);

            // Adjust Kill Trigger & Score
            killTrigger.transform.position = new Vector3(killTrigger.transform.position.x, height - killTriggerAdjust, killTrigger.transform.position.z);
            scoreText.GetComponent<Text>().text = "Score: " + (int) highestPoint;
        }

    }

    public void GameOver()
    {
        Time.timeScale = 1f;
        retryButton.SetActive(true);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GenerateFrame()
    {
        level++;

        // Slowly speed up game
        Time.timeScale = 1f + ((float) level * .1f);

        if (_previousFrame != null)
        {
            Destroy(_previousFrame);
        }

        _previousFrame = currentFrame;
        currentFrame = Instantiate(frame, currentTop.transform.position, Quaternion.identity) as GameObject;
        currentTop = currentFrame.GetComponent<Frame>().GetTop();

        GeneratePlatforms();
    }

    private void GeneratePlatforms()
    {
        // Difficulty based on level value.  Stages are 20 units high.  Each play should average about 15 sec.
        // Left and right range from -3 to 3
        Vector3 generateLocation = currentFrame.transform.position;
        for (int x=0; x < 20; x++)
        {
            // Select a platform
            GameObject platformToGenerate;

            // Make sure player has something to jump to
            if (x % 3 == 0)
            {
                platformToGenerate = platform;
            }
            else
            {
                int randomVar = Random.Range(0, 100);

                // Non Hazard
                if (randomVar <= 75)
                {
                    if (randomVar <= 75/level)
                    {
                        platformToGenerate = platform;
                    }
                    else
                    {
                        platformToGenerate = null;
                    }
                }
                // Hazard
                else
                {
                    if (randomVar <= 90)
                    {
                        platformToGenerate = trap;
                    }
                    else
                    {
                        platformToGenerate = monster;
                    }
                }

            }

            if (platformToGenerate != null)
            {
                GameObject tempPlatform = Instantiate(platformToGenerate, generateLocation + new Vector3(Random.Range(-2f, 2f), x, 0), Quaternion.identity) as GameObject;
            }
        }
    }
}
