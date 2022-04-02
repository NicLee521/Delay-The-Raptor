using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 2;
    private Vector3 moveVector;
    // Update is called once per frame
    void Update()
    {
        transform.position += moveVector * Time.deltaTime * moveSpeed;
    }
    public void OnMove(InputValue value){
        moveVector = value.Get<Vector3>();
    }
}
