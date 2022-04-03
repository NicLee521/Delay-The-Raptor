using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogWalkCharacterController : MonoBehaviour
{
    public float allowedTimeOut = 5.0f;
    private float timeLeft;
    public bool isNear = true;
    private bool isOver = false;
    private GameObject dog;

    // Update is called once per frame
    void Update()
    {
        if(!isNear && !isOver){
            if (timeLeft > 0){
                timeLeft -= Time.deltaTime;
            } else{
                isOver = true;
                Time.timeScale = 0.0f;
                timeLeft = 0;
                dog.SendMessage("OnLose");
            }
        }
    }

    public void OnCreate(GameObject creator){
        dog = creator;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Dog"){ 
            isNear = false;
            timeLeft = allowedTimeOut;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Dog"){
            isNear = true;
        }
    }
}
