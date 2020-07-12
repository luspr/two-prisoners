using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    private int damageInflicted;
    private bool isStanding = true;            //is the character standing?
    private bool edgeMemIsStanding = true;     //edge memory
    private float originHeight;
    private float destinationHeight;
    private float abyssTimer;
    private CharacterController characterController;
    private Health health;

    [SerializeField]
    private float falldownFactor;   //severity of damage
    [SerializeField]
    private float minimumHeight;      //below this, there is no fall down damage


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        isStanding = characterController.isGrounded;

        if (!isStanding)       //character is falling
        {
            abyssTimer += Time.deltaTime;
            if (edgeMemIsStanding)  //character just started falling
            {
                originHeight = gameObject.transform.position.y; //take starting height
            }
        }

        if (isStanding && !edgeMemIsStanding)       //character hit the floor
        {
            abyssTimer = 0;     //reset
            destinationHeight = gameObject.transform.position.y;
            FallDownDamage(originHeight - destinationHeight);
        }

        if (abyssTimer > 4)     //death from falling for too long
        {
            abyssTimer = 0;
            FallDownDamage(9999);
        }

        edgeMemIsStanding = isStanding;     //edge memory
    }

    public void FallDownDamage(float height)
    {
        if (height > minimumHeight)
        {
            damageInflicted = (int)Mathf.Floor(falldownFactor * Mathf.Pow(height, 2));
            health.TakeDamage(damageInflicted);
        }
    }

}
