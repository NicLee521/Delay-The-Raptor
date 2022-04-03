using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FightclubController : MonoBehaviour
{
    public GameObject fightclubEntry;
    public GameObject heart;

    private GameObject player;
    private GameObject noMoneyText;
    private CharacterStats charStats;
    private GameObject paidOff;
    private GameObject beatUp;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
        player = GameObject.Find("Player");
        charStats = player.GetComponent<CharacterStats>();
        player.AddComponent<FightclubMingame>();
        noMoneyText = GameObject.Find("No Money");
        paidOff = GameObject.Find("Paid");
        beatUp = GameObject.Find("Beat");

        noMoneyText.SetActive(false);
        paidOff.SetActive(false);
        beatUp.SetActive(false);
    }

    public void onPay(){
        if(charStats.money < 20){
            noMoneyText.SetActive(true);
        }else {
            charStats.money -= 20;
            charStats.karma += 25;
            GloabalStats.Instance.oldManOutcomes = GloabalStats.Outcomes.Good;
            fightclubEntry.SetActive(false);
            charStats.SavePlayerData();
            paidOff.SetActive(true);
        }
    }

    public void onBeat(){
        fightclubEntry.SetActive(false);
        beatUp.SetActive(true);
    }

    public void onFight(){
        beatUp.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ChangeScene(){
        SceneManager.LoadScene("Razorville", LoadSceneMode.Single);
    }

}
