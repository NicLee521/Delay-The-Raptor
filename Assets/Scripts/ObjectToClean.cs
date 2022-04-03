using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectToClean : MonoBehaviour
{

    private float timeLeft;
    private GameObject graffitiController;
    // Start is called before the first frame update
    public void Spawned(GameObject controller)
    {
        graffitiController = controller;
        GraffitiController script = graffitiController.GetComponent<GraffitiController>();
        timeLeft = Random.Range(script.minTimeAlive, script.maxTimeAlive);
        StartCoroutine(DeleteAfterTime(timeLeft, graffitiController));
    }

    public void Clicked(){
        graffitiController.SendMessage("AddPoints");
        Destroy(gameObject);
    }

    IEnumerator DeleteAfterTime(float timeAlive, GameObject cont){
        yield return new WaitForSeconds(timeAlive);
        cont.gameObject.SendMessage("SubtractPoints");
        Destroy(gameObject);
    }
}
