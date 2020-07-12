using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject[] player;
    [SerializeField]
    private int[] respawnDuration;
    [SerializeField]
    private Vector3[] respawnLocation;
    [SerializeField]
    private bool[] isPlayer;

    private List<Health> healthScript = new List<Health>();

    void Start()
    {
        
        foreach (GameObject element in player)
        {
            healthScript.Add(element.GetComponent<Health>());       //store all health scripts in array
        }

        if ((respawnDuration.Length != player.Length) || (respawnLocation.Length != player.Length)||(player.Length != isPlayer.Length))
        {
            Debug.LogError("Respawn Manager: Invalid parameter sizes for serialized fields.");  //error handling
        }

    }

    public void NotifyDeath()
    {
        int count = 0;
        foreach (Health element in healthScript)
        {
            int currentHealth = element.GetCurrentHealth();      //determine dying object TODO: get this to work without having to look for specific object
            if (currentHealth <= 0)
            {
                StartCoroutine(Die(count));          //death, pointing to appropriate player
            }
            count++;
        }
    }

    private IEnumerator Die(int index)
    {
        GameObject playr = player[index];                       //retrieve appropriate GameObject from List
        WeaponInventory weaponScript;
        MonoBehaviour movementScript;
        MonoBehaviour falldownScript;

        if (isPlayer[index])
        {
            weaponScript = playr.GetComponent<WeaponInventory>();
            movementScript = playr.GetComponent<Movement>();
            falldownScript = playr.GetComponent<FallDown>();
        }
        else
        {
            weaponScript = playr.GetComponent<WeaponInventory>();
            movementScript = playr.GetComponent<Bot>();
            falldownScript = playr.GetComponent<AISimpleAttack>();
        }

        int lostWeapon = weaponScript.LoseWeapon();     //lose one weapon at random
        if (lostWeapon > -1)
        {
            GetComponent<WeaponDropManager>().CreateDrop(playr.transform, lostWeapon);        //create weapon drop close to player transform
        }

        //disable actions
        movementScript.enabled = false;
        falldownScript.enabled = false;
        weaponScript.SetDead(true);


        Animator animator = playr.GetComponentInChildren<Animator>();
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

        //Wait for respawn
        float respawnTime = respawnDuration[index];     //retrieve appropriate respawn duration
        yield return new WaitForSeconds(respawnTime);

        //respawn


        //reset position, animator, health
        animator.ResetTrigger("Die");
        animator.SetTrigger("Respawn");
        playr.transform.position = respawnLocation[index];

        //enable actions
        movementScript.enabled = true;
        falldownScript.enabled = true;
        weaponScript.InstantiateActiveWeapon();
        weaponScript.SetDead(false);



        healthScript[index].SetHealth(healthScript[index].GetMaxHealth());     //set health back to max health


        yield break;
    }

}
