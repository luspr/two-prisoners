using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UserInput;

public class RespawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject[] players;

    [SerializeField]
    private float RespawnCooldownTime;

    private List<Health> healthScript = new List<Health>();

    void Start()
    {
        
        foreach (GameObject element in players)
        {
            healthScript.Add(element.GetComponent<Health>());       //store all health scripts in array
        }

    }

    public void NotifyDeath(GameObject dyingAgent)
    {
           
        StartCoroutine(Die(dyingAgent));          //death, pointing to appropriate player
     
      
    }

    private void setComponents(GameObject agent, bool enabled)
    {
        AgentInformation agentInfo = agent.GetComponent<AgentInformation>();
        if (agentInfo.IsPlayer)
        {
            agent.GetComponent<Attack>().enabled = enabled;
            agent.GetComponent<Movement>().enabled = enabled;
            agent.GetComponent<FallDown>().enabled = enabled;
        }
        else
        {
            // Disable NavMesh, AISimpleAttack, stateMachine
            agent.GetComponent<AISimpleAttack>().enabled = enabled;
            //agent.GetComponent<NavMeshAgent>().enabled = enabled;
        }
        //agent.GetComponent<WeaponInventory>().enabled = enabled;      //weaponInventory needs to be enable so to handle the drops and the deselection of the lost wpn
    }

    private IEnumerator Die(GameObject dyingAgent)
    {
        AgentInformation agentInfo = dyingAgent.GetComponent<AgentInformation>();

        WeaponInventory weaponScript = dyingAgent.GetComponent<WeaponInventory>();
        int lostWeapon = weaponScript.LoseWeapon();     //lose one weapon at random
        if (lostWeapon > -1)
        {
            GetComponent<WeaponDropManager>().CreateDrop(dyingAgent.transform, lostWeapon);        //create weapon drop close to player transform
        }

        weaponScript.SetDead(true);
        setComponents(dyingAgent, false);

        Animator animator = dyingAgent.GetComponentInChildren<Animator>();
        int dieHash = Animator.StringToHash("DeathFromFront");

        // Play
        animator.ResetTrigger("Respawn");
        animator.SetTrigger("Die");
        // Wait until animation starts
        while (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != dieHash)
        {
            yield return null;
        }

        //Now wait until the current state is done playing
        float waitTime = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(waitTime);

        yield return new WaitForSeconds(agentInfo.TimeToRespawn);


        //respawn

        //reset position, animator, health
        animator.ResetTrigger("Die");
        animator.SetTrigger("Respawn");
        dyingAgent.transform.position = agentInfo.SpawnLocation;

        yield return new WaitForSeconds(RespawnCooldownTime);
        setComponents(dyingAgent, true);

        weaponScript.InstantiateActiveWeapon();
        weaponScript.SetDead(false);

        dyingAgent.GetComponent<Health>().ResetHealth();

        yield break;
    }

}
