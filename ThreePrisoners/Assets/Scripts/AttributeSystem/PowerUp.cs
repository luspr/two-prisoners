using UnityEngine;

[System.Serializable]
public struct AttributeModifierValue
{
    public AttributeEnum attributeType;
    public float value;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PowerUp", order = 1)]
public class PowerUp : ScriptableObject
{
    public AttributeModifierValue[] additiveModifiers;
    public AttributeModifierValue[] multiplicativeModifiers;
    public bool temporary;
    public float duration;
    public string powerUpName;
}