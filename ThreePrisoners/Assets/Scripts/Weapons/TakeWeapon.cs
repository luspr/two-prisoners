using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWeapon : MonoBehaviour
{
    [SerializeField]
    private int weaponID;       //this has to correspond to the weaponArsenal IDs

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.GetComponent<WeaponInventory>() != null)     //only if inventory exists
        {
            bool gathered = col.transform.GetComponent<WeaponInventory>().GatherDrop(weaponID);
            if (gathered)
            {
                Destroy(this.transform.parent.gameObject);
            }
        }
    }
}
