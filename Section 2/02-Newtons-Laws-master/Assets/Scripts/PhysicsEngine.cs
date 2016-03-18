using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsEngine : MonoBehaviour {
    public bool showTrails = true;
    public float mass = 1;                                  // [kg]
    public Vector3 velocityVector = new Vector3(1, 1, 1);   // [m s^-1]
    public Vector3 netForceVector;                          // N [kg m s^-2]

    public List<Vector3> forceVectorList = new List<Vector3>(); // m/s
    private LineRenderer lineRenderer;
    private int numberOfForces;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetColors(Color.yellow, Color.yellow);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.useWorldSpace = false;
    }

    void FixedUpdate()
    {
        RenderTrails();
        UpdatePosition();
    }

    void RenderTrails()
    {
        // Use the lineRenderer to display the force trails
        if (showTrails)
        {
            lineRenderer.enabled = true;
            numberOfForces = forceVectorList.Count;
            lineRenderer.SetVertexCount(numberOfForces * 2);
            int i = 0;
            foreach (Vector3 forceVector in forceVectorList)
            {
                lineRenderer.SetPosition(i, Vector3.zero);
                lineRenderer.SetPosition(i + 1, -forceVector);
                i = i + 2;
            }
             //lineRenderer.SetVertexCount(2);
             //lineRenderer.SetPosition(0, Vector3.zero);
             //lineRenderer.SetPosition(1, -velocityVector);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void OnDestroy()
    {
        GameObject.FindObjectOfType<UniversalGravitation>().addObject();
    }

    public void AddForce(Vector3 forceVector)
    {
        forceVectorList.Add(forceVector);
    }

    void UpdatePosition()
    {
        // Set netForce back to zero
        netForceVector = Vector3.zero;

        // Sum up Vectors
        foreach (Vector3 force in forceVectorList)
        {
            netForceVector += force;
        }

        forceVectorList = new List<Vector3>();

        // Update velocity
        Vector3 accelerationVector = netForceVector / mass;
        velocityVector += accelerationVector * Time.deltaTime;

        // Update position
        Vector3 deltaS = velocityVector * Time.deltaTime;
        transform.position += deltaS;
    }
}
