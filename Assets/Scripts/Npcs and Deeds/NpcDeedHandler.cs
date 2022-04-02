using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDeedHandler : MonoBehaviour
{
    public DeedObject thisDeed;

    void Start(){
        UpdateFromGlobal();
    }

    void UpdateFromGlobal(){
        if(GloabalStats.Instance.deeds.ContainsKey(thisDeed.npcName)){
            thisDeed.isEnabled = GloabalStats.Instance.deeds[thisDeed.npcName];
        }else{
            thisDeed.isEnabled = true;
        }
    }
}
