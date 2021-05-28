using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementBehaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _jumpScale;
    private Vector3 _velocity;
    [SerializeField]
    private bool _isGrounded;
    private bool _canMove = true;
    private Collider _collider;
    private float _distanceToGround;
    private float _moveDirection;

    public Vector3 Velocity
    {
        get
        {
            return _velocity;
        }
    }

    public float MoveDirection
    {
        get
        {
            return _moveDirection;
        }
    }

    public bool IsGrounded { get => _isGrounded; }

    // Start is called before the first frame update
    void Start()
    {
        //Initialize component references
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        //Get the the distance from the object center to the ground 
        _distanceToGround = _collider.bounds.extents.y;
    }

    public void DisableMovement()
    {
        _canMove = false;
    }

    private void FixedUpdate()
    {
        //If the player isn't supposed to move, set velocity to zero and return
        if (!_canMove)
        {
            _velocity = Vector3.zero;
            return;
        }

        // Cast a ray downward to see if the player is touching the ground
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _distanceToGround + 0.01f);

        //Update position based on player input
        _moveDirection = Input.GetAxis("Horizontal");
        _velocity.x =  _moveDirection * _moveSpeed;
        _rigidbody.MovePosition(transform.position + _velocity * Time.deltaTime);

        //If the player pressed the jump button and is on the gorund, add a force upwards
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(Vector3.up * _jumpScale, ForceMode.Impulse);
            _isGrounded = false;
        }

        //Set the rigidbody to be kinematic if the player is back on the ground
        _rigidbody.isKinematic = _isGrounded;

        //Update player rotation
        if (Velocity.magnitude > 0)
            transform.forward = Velocity.normalized;
    }
}
