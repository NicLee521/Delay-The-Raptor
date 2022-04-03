using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDeedHandler : MonoBehaviour
{
    public DeedObject thisDeed;
    public bool isShopDeed;
    public DeedObject[] shopDeeds;
    public Material material;

    void Start(){
        if(isShopDeed && thisDeed == null){
            thisDeed = shopDeeds[(Random.Range(0,shopDeeds.Length))];
        }
        UpdateFromGlobal();
        UpdateOnResult();
    }

    void UpdateFromGlobal(){
        if(GloabalStats.Instance.deeds.ContainsKey(thisDeed.npcName)){
            thisDeed.isEnabled = GloabalStats.Instance.deeds[thisDeed.npcName];
        }else{
            thisDeed.isEnabled = true;
        }
    }

    void UpdateOnResult(){
        if(!thisDeed.isEnabled){
            switch(thisDeed.npcName){
                case "Mrs.Jenkins":
                    MrsJenkinsUpdate();
                    break;
                case "Old Man Jenkins":
                    OldManJenkinsUpdate();
                    break;
                case "Derrik":
                    WallUpdate();
                    break;
                default:
                    break;
            }
        }
    }

    void MrsJenkinsUpdate(){
        switch(GloabalStats.Instance.mrsJenkinsOutcome){
            case GloabalStats.Outcomes.Good:
                transform.parent.position = GameObject.Find("Grandma Good").transform.position;
                break;
            case GloabalStats.Outcomes.Bad:
                Destroy(transform.parent.gameObject);
                break;
            case GloabalStats.Outcomes.Other:
                transform.parent.position = GameObject.Find("Grandma Bad").transform.position;
                GameObject stroller = GameObject.Find("Stroller");
                stroller.transform.GetChild(0).gameObject.SetActive(true);
                break;
            default:
                break;
        }   
    }

    void OldManJenkinsUpdate(){
        GameObject[] kids = GameObject.FindGameObjectsWithTag("Enemy");
        switch(GloabalStats.Instance.oldManOutcomes){
            case GloabalStats.Outcomes.Good:
                transform.parent.gameObject.SetActive(false);
                foreach(GameObject kid in kids){
                    kid.SetActive(false);
                }
                break;
            case GloabalStats.Outcomes.Bad:
                transform.parent.gameObject.SetActive(false);
                foreach(GameObject kid in kids){
                    kid.SetActive(false);
                }
                break;
            case GloabalStats.Outcomes.Other:
                break;
            default:
                break;
        }
    }

    void WallUpdate(){
        GameObject wall = GameObject.Find("Graffiti Wall");
        switch(GloabalStats.Instance.wallOutcomes){
            case GloabalStats.Outcomes.Good:
                Renderer wallRender = wall.GetComponent<Renderer>();
                wallRender.material = material;
                break;
            case GloabalStats.Outcomes.Bad:
                break;
            default:
                break;
        }
    }
}
