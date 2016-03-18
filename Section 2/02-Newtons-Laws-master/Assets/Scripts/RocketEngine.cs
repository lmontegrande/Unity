using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PhysicsEngine))]
public class RocketEngine : MonoBehaviour {

    public float fuelMass = 1;                                  // [kg]
    public float maxThrust;                                     // kN [kg m s^-2]

    [Range (0, 1f)]
    public float thrustPercent;                                 // [none]
    public Vector3 thrustUnitVector = new Vector3(0, 0, 0);     // [none]

    private PhysicsEngine physicsEngine;
    private float currentThrust;                                // N

    void Start ()
    {
        physicsEngine = GetComponent<PhysicsEngine>();
        physicsEngine.mass += fuelMass;
    }

    void FixedUpdate ()
    {
        float fuelThisUpdate = FuelThisUpdate();
        if (fuelMass > fuelThisUpdate)
        {
            fuelMass -= fuelThisUpdate;
            physicsEngine.mass -= fuelThisUpdate;
            ExertForce();
        } else
        {
            //Debug.LogWarning("Out of fuel");
        }
    }

    float FuelThisUpdate()
    {
        float exhaustMassFlow;          // [kg s^-1]
        float effectiveExhaustVelocity = 4462f; // [m s^-1]

        exhaustMassFlow = (currentThrust / effectiveExhaustVelocity);
       

        return exhaustMassFlow * Time.deltaTime;
    }

    void ExertForce()
    {
        currentThrust = thrustPercent * maxThrust * 1000f; // N
        Vector3 thrustVector = thrustUnitVector.normalized * currentThrust; // N
        physicsEngine.AddForce(thrustVector);
    }
}
