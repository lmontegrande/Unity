using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointsTimer : MonoBehaviour {

    public Text Points;

    private int _timer;
    private Coroutine _pointsTimer;

	// Use this for initialization
	void Start () {
        _pointsTimer = StartCoroutine(CoPointsTimer());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator CoPointsTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _timer += 10;
            Points.text = "" + _timer;
        }
    }
}
