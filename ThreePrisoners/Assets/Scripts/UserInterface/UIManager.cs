using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform PlayerTransform;

    private Slider healthBar;
    private Health playerHealth;

    private Slider sprintBar;
    private Movement playerMovement;

    private WeaponInventory wpnInventory;
    private List<Sprite> images = new List<Sprite>();
    private Image wpnSprite;

    void Start()
    {
        playerHealth = PlayerTransform.GetComponent<Health>();
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();

        playerMovement = PlayerTransform.GetComponent<Movement>();
        sprintBar = GameObject.Find("SprintBar").GetComponent<Slider>();

        wpnInventory = PlayerTransform.GetComponent<WeaponInventory>();
        wpnSprite = GameObject.Find("EquippedWeapon").GetComponent<Image>();

        //get sprites
        GameObject[] arsenal = wpnInventory.GetWeaponArsenal();
        foreach (GameObject weapon in arsenal)
        {
            Sprite weaponIcon = weapon.transform.Find("Sprite").GetComponent<SpriteMask>().sprite;
            images.Add(weaponIcon);
        }

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = 100 * playerHealth.GetCurrentHealth()/playerHealth.GetMaxHealth();
        sprintBar.value = 100 * playerMovement.GetStamina()/playerMovement.GetMaxStamina();

        int wpnID = wpnInventory.GetActiveWeaponID();
        wpnSprite.sprite = images[wpnID];

    }
}
