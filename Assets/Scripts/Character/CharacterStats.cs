using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    public int karma;
    public float money;
    public float timeLeft;
    public Vector3 currentPosition;

    public bool timerIsRunning = true;

    public Slider karmaSliderNegitive;
    public Slider karmaSliderPositive;
    public TMP_Text timerText;
    public TMP_Text moneyText;
    private Image karmaFill;
    public GameObject heart;


    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayerData();
        if(currentPosition != null){
            this.transform.position = currentPosition;
        }
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

    public void UpdatePlayerData(){
        karma = GloabalStats.Instance.playerKarma;
        money = GloabalStats.Instance.playerMoney;
        timeLeft = GloabalStats.Instance.playerTimeLeft;
        currentPosition = GloabalStats.Instance.currentPosition;
        KarmaSliderHandler();
        UpdateMoney();
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

    public void SavePlayerData(){
        GloabalStats.Instance.playerKarma = karma;
        GloabalStats.Instance.playerMoney = money;
        GloabalStats.Instance.playerTimeLeft = timeLeft;
        GloabalStats.Instance.currentPosition = currentPosition;
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
