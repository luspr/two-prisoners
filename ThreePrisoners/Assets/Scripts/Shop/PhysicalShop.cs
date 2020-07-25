using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalShop : MonoBehaviour, IInteractableObject
{
    [SerializeField]
    private GameObject uiCanvas;

    public void OnInteract(Transform interactor)
    {
        uiCanvas.GetComponent<UIShop>().PopUpShop(interactor.gameObject);

    }
}
