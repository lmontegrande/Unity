using UnityEngine;
using System.Collections;

public class GunGuyController : MonoBehaviour {

    [SerializeField]
    private GameObject 
        bullet,
        sword,
        hand;

    [SerializeField]
    private float
        moveSpeed = 1f,
        sprintSpeed = 2f,
        bulletSpeed = 1f,
        swordSpeed = 1f,
        fireCooldown = .2f;

    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	private void Update () {
        LookAt();
        Move();
        Attack();
	}

    private void LookAt()
    {
        // Look at mouse location
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void Move()
    {
        // Get move input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Apply move input
        Vector2 movement = new Vector2(horizontal, vertical);
        if (Input.GetButton("Sprint"))
            movement *= sprintSpeed;
        else
            movement *= moveSpeed;

        _rigidBody.velocity = movement;
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bulletClone = (GameObject) Instantiate(bullet, hand.transform.position, Quaternion.identity);
            bulletClone.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
            Destroy(bulletClone, 1f);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            GameObject swordClone = (GameObject)Instantiate(sword, hand.transform.position, Quaternion.identity);
            swordClone.GetComponent<Rigidbody2D>().velocity = transform.up * swordSpeed;
            swordClone.transform.rotation = transform.rotation;
            Destroy(swordClone, .2f);
        }
    }
}
