using UnityEngine;
using System.Collections;

public class MoveBetween : MonoBehaviour {

    [SerializeField]
    private Transform[] patrolPoints;

    [SerializeField]
    private float moveTime = 1f;

    private int currentPoint = 0;
    private int nextPoint = 1;
    private float currentMoveTime = 0f;
    private bool arrived = true;

    private void Start()
    {
        gameObject.transform.position = patrolPoints[0].transform.position;
    }

    private void Update()
    {
        if (arrived)
        {
            StartCoroutine(MoveToNext(patrolPoints[currentPoint], patrolPoints[nextPoint]));
        }
    }

    private IEnumerator MoveToNext(Transform previous, Transform next)
    {
        arrived = false;

        while (Vector3.Distance(transform.position, next.position) > 0.05f)
        {
            transform.position = Vector3.Lerp(previous.position, next.position, currentMoveTime/moveTime);
            currentMoveTime += Time.deltaTime;
            yield return null;
        }

        if (nextPoint == (patrolPoints.Length-1))
            nextPoint = 0;
        else
            nextPoint++;

        if (currentPoint == (patrolPoints.Length - 1))
            currentPoint = 0;
        else
            currentPoint++;

        currentMoveTime = 0f;
        arrived = true;
    }
}
