using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloabalStats : MonoBehaviour
{
    public int playerKarma;
    public float playerMoney;
    public float playerTimeLeft;

    public static GloabalStats Instance;

    void Awake ()   
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
