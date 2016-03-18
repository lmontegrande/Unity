using UnityEngine;
using System.Collections;

public class MagnusEffect : MonoBehaviour {

    public float magnusConstant = 1f;

    private Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update () {
        rigidBody.AddForce(Vector3.Cross(rigidBody.angularVelocity, rigidBody.velocity) * magnusConstant);
	}
}
