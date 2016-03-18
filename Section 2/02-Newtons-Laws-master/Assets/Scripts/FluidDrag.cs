using UnityEngine;
using System.Collections;

public class FluidDrag : MonoBehaviour {

    [Range(1, 2f)]
    public float velocityExponent; // [none]

    public float dragConstant; // 

    private PhysicsEngine physicsEngine; 

    private const float airDensity = 1.2041f; // [kg m^-3] 
    private const float sphereDragCoefficient = 0.47f;

	// Use this for initialization
	void Start () {
        physicsEngine = gameObject.GetComponent<PhysicsEngine>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 velocityVector = physicsEngine.velocityVector;
        float speed = velocityVector.magnitude;
        float dragSize = CalculateDrag(speed);
        Vector3 dragVector = dragSize * velocityVector.normalized;

        physicsEngine.AddForce(-dragVector);
	}

    float CalculateDrag(float speed)
    {

        float dragForce = dragConstant * Mathf.Pow(speed, velocityExponent);
        return dragForce;
    }
}
