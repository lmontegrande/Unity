using UnityEngine;
using System.Collections;

public class NeedleHeadScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D target)
    {
        Debug.Log("Needle Hit");
        Time.timeScale = 0f;
        if (ScoreManager.instance != null)
            ScoreManager.instance.GameOver();
    }
}
