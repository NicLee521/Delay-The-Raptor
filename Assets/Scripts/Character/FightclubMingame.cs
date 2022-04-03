using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FightclubMingame : MonoBehaviour
{

    private BoxCollider hitBox;
    private Animator animator; 
    private int health = 5;
    
    private GameObject healthBar;
    private GameObject gotBeatUp;
    private CharacterStats charStats; 
    private GameObject enemyDefeated;


    // Start is called before the first frame update
    void Awake()
    {
        charStats = gameObject.GetComponent<CharacterStats>();
        gotBeatUp = GameObject.Find("Got Beat Up");
        gotBeatUp.SetActive(false);
        enemyDefeated = GameObject.Find("Victory");
        enemyDefeated.SetActive(false);
        healthBar = GameObject.Find("Health");
        for(int i = 0; i <= 4; i++){
            Instantiate(charStats.heart,healthBar.transform);
        }
        animator = GetComponentInChildren<Animator>();
        hitBox = gameObject.AddComponent<BoxCollider>();
        hitBox.center = new Vector3(0.0f,1.0f,.75f);
        hitBox.size = new Vector3(1.5f, 1.5f, 1.0f);
        hitBox.isTrigger = true;
        hitBox.enabled = false;
    }


    public void OnSwing(InputValue value){
        if(!hitBox.enabled){
            StartCoroutine(Punch());
        }
    }

    IEnumerator Punch(){
        animator.SetBool("isPunching", true);
        yield return new WaitForSeconds(.75f);
        hitBox.enabled = true;
        yield return new WaitForSeconds(.75f);
        animator.SetBool("isPunching", false);
        hitBox.enabled = false;
    }

    private void OnTriggerEnter(Collider other){
        if(!other.isTrigger){
            return;
        }else if(other.gameObject.tag == "Enemy"){
            if(health > 1){
                health -= 1;
                Destroy(healthBar.transform.GetChild(0).gameObject);
            }else{
                Destroy(healthBar.transform.GetChild(0).gameObject);
                Time.timeScale = 0.0f;
                gotBeatUp.SetActive(true);
                charStats.money = 0;
                charStats.karma -= 25;
                charStats.SavePlayerData();
            }
        }
    }

    public void EnemyDefeated(){
        Time.timeScale = 0.0f;
        charStats.karma -= 10;
        charStats.SavePlayerData();
        enemyDefeated.SetActive(true);
    }
}
