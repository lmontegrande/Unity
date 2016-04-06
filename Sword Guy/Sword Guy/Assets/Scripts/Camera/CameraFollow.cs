using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public bool 
        followY = true, 
        useXBounds = false, 
        useYBounds = false;

    public float 
        xLowerBounds = 0, 
        xUpperBounds = 100,
        yLowerBounds = 0,
        yUpperBounds = 100;


    [SerializeField]
    float editY, editX, cameraSpeed = 5f;

    [SerializeField]
    GameObject target;
	
	void Update () {
        float lerpValx = Mathf.Lerp(transform.position.x, target.transform.position.x + editX, Time.deltaTime * cameraSpeed);
        if (lerpValx < xLowerBounds && useXBounds)
            lerpValx = xLowerBounds;
        if (lerpValx > xUpperBounds && useXBounds)
            lerpValx = xUpperBounds;

        float lerpValy;
        if (followY)
        {
            lerpValy = Mathf.Lerp(transform.position.y, target.transform.position.y + editY, Time.deltaTime * cameraSpeed);
            if (useYBounds)
            {
                if (lerpValy < yLowerBounds)
                    lerpValy = yLowerBounds;
                if (lerpValy > yUpperBounds)
                    lerpValy = yUpperBounds;
            }
        }
        else
            lerpValy = transform.position.y;
        Vector3 lerpedVector = new Vector3(lerpValx, lerpValy, transform.position.z);
        transform.position = lerpedVector;
    }
}
