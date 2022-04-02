using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    private int karma;
    private float money;
    private float timeLeft;

    private bool timerIsRunning = true;

    public Slider karmaSliderNegitive;
    public Slider karmaSliderPositive;
    public TMP_Text timerText;
    public TMP_Text moneyText;
    private Image karmaFill;


    private GloabalStats thisGlobalStats;
    // Start is called before the first frame update
    void Start()
    {
        thisGlobalStats = GameObject.Find("GlobalManager").GetComponent<GloabalStats>();
        karma = thisGlobalStats.playerKarma;
        money = thisGlobalStats.playerMoney;
        timeLeft = thisGlobalStats.playerTimeLeft;
        KarmaSliderHandler();
        UpdateMoney();
    }

    void KarmaSliderHandler() {
        if(karma > 0){
            karmaSliderNegitive.gameObject.SetActive(false);
            karmaSliderPositive.value = karma;
            karmaFill = karmaSliderPositive.fillRect.gameObject.GetComponent<Image>();
            karmaFill.color = Color.blue;
        } else if(karma < 0){
            karmaSliderPositive.gameObject.SetActive(false);
            karmaSliderNegitive.value = karma*-1;
            karmaFill = karmaSliderNegitive.fillRect.gameObject.GetComponent<Image>();
            karmaFill.color = Color.red;
        }else{
            karmaSliderNegitive.value = 0;
            karmaSliderPositive.value = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                DisplayTime(timeLeft);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeLeft = 0;
                timerIsRunning = false;
            }
        }
    }

    void SavePlayerData(){
        thisGlobalStats.playerKarma = karma;
        thisGlobalStats.playerMoney = money;
        thisGlobalStats.playerTimeLeft = timeLeft;
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void UpdateMoney(){
        moneyText.text = "$" + money;
    }
}
