using UnityEngine;


class PowerUpStation : MonoBehaviour, IInteractableObject
{
    public PowerUp powerUp;


    public void OnInteract(Transform interactor)
    {
        var attributeManager = interactor.GetComponent<PlayerAttributeManager>();
        if (attributeManager == null)
        {
            return;
        }
        var modification = AttributeModificationFactory.GetAttributeModification(powerUp);
        attributeManager.RegisterModification(modification);
    }
}