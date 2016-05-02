using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour {

    public Text timeText;

    [SerializeField]
    private int startingTime = 60;

    [SerializeField]
    private float _timeUnitInSeconds = 1f;

    private int _timeLeft;

	// Use this for initialization
	void Start () {
        _timeLeft = startingTime;
        StartCoroutine(CoLevelCountDown());
	}

    IEnumerator CoLevelCountDown()
    {
        while (_timeLeft > 0)
        {
            _timeLeft--;
            timeText.text = "" + _timeLeft;
            yield return new WaitForSeconds(_timeUnitInSeconds);
        }

        // Time's up
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddTime(int time)
    {
        if (_timeLeft > 0)
        {
            _timeLeft += time;
            timeText.text = "" + _timeLeft;
        }
    }
}
