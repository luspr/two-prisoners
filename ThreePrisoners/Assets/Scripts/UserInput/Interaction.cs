using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    [SerializeField]
    private float interactionRange;

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
            // if (Physics.Raycast(transform.position + transform.up * 1.2f, Camera.main.transform.forward * interactionRange, out hit, 2f))
            if (Physics.Raycast(ray, out hit, 50f))
            {
                Debug.Log(hit.transform.name);
                Debug.Log("distance"  + Vector3.Distance(transform.position, hit.point));
                if (Vector3.Distance(transform.position, hit.transform.position) <= interactionRange) {
                    IInteractableObject interactable = hit.transform.GetComponent<IInteractableObject>();
                    if (interactable != null)
                    {
                        interactable.OnInteract(transform);
                    }
                }
            }
        }
    }

}
