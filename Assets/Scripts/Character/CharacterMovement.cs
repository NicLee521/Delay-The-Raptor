using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CharacterMovement : MonoBehaviour
{
    public Animator animator; 
    private bool running;
    public float moveSpeed = 2;
    private Vector3 moveVector;
    public bool moveDisabled = false;

    void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        transform.position += moveVector * Time.deltaTime * moveSpeed;   
        transform.rotation = Quaternion.LookRotation(moveVector);   
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
