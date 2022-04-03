using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;


public class DogWalk : MonoBehaviour
{
    private GameObject start;
    private GameObject win;
    private GameObject lose;
    private GameObject player;
    private CharacterStats charStats;
    private IEnumerator coroutine;

    [Serializable]
    public struct Waypoints{
        public Transform waypoint;
        public float travelTime;
    }

    public Waypoints[] waypoints;
    // Start is called before the first frame update
    void Start()
    {
        start = GameObject.Find("Start");
        win = GameObject.Find("Win");
        lose = GameObject.Find("Lose");
        player = GameObject.Find("Player");
        charStats = player.GetComponent<CharacterStats>();
        player.AddComponent<DogWalkCharacterController>();
        player.SendMessage("OnCreate",gameObject);
        start.SetActive(true);
        win.SetActive(false);
        lose.SetActive(false);

    }

    public void OnStart(){
        start.SetActive(false);
        coroutine = GoToWaypoints();
        StartCoroutine(coroutine);
    }

    IEnumerator GoToWaypoints(){
        foreach(Waypoints waypoint in waypoints){
            transform.DOMove(waypoint.waypoint.position, waypoint.travelTime);
            yield return new WaitForSeconds(waypoint.travelTime +1f);
        }
        OnWin();
    }

    public void ChangeScene(){
        SceneManager.LoadScene("Razorville", LoadSceneMode.Single);
    }

    public void OnLose(){
        StopCoroutine(coroutine);
        charStats.karma -= 25;
        charStats.SavePlayerData();
        lose.SetActive(true);
    }

    private void OnWin(){
        Time.timeScale = 0.0f;
        charStats.karma += 25;
        charStats.money += 20;
        charStats.SavePlayerData();
        win.SetActive(true);
    }
}
