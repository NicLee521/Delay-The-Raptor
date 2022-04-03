using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDeedHandler : MonoBehaviour
{
    public DeedObject thisDeed;
    public bool isShopDeed;
    public DeedObject[] shopDeeds;

    void Start(){
        if(isShopDeed && thisDeed == null){
            thisDeed = shopDeeds[(Random.Range(0,shopDeeds.Length))];
        }
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
