using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSprites : MonoBehaviour
{
    private static ItemSprites _instance;

    public static ItemSprites instance
    {
        get
        {
            if (_instance == null) _instance = Instantiate(Resources.Load("ItemSprites") as GameObject).GetComponent<ItemSprites>();
            return _instance;
        }
    }


    public Sprite[] itemSprites;

    public Sprite GetSprite(int index)
    {
        return itemSprites[index];
    }
}
