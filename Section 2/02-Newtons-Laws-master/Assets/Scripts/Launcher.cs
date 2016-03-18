using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {

    public AudioClip windup;
    public AudioClip launch;
    public float maxLaunchSpeed;
    public float launchValInc = 1;
    public float launchVal;
    public GameObject ball;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    void OnMouseDown()
    {
        InvokeRepeating("IncreaseLaunchSpeed", 0, 0.1f);
        audioSource.clip = windup;
        audioSource.Play();
    }

    void OnMouseUp()
    {
        CancelInvoke("IncreaseLaunchSpeed");
        FireBall();
        audioSource.clip = launch;
        audioSource.Play();
        launchVal = 0;
    }

    void FireBall()
    {
        GameObject ballClone = (GameObject) Instantiate(ball, gameObject.transform.position, new Quaternion(0,0,0,0));
        GameObject.FindObjectOfType<UniversalGravitation>().addObject();

        PhysicsEngine physicsEngine = ballClone.GetComponent<PhysicsEngine>();
        physicsEngine.velocityVector = ((new Vector3(1, 1, 0)).normalized * launchVal);

        Destroy(ballClone, 4f);
    }

    void IncreaseLaunchSpeed()
    {
        if (launchVal < maxLaunchSpeed)
        {
            launchVal += launchValInc;
        }
    }
}
