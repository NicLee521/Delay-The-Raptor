using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FightclubMingame : MonoBehaviour
{

    private BoxCollider hitBox;

    // Start is called before the first frame update
    void Awake()
    {
        hitBox = gameObject.AddComponent<BoxCollider>();
        hitBox.center = new Vector3(0.0f,1.0f,.75f);
        hitBox.size = new Vector3(1.5f, 1.5f, 1.0f);
        hitBox.isTrigger = true;
        hitBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSwing(InputValue value){
        if(!hitBox.enabled){
            Debug.Log("PUNCH");
            StartCoroutine(Punch());
        }
    }

    IEnumerator Punch(){
        hitBox.enabled = true;
        yield return new WaitForSeconds(2.0f);
        hitBox.enabled = false;
    }
}
