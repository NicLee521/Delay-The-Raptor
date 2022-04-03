using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMinigame : MonoBehaviour
{
    public GameObject loseContainer;
    public GameObject oofContainer;
    public GameObject winContainer;

    private CharacterStats charStats;
    private CarSpawn gameController;
    // Start is called before the first frame update
    void Awake()
    {
        gameController = GameObject.Find("MinigameController").GetComponent<CarSpawn>();
        loseContainer = GameObject.Find("Lose Container");
        winContainer = GameObject.Find("Win Container");
        oofContainer = GameObject.Find("Win and Lose Container");
        charStats = gameObject.GetComponent<CharacterStats>();
        loseContainer.SetActive(false);
        winContainer.SetActive(false);
        oofContainer.SetActive(false);
        charStats.timerIsRunning = false;
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("Here");
        if(other.gameObject.tag == "Car"){
            gameController.notFinished = false;
            Time.timeScale = 0.0f;
            charStats.karma -= 25;
            GloabalStats.Instance.mrsJenkinsOutcome = GloabalStats.Outcomes.Bad;
            loseContainer.SetActive(true);
            charStats.SavePlayerData();
        }
        if(other.gameObject.tag == "Win"){
            Time.timeScale = 0.0f;
            if(Random.Range(0.0f,100.0f) < 50.0f) {
                winContainer.SetActive(true);
                GloabalStats.Instance.mrsJenkinsOutcome = GloabalStats.Outcomes.Good;
                charStats.karma += 25;
            }else{
                oofContainer.SetActive(true);
                GloabalStats.Instance.mrsJenkinsOutcome = GloabalStats.Outcomes.Other;
                charStats.karma -= 25;
            }
            charStats.SavePlayerData();
        }
    }
}
