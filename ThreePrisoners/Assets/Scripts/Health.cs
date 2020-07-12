using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    private int maxHealthPoints = 100;  // int or float?
    [SerializeField]
    private GameObject manager;     //game state manager

    private int currentHealthPoints;
    private RespawnManager rspwn;


    void Start()
    {
        currentHealthPoints = maxHealthPoints;
        rspwn = manager.GetComponent<RespawnManager>();
    }

    public void SetMaxHealthPoints(int maxHealthPoints)
    {
        this.maxHealthPoints = maxHealthPoints;
        this.currentHealthPoints = maxHealthPoints;
    }


    public int GetCurrentHealth()
    {
        return currentHealthPoints;
    }

    public int GetMaxHealth()
    {
        return maxHealthPoints;
    }

    public void SetHealth(int healthValue)
    {
        currentHealthPoints = healthValue;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealthPoints <= 0)
        {
            return;
        }
        currentHealthPoints -= damage;
        if (currentHealthPoints <= 0)
        {
            rspwn.NotifyDeath();          //notify observer
        }

    }
}
