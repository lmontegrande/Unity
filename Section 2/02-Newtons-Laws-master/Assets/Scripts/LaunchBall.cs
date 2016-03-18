using UnityEngine;
using System.Collections;

public class LaunchBall : MonoBehaviour {

    public Vector3 launchVelocity;
    public Vector3 initialW;

    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = launchVelocity;
        rigidBody.angularVelocity = initialW;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
