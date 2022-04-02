using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;


public class CharacterDeedHandler : MonoBehaviour
{
    public TMP_Text interactText;
    public GameObject dialogParent;
    public GameObject yesOrNo;

    private DeedObject currentDeed;
    private bool inInteractCol;
    private bool interactedWith;
    private CharacterMovement charMoveController;
    private bool autoFill;
    private bool dialogFinished;
    private TMP_Text dialogGiver;
    private TMP_Text dialogText;
    private bool goToNextDialog;
    private bool endOfDialog;


    // Start is called before the first frame update
    void Start()
    {
        charMoveController = this.gameObject.GetComponent<CharacterMovement>();
        dialogGiver = dialogParent.transform.Find("Dialog Giver").gameObject.GetComponent<TMP_Text>();
        dialogText = dialogParent.transform.Find("Dialog Text").gameObject.GetComponent<TMP_Text>();
        CleanUp();
    }

    void CleanUp(){
        interactText.gameObject.SetActive(false);
        dialogParent.SetActive(false);
        yesOrNo.SetActive(false);
        inInteractCol = false;
        interactedWith = false;
        autoFill = false;
        dialogFinished = false;
        goToNextDialog = false;
        endOfDialog = false;
        charMoveController.moveDisabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Deed"){
            currentDeed = other.gameObject.GetComponent<NpcDeedHandler>().thisDeed;
            if(currentDeed.isEnabled){
                interactText.text = "Press E to talk with " + currentDeed.npcName;
                interactText.gameObject.SetActive(true);
                inInteractCol = true;
            }
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
        if(inInteractCol && !interactedWith){
            charMoveController.moveDisabled = true;
            interactText.gameObject.SetActive(false);
            dialogGiver.text = currentDeed.npcName + ":";
            dialogParent.SetActive(true);
            dialogText.text ="";
            interactedWith = true;
        }
        if(interactedWith){
            goToNextDialog = true;
            StartCoroutine(DialogController());
        }
    }

    public void OnSkip(InputValue value){
        if(interactedWith){
            autoFill=true;
        }
        if(dialogFinished){
            goToNextDialog=true;
        }
        if(endOfDialog){
            yesOrNo.SetActive(true);
        }
    }

    public void OnYes(){
        currentDeed.Disable();
        SceneManager.LoadScene(currentDeed.miniGameSceneName, LoadSceneMode.Single);
    }

    public void OnNo(){
        CleanUp();
    }

    IEnumerator DialogController(){
        foreach(string dialog in currentDeed.dialog){
            autoFill = false;
            dialogText.text = "";
            yield return StartCoroutine(TextSlower(.06f, dialog, dialogText));
        }
        endOfDialog = true;
        StopCoroutine(DialogController());
    }

    IEnumerator TextSlower(float time, string theText, TMP_Text textBox)
    {
        dialogFinished = false;
        goToNextDialog = false;
        while(!goToNextDialog){
            if(!dialogFinished) {
                foreach (char ch in theText){
                    textBox.text += ch;
                    if(autoFill){
                        textBox.text = theText;
                        break;
                    }
                    yield return new WaitForSeconds(time);
                }
            }
            dialogFinished = true;
            yield return null;
        }
        StopCoroutine(TextSlower(0.6f, theText, textBox));
    } 

}
