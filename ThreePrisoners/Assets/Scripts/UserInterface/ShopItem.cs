using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem
{
    static ShopItem instance = null;

    public enum ItemType
    {
        WWRifle,
        RailGun,
        RocketLauncher

    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default: return 0;
            case ItemType.WWRifle: return 50;
            case ItemType.RailGun: return 100;
            case ItemType.RocketLauncher: return 100;

        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default: return null;
            case ItemType.WWRifle: return ItemSprites.instance.GetSprite(0);
            case ItemType.RailGun: return ItemSprites.instance.GetSprite(1);
            case ItemType.RocketLauncher: return ItemSprites.instance.GetSprite(2);
        }
    }

}
