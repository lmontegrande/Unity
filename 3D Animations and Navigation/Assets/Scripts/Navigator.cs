using UnityEngine;
using System.Collections;

/// <summary>
/// On your terrain, open Window->Navigation (I also like to decrease size of terrain - 
/// baking the default 2000x2000 terrain takes a bit. For testing 100x100 takes about one second to bake)
/// Place objects in terrain, mark as static (Navigation static if you want to be selective on the static type)
/// Go to navigation pane and bake data. I highlight terrain first so I can see the navmesh as blue highlight to see the areas
/// Then add a NavMeshAgent to the object you want to move through the terrain.
/// </summary>
public class Navigator : MonoBehaviour
{

    private NavMeshAgent _navMeshAgent;

    private Vector3 _destination;

    private Vector3 _startingLocation;

    // Use this for initialization
    void Start()
    {
        _startingLocation = transform.position;
        _destination = GameObject.FindGameObjectWithTag("Player").transform.position;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(_destination);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_navMeshAgent.pathPending
            &&
            _navMeshAgent.remainingDistance
            <=
            _navMeshAgent.stoppingDistance)
        // We arrived, reset the destination
        {
            Debug.Log("Arrived - done navigating, setting new location");
            _navMeshAgent.SetDestination(_startingLocation);

        }
    }
}
