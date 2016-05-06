using UnityEngine;
using System.Collections;

public class MaxController : MonoBehaviour {

    [SerializeField]
    float 
        moveSpeed = 2f,
        rotateSpeed = 5f,
        jumpForce = 10f;

    private Rigidbody _rigidBody;
    private CharacterController _characterController;
    private Animator _animator;

	void Start () {
        _rigidBody = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
        float adjust = moveSpeed;

        if (Input.GetButton("Sprint"))
        {
            adjust = adjust * 2;
        }

        _rigidBody.velocity = new Vector3(0, _rigidBody.velocity.y, 0) + transform.forward * Input.GetAxis("Vertical") * adjust;
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        _animator.SetFloat("MoveSpeed", _rigidBody.velocity.magnitude);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetTrigger("Punch");
        }
        if (Input.GetButtonDown("Fire2"))
        {
            _animator.SetTrigger("Kick");
        }
        if (Input.GetButtonDown("Jump"))
        {
            _animator.SetTrigger("Jump");
            _rigidBody.velocity += new Vector3(_rigidBody.velocity.x, jumpForce, _rigidBody.velocity.z);
        }
    }
}
