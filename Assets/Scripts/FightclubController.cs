using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightclubController : MonoBehaviour
{

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.AddComponent<FightclubMingame>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
