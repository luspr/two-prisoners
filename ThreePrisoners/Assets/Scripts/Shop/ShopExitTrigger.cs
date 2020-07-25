using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopExitTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject uishop;

    public void OnTriggerExit(Collider col)
    {
        uishop.GetComponent<UIShop>().ExitShop(col.gameObject);
    }
}
