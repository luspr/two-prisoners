using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopExitTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject uishop;

    public void OnTriggerExit()
    {
        uishop.GetComponent<UIShop>().ExitShop();
    }
}
