using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UniversalGravitation : MonoBehaviour
{
    private PhysicsEngine[] physicsEngineArray;

    private const float GRAVITY = 6.674e-11f; // N * (m/kg)2 [ m^3 s^-2 kg^-1]

    void Start()
    {
        physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>();
    }

    void FixedUpdate()
    {
        CalculateGravity();
    }

    public void addObject()
    {
        physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>();
    }

    void CalculateGravity()
    {
        foreach (PhysicsEngine physicsEngineA in physicsEngineArray)
        {
            foreach (PhysicsEngine physicsEngineB in physicsEngineArray)
            {
                if (physicsEngineA != physicsEngineB)
                {
                    //Debug.Log("Calculating force exerted on " + physicsEngineA + " due to gravity by " + physicsEngineB);

                    Vector3 offset = physicsEngineA.transform.position - physicsEngineB.transform.position;
                    float rSquared = Mathf.Pow(offset.magnitude, 2);
                    float gravityMagnitute = GRAVITY * physicsEngineA.mass * physicsEngineB.mass / rSquared;
                    Vector3 gravityFeltVector = gravityMagnitute * offset.normalized;

                    physicsEngineA.AddForce(-gravityFeltVector);
                }
            }
        }
    }
}
