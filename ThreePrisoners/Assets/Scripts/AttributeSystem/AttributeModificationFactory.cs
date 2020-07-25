using System.Collections.Generic;

class AttributeModificationFactory
{
    public static AttributeModification GetAttributeModification(PowerUp powerUp)
    {
        AttributeModification attributeModification;
        if (powerUp.temporary)
        {
            attributeModification = new TemporaryAttributeModification(
                powerUp.powerUpName,
                powerUp.duration
            );
        }
        else
        {
            attributeModification = new PermanentAttributeModification(powerUp.powerUpName);
        }

        List<ICharacterAttributeModifier> modifiers = new List<ICharacterAttributeModifier>();

        foreach (AttributeModifierValue amv in powerUp.additiveModifiers)
        {
            modifiers.Add(new GenericAdditiveModifier(
                amv.attributeType,
                amv.value
            ));
        }
        foreach (AttributeModifierValue amv in powerUp.multiplicativeModifiers)
        {
            modifiers.Add(new GenericMultiplicativeModifier(
                amv.attributeType,
                amv.value
            ));
        }
        attributeModification.SetModifierList(modifiers);
        attributeModification.isVolatile = powerUp.isVolatile;

        return attributeModification;
    }
}