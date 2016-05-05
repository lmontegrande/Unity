using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

    [SerializeField]
    private bool isLeftWall = true;

    private GameObject
        leftDoodle,
        rightDoodle;

    void Awake()
    {
        leftDoodle = GameObject.Find("Doodle Left Image");
        rightDoodle = GameObject.Find("Doodle Right Image");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isLeftWall)
                collision.gameObject.transform.position = rightDoodle.transform.position;
            else
                collision.gameObject.transform.position = leftDoodle.transform.position;
        }
    }
}
