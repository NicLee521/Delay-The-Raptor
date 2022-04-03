using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    public int karma;
    public float money;
    public float timeLeft;
    public Vector3 currentPosition;

    public bool timerIsRunning = true;
    private Animator animator; 

    public Slider karmaSliderNegitive;
    public Slider karmaSliderPositive;
    public TMP_Text timerText;
    public TMP_Text moneyText;
    private Image karmaFill;
    public GameObject heart;


    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        UpdatePlayerData();
        if(currentPosition != null &&  SceneManager.GetActiveScene().name == "Razorville"){
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
        }else if(karma <= -100){
            karmaSliderPositive.gameObject.SetActive(false);
            karmaSliderNegitive.value = -100;
            karmaFill = karmaSliderNegitive.fillRect.gameObject.GetComponent<Image>();
            karmaFill.color = Color.red;
        }else if(karma >= 100){
            karmaSliderNegitive.gameObject.SetActive(false);
            karmaSliderPositive.value = 100;
            karmaFill = karmaSliderPositive.fillRect.gameObject.GetComponent<Image>();
            karmaFill.color = Color.blue;
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
                timeLeft = 0;
                timerIsRunning = false;
                if(karma >= 50){
                    PlayerWin();
                }else{
                    PlayerLose();
                }
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

    void PlayerWin(){
        animator.SetBool("heavenBound", true);
        Vector3 heaven = transform.position;
        heaven.y += 50;
        StartCoroutine(LerpPosition(heaven,3));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    void PlayerLose(){

    }
}
