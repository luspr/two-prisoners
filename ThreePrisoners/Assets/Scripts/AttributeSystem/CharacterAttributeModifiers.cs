using UnityEngine;

class GenericAdditiveModifier : IAdditiveAttributeModifier
{
    private AttributeEnum attributeType;
    float value;

    public GenericAdditiveModifier(
        AttributeEnum attributeType,
        float value)
    {
        this.attributeType = attributeType;
        this.value = value;
    }

    public void ApplyToAttribute(PlayerAttributeManager playerAttributeManager)
    {
        CharacterAttribute attribute = playerAttributeManager.GetAttribute(attributeType);
        Debug.Log("apply modifier to " + attribute.ToString());
        attribute.RegisterModifier(this);
    }

    public float GetValue()
    {
        return value;
    }

    public void RemoveFromAttribute(PlayerAttributeManager playerAttributeManager)
    {
        CharacterAttribute attribute = playerAttributeManager.GetAttribute(attributeType);
        attribute.RemoveModifier(this);
    }
}

class GenericMultiplicativeModifier : IMultiplicativeAttributeModifier
{
    private AttributeEnum attributeType;
    float value;

    public GenericMultiplicativeModifier(
        AttributeEnum attributeType,
        float value)
    {
        this.attributeType = attributeType;
        this.value = value;
    }

    public void ApplyToAttribute(PlayerAttributeManager playerAttributeManager)
    {
        CharacterAttribute attribute = playerAttributeManager.GetAttribute(attributeType);
        attribute.RegisterModifier(this);
    }

    public float GetValue()
    {
        return value;
    }

    public void RemoveFromAttribute(PlayerAttributeManager playerAttributeManager)
    {
        CharacterAttribute attribute = playerAttributeManager.GetAttribute(attributeType);
        attribute.RemoveModifier(this);
    }
}