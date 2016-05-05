using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoodleController : MonoBehaviour {

    [SerializeField]
    private Scrollbar moveScrollBar;

    [SerializeField]
    private float horizontalSpeed = 1f;

    [SerializeField]
    private bool touchMode = false;

    private Rigidbody2D _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        if (touchMode)
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
    }

    void HandleTouchInput()
    {
        float moveVar = moveScrollBar.value * 2 - 1;
        _rigidBody.velocity = new Vector2(moveVar * horizontalSpeed, _rigidBody.velocity.y);
    }

    void UpdateCamera()
    {
        GameManager.instance.AdjustCamera(transform.position.y);
    }
    

}
