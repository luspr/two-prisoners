using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDropManager : MonoBehaviour
{
    [SerializeField]
    private WeaponEnum weaponDrops;

    public void CreateDrop(Transform dropPos, int dropID)
    {
        dropPos.Translate(new Vector3(0, 1, 0));
        Instantiate(weaponDrops.weaponArsenal[dropID], dropPos.position, dropPos.rotation);
    }

    public void CreateShopDrop(int dropID)
    {
        Debug.Log("Button Clicked");
        Transform dropPos = this.transform;
        dropPos.Translate(new Vector3(0, 1, 0));
        Instantiate(weaponDrops.weaponArsenal[dropID], dropPos.position, dropPos.rotation);
    }
}
