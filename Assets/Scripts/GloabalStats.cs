using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GloabalStats : MonoBehaviour
{
    public static GloabalStats Instance;


    public int playerKarma;
    public float playerMoney;
    public float playerTimeLeft;
    public Dictionary<string,bool> deeds = new Dictionary<string,bool>();


    void Awake ()   
    {
        if (Instance == null)
        {
            GetAllDeeds();
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
    }

    void GetAllDeeds(){
        DeedObject[] deedArray = FindObjectsOfType<DeedObject>();
        foreach (DeedObject deed in deedArray){
            deeds.Add(deed.npcName, deed.isEnabled);
        }
    }
}
