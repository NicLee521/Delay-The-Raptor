using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CharacterMovement : MonoBehaviour
{
    public Animator animator; 
    public float moveSpeed = 2;
    public float rotationSpeed = 2;
    public bool moveDisabled = false;

    private Rigidbody rb;
    private Vector3 moveVector;
    private bool running;

    void Start() {
        animator = GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(moveVector * moveSpeed);
        if (moveVector != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(moveVector),
                Time.deltaTime * rotationSpeed
            ); 
        }
    }

    public void OnMove(InputValue value){      
        if(!moveDisabled){
            animator.SetBool("isRunning", true);
            moveVector = value.Get<Vector3>();
        } if(value.Get<Vector3>() == Vector3.zero) {
            animator.SetBool("isRunning", false);
        }
    }
}
