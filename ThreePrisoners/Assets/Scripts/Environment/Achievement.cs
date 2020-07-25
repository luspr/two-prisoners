using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{

    [SerializeField]
    private GameObject OgameStateManager;
    [SerializeField]
    private int achievementID;

    void OnTriggerEnter(Collider col)
    {
        var info = col.gameObject.GetComponent<AgentInformation>();
        if (info != null)     //only if agent with available info
        {
            OgameStateManager.GetComponent<GameStateManager>().RegisterAchievement(achievementID, info.AgentID);
        }
    }
}
