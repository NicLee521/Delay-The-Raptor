using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 5;
    public float rotationSpeed = 3;
    private int health = 2;

    private BoxCollider hitBox;
    private GameObject player;
    private Rigidbody rb;
    private GameObject bottomEnemy;
    private Animator runningAnimator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        hitBox = gameObject.AddComponent<BoxCollider>();
        hitBox.center = new Vector3(0.5f,1.0f,1.4f);
        hitBox.size = new Vector3(1.0f, 1.0f, 1.0f);
        hitBox.isTrigger = true;
        rb = gameObject.GetComponent<Rigidbody>();
        bottomEnemy = GameObject.Find("Kid@Surprise Uppercut");
        runningAnimator = bottomEnemy.GetComponent<Animator>();
        StartCoroutine(Punch());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((player.transform.position - transform.position) != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation((player.transform.position - transform.position)),
                Time.deltaTime * rotationSpeed
            ); 
        }
        if(Vector3.Distance(player.transform.position, transform.position) < 2.5f){
            runningAnimator.SetBool("isRunning", false);
        }else{
            rb.AddForce((player.transform.position - transform.position) * moveSpeed);
            runningAnimator.SetBool("isRunning", true);
        }
    }

    IEnumerator Punch(){
        while(gameObject){
            hitBox.enabled = true;
            yield return new WaitForSeconds(.75f);
            hitBox.enabled = false;
            yield return new WaitForSeconds(2.0f);
        }
    }

    private void OnTriggerEnter(Collider other){
        if(!other.isTrigger){
            return;
        }else if(other.gameObject.tag == "Player"){
            if(health > 0){
                health -= 1;
            }else{
                player.gameObject.SendMessage("EnemyDefeated");
                Destroy(gameObject);
            }
        }
    }
}
