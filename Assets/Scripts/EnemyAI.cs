using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 5;
    public float rotationSpeed = 3;

    private GameObject player;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce((player.transform.position - transform.position) * moveSpeed);
        if ((player.transform.position - transform.position) != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation((player.transform.position - transform.position)),
                Time.deltaTime * rotationSpeed
            ); 
        }
    }
}
