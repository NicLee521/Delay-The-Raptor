using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private Rigidbody rb;
    public int speed = 5;
    public float timeAlive = 4.0f;

    public Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(moveDirection*speed);
    }

    void Update(){
        if (timeAlive > 0){
            timeAlive -= Time.deltaTime;
        } else {
            gameObject.SetActive(false);
        }
    }
}
