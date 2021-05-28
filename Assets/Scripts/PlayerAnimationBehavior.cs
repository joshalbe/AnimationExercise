using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationBehavior : MonoBehaviour
{
    private PlayerMovementBehaviour _movementBehavior;
    [SerializeField]
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the move component reference
        _movementBehavior = GetComponent<PlayerMovementBehaviour>();
    }

    public void ActivateWinTrigger()
    {
        _animator.SetTrigger("WinGame");
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Speed", _movementBehavior.Velocity.magnitude);
        _animator.SetBool("InAir", !_movementBehavior.IsGrounded);
    }
}
