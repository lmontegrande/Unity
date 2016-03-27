using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public bool followY = true;


    [SerializeField]
    float editY, editX, cameraSpeed = 5f;

    [SerializeField]
    GameObject target;
	
	// Update is called once per frame
	void Update () {
        float lerpValx = Mathf.Lerp(transform.position.x, target.transform.position.x + editX, Time.deltaTime * cameraSpeed);
        float lerpValy;
        if (followY)
            lerpValy = Mathf.Lerp(transform.position.y, target.transform.position.y + editY, Time.deltaTime * cameraSpeed);
        else
            lerpValy = transform.position.y;
        Vector3 lerpedVector = new Vector3(lerpValx, lerpValy, transform.position.z);
        transform.position = lerpedVector;

        /*
        if (followY)
            transform.position = new Vector3(target.transform.position.x + editX, target.transform.position.y + editY, transform.position.z);
        else
            transform.position = new Vector3(target.transform.position.x + editX, transform.position.y, transform.position.z);
            */
    }
}
