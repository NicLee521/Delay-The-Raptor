using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deed", menuName = "ScriptableObjects/DeedObject", order = 1)]
public class DeedObject : ScriptableObject
{
    public string npcName;
    public string miniGameSceneName;
    public string[] dialog;
    public bool isEnabled = true;

    public void Disable(){
        isEnabled = false;
        GloabalStats.Instance.deeds[npcName] = false;
    }

}
