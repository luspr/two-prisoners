using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;
    private bool shopPopped = false;

    private List<Transform> activeShopInstances = new List<Transform>();
    private GameObject player;


    public void Awake()
    {
        //initialize template
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopObjectTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    public void Start()
    {
        //CreateItemButton(ShopItem.GetSprite(ShopItem.ItemType.WWRifle), "Rifle", ShopItem.GetCost(ShopItem.ItemType.WWRifle),0);       
        // CreateItemButton(ShopItem.GetSprite(ShopItem.ItemType.RailGun), "Rail Gun", ShopItem.GetCost(ShopItem.ItemType.RailGun), 1);
        // CreateItemButton(ShopItem.GetSprite(ShopItem.ItemType.RocketLauncher), "Rocket Launcher", ShopItem.GetCost(ShopItem.ItemType.RocketLauncher), 2);
    }

    public void PopUpShop()
    {
        if (shopPopped == false)
        {
            CreateItemButton(ShopItem.GetSprite(ShopItem.ItemType.WWRifle), "Rifle", ShopItem.GetCost(ShopItem.ItemType.WWRifle), 0);
            CreateItemButton(ShopItem.GetSprite(ShopItem.ItemType.RailGun), "Rail Gun", ShopItem.GetCost(ShopItem.ItemType.RailGun), 1);
            CreateItemButton(ShopItem.GetSprite(ShopItem.ItemType.RocketLauncher), "Rocket Launcher", ShopItem.GetCost(ShopItem.ItemType.RocketLauncher), 2);
            shopPopped = true;
            //action handling
            Cursor.lockState = CursorLockMode.None;
            //player.GetComponent<WeaponInventory>().enabled = false;

        }
    }

    public void ExitShop()
    {
        foreach (Transform display in activeShopInstances)
        {
            Destroy(display.gameObject);
        }

        activeShopInstances.Clear();
        shopPopped = false;
        //action handling
        Cursor.lockState = CursorLockMode.Locked;
        //player.GetComponent<WeaponInventory>().enabled = true;


    }

    private void CreateItemButton(Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {

        //create instance of item display
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 20f;
        shopItemRectTransform.anchoredPosition = new Vector3(0, -shopItemHeight * positionIndex,0);

        shopItemTransform.Find("itemName").GetComponent<Text>().text = itemName;
        shopItemTransform.Find("priceTag").GetComponent<Text>().text = itemCost.ToString();
        shopItemTransform.Find("itemSprite").GetComponent<Image>().sprite = itemSprite;
        shopItemTransform.gameObject.SetActive(true);

        //add reference to list to later remove
        activeShopInstances.Add(shopItemTransform);

    }

   
}
