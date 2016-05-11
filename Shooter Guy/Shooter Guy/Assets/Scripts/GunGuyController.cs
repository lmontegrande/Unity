using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunGuyController : MonoBehaviour {

    [SerializeField]
    private GameObject 
        bullet,
        sword,
        hand;

    [SerializeField]
    private Text 
        _attackChargeText,
        _teleportChargeText;

    [SerializeField]
    private int health = 100;

    [SerializeField]
    private float
        moveSpeed = 1f,
        sprintSpeed = 2f,
        bulletSpeed = 1f,
        swordSpeed = 1f,
        teleportDistance = 1f,
        fireCooldown = .2f,
        swordChargeTime = 3f,
        teleportChargeTime = 2f,
        accelerometerAdjust = 2f,
        controlFactor = 1f;

    [SerializeField]
    private bool touchMode = false;

    private Rigidbody2D _rigidBody;
    private float fireCharge = 0;
    private float teleportCharge = 0;
    private bool isShootingButtonDown = false;
    private bool isTouchBeenReset = true;
    private bool dead = false;

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        touchMode = true;
#else
        accelerometerAdjust = 1f;
#endif
        _rigidBody = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	private void Update () {
        if (dead)
        {
            return;
        }

        UpdateHUD();
        LookAt();
        ActionInput();
	}

    private void UpdateHUD()
    {
        if (fireCharge > swordChargeTime)
        {
            _attackChargeText.text = "SWORD READY!";
        }
        else
        {
            _attackChargeText.text = fireCharge.ToString();
        }

        if (teleportCharge > teleportChargeTime)
        {
            _teleportChargeText.text = "TELEPORT READY!";
        }
        else
        {
            _teleportChargeText.text = teleportCharge.ToString();
        }
    }

    private void LookAt()
    {
        // Look at mouse location
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void Move(float horizontal, float vertical, bool sprint)
    {
        // Get move input

        // Apply move input
        Vector2 movement = new Vector2(horizontal, vertical);
        if (sprint)
            movement *= sprintSpeed;
        else
            movement *= moveSpeed;

        _rigidBody.velocity = Vector2.MoveTowards(_rigidBody.velocity, movement * accelerometerAdjust, controlFactor);
    }

    private void Fire()
    {
        isShootingButtonDown = false;
        if (fireCharge < swordChargeTime)
        {
            GameObject bulletClone = (GameObject)Instantiate(bullet, hand.transform.position, Quaternion.identity);
            bulletClone.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
            Destroy(bulletClone, 1f);
        }
        else
        {
            GameObject swordClone = (GameObject)Instantiate(sword, hand.transform.position, Quaternion.identity);
            swordClone.GetComponent<Rigidbody2D>().velocity = transform.up * swordSpeed;
            swordClone.transform.rotation = transform.rotation;
            Destroy(swordClone, .2f);
        }

    }

    private void Teleport(Vector3 teleportLocation)
    {
        if (teleportCharge >= teleportChargeTime)
        {
            Vector3 target = new Vector3(teleportLocation.x, teleportLocation.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, teleportDistance);
            teleportCharge = 0;
        }
    }

    private void ActionInput()
    {
        if (!touchMode)
        {
            MoveInput();
            FireInput();
            TeleportInput();
        }
        else
        {
            if (Input.touches.Length == 0)
            {
                isTouchBeenReset = true;
            }

            TouchMoveInput();
            TouchFireInput();
            TouchTeleportInput();
        }

        if (isShootingButtonDown)
        {
            fireCharge += Time.deltaTime;
        }
        else
        {
            fireCharge = 0;
        }

        teleportCharge += Time.deltaTime;
    }

    // KEYBOARD INPUT BEGIN ------------------------------------------------
    private void MoveInput()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetButton("Sprint"));
    }

    private void FireInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isShootingButtonDown = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Fire();
        }
    }

    private void TeleportInput()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Teleport(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    // KEYBOARD INPUT END ------------------------------------------------

    // TOUCH INPUT BEGIN ------------------------------------------------
    private void TouchMoveInput()
    {
        Move(Input.acceleration.x, Input.acceleration.y, true);
    }

    private void TouchFireInput()
    {
        if (!isTouchBeenReset)
        {
            return;
        }

        if (Input.touches.Length == 1 && Input.touches[0].phase == TouchPhase.Began)
        {
            isShootingButtonDown = true;
        }

        if (Input.touches.Length == 1 && Input.touches[0].phase == TouchPhase.Ended)
        {
            if (isShootingButtonDown)
                Fire();
        }
    }

    private void TouchTeleportInput()
    {
        if (Input.touches.Length == 2 && Input.touches[1].phase == TouchPhase.Began)
        {
            Teleport(Camera.main.ScreenToWorldPoint(Input.touches[1].position));
            isTouchBeenReset = false;
            isShootingButtonDown = false;
        }
    }
    // TOUCH INPUT END ------------------------------------------------

    private void Die()
    {
        dead = true;
    }

    public void GetHit(int damage, Vector3 otherPosition, float knockBack)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
            return;
        }

        Vector2 difference = transform.position - otherPosition;
        _rigidBody.velocity += difference * knockBack;
    }

    
}
