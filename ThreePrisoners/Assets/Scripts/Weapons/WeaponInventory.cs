using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{

    [SerializeField]
    private WeaponEnum weaponEnum;
    [SerializeField]
    private int maxWeaponsEquipped = 4;

    private IWeapon[] weapons;
    private GameObject[] weaponGameObjects;

    private List<int> weaponsEquipped = new List<int>();
    private int weaponIndex = 0;
    private int activeWeaponID;

    private GameObject activeWeaponObject;
    private IWeapon shootScript;

    private float weaponTimer;              //this measures the time since the last shot
    private float distanceScrolled;
    private bool fired;
    private bool lostWeapon;
    private bool weaponDropsLocked;
    private bool isDead;        //is player dead / dying?

    private void Awake()
    {
        weaponGameObjects = weaponEnum.weaponArsenal;
        weapons = new IWeapon[weaponGameObjects.Length];
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = weaponGameObjects[i].GetComponent<IWeapon>();
        }
    }

    void Start()
    {

        weaponsEquipped.Add(0);          //add default weapon
        activeWeaponID = weaponsEquipped[0];
        activeWeaponObject = Instantiate(weaponGameObjects[activeWeaponID], transform.position, transform.rotation);
        activeWeaponObject.transform.SetParent(gameObject.transform);   //set player as parent
    }

    public bool GatherDrop(int weapID)
    {
        if (!isDead)
        {
            if (!weaponsEquipped.Contains(weapID) && (weaponsEquipped.Count < maxWeaponsEquipped) && !weaponDropsLocked)
            {
                weaponsEquipped.Add(weapID);
                return true;
            }
        }
        return false;
    }

    public int LoseWeapon()
    {
        lostWeapon = true;  //this bool makes sure that lost weapon gameObject is being switched away from

        if (weaponsEquipped.Count > 1)
        {
 

            int rnd = Random.Range(0, weaponsEquipped.Count);     //determine random number for weapon to be removed
            int rmvdID = weaponsEquipped[rnd];
            weaponsEquipped.RemoveAt(rnd);

            return rmvdID;
        }
        return -1;
    }


    void Update()
    {
        // Scroll for weapon switch
        distanceScrolled += Input.GetAxis("Mouse ScrollWheel");
        if ((Mathf.Abs(distanceScrolled) >= 1) || lostWeapon)       //update active weapon if either 1) player scrolled 2) weapon has been lost
        {

            DestroyActiveWeapon();                               //destroy old weapon game object

            weaponIndex = (weaponsEquipped.Count + weaponIndex + 1) % weaponsEquipped.Count;       //increment/decrement weapon index
            activeWeaponID = weaponsEquipped[weaponIndex];                    //get ID of new weapon

            if (!isDead)
            {
                InstantiateActiveWeapon();                  //reeinstantiate only if player is alive
            }


            distanceScrolled = 0;   //reset scrollwheel
            lostWeapon = false;     //reset weapon lost flag
        }
    }

    public GameObject[] GetWeaponArsenal()
    {
        return weaponGameObjects;
    }

    public IWeapon GetActiveWeapon()
    {
        return activeWeaponObject.GetComponent<IWeapon>();
    }

    public int GetActiveWeaponID()
    {
        return activeWeaponID;
    }

    public void DestroyActiveWeapon()
    {
        Destroy(activeWeaponObject);
    }

    public void InstantiateActiveWeapon()       //instantiates active weapon object
    {
        activeWeaponObject = Instantiate(weaponGameObjects[activeWeaponID], transform.position, transform.rotation); //instantiate new weapon
        activeWeaponObject.transform.SetParent(gameObject.transform);   //set player as parent
    }

    public void SetDead(bool dead)
    {
        isDead = dead;
    }

}
