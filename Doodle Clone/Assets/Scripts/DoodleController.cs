using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoodleController : MonoBehaviour {

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Scrollbar moveScrollBar;

    [SerializeField]
    private float 
        horizontalSpeed = 1f,
        bulletSpeed = 1f,
        sideShotModifier = 2f;

    [SerializeField]
    private bool 
        touchMode = false,
        isUsingAccelorometer = false;

    private Rigidbody2D _rigidBody;

    void Start()
    {

#if UNITY_ANDROID
        touchMode = true;
        isUsingAccelorometer = true;
#endif

#if UNITY_EDITOR
        touchMode = false;
        isUsingAccelorometer = false;
#endif

        _rigidBody = GetComponent<Rigidbody2D>();
        if (touchMode && !isUsingAccelorometer)
        {
            moveScrollBar.gameObject.SetActive(true);
        }
    }

	void Update()
    {
        if (!touchMode)
        {
            HandleInput();
        }
        else
        {
            HandleTouchInput();
        }

        UpdateCamera();
        
    }

    void HandleInput()
    {
        // Get and apply horizontal direction force

        _rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * horizontalSpeed, _rigidBody.velocity.y);
        if (Input.GetButtonDown("Fire1"))
        {
            Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    void HandleTouchInput()
    {
        if (!isUsingAccelorometer)
        {
            float moveVar = moveScrollBar.value * 2 - 1;
            _rigidBody.velocity = new Vector2(moveVar * horizontalSpeed, _rigidBody.velocity.y);
        }
        else
        {
            _rigidBody.velocity = new Vector2(Input.acceleration.x * horizontalSpeed, _rigidBody.velocity.y);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Fire(position);
        }
    }

    void UpdateCamera()
    {
        GameManager.instance.AdjustCamera(transform.position.y);
    }
    
    void Fire(Vector3 interactPosition)
    {
        Vector3 bulletDirection = (transform.position - interactPosition).normalized;
        GameObject bulletClone = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        bulletClone.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletDirection.x * bulletSpeed * sideShotModifier, bulletSpeed);
        Destroy(bulletClone, 1f);
    }
}
