using UnityEngine;
using System.Collections;

public class CircleRotateScript : MonoBehaviour {

    [SerializeField]
    private float rotationSpeed = 50f;

    private bool canRotate;

    private float angle;

	void Awake () {
        canRotate = true; StartCoroutine(ChangeRotation());
	}
	
	// Update is called once per frame
	void Update () {
	    if (canRotate)
        {
            RotateTheCircle();
        }
	}

    IEnumerator ChangeRotation()
    {
        yield return new WaitForSeconds(2f);

        if (Random.Range(0, 2) >= 1)
        {
            rotationSpeed = -Random.Range(50, 100);
        }
        else
        {
            rotationSpeed = Random.Range(50, 100);
        }

        StartCoroutine(ChangeRotation());
    }

    public void speedUp()
    {
        rotationSpeed += 10f;
    }

    public void slowDown()
    {
        if (rotationSpeed <= 0)
            return;

        rotationSpeed -= 10f;
    }

    void RotateTheCircle()
    {
        angle = transform.rotation.eulerAngles.z;
        angle += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
