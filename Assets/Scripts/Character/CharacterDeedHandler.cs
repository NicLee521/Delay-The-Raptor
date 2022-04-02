using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;


public class CharacterDeedHandler : MonoBehaviour
{
    public TMP_Text interactText;
    public GameObject dialogParent;

    private DeedObject currentDeed;
    private bool inInteractCol = false;

    // Start is called before the first frame update
    void Start()
    {
        interactText.gameObject.SetActive(false);
        dialogParent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Deed"){
            currentDeed = other.gameObject.GetComponent<DeedObject>();
            interactText.text = "Press E to talk with " + currentDeed.npcName;
            interactText.gameObject.SetActive(true);
            inInteractCol = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Deed"){ 
            interactText.gameObject.SetActive(false);
            inInteractCol = false;
        }
    }

    public void OnInteract(InputValue value){
        if(inInteractCol){
            TMP_Text dialogGiver = dialogParent.transform.Find("Dialog Giver").gameObject.GetComponent<TMP_Text>();
            TMP_Text dialogText = dialogParent.transform.Find("Dialog Text").gameObject.GetComponent<TMP_Text>();
            dialogGiver.text = currentDeed.npcName + ":";
            dialogParent.SetActive(true);
            TextSlower(10, currentDeed.dialog[0], dialogText);
        }
    }

    IEnumerator TextSlower(float time, string theText, TMP_Text textBox)
    {
        Debug.Log("here");
        foreach (char ch in theText)
        {
            textBox.text += ch;
            // wait between each letter
            yield return new WaitForSeconds(time);
        }
        StopCoroutine(TextSlower(0.0f, theText, textBox));
    } 
}
