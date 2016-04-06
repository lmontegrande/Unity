using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public float spawnTime;

    [SerializeField]
    GameObject enemy;

    private float currentTime = 0;
	
	// Update is called once per frame
	void Update () {
	    if (spawnTime <= currentTime)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
	}
}
