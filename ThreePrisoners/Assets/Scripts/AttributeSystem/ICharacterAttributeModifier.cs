/* 
Atomic attribute modification for individual attributes.
*/

public interface ICharacterAttributeModifier
{
    void ApplyToAttribute(PlayerAttributeManager playerAttributeManager);
    void RemoveFromAttribute(PlayerAttributeManager playerAttributeManager);
}

public interface IAdditiveAttributeModifier : ICharacterAttributeModifier
{
    float GetValue();
}

public interface IMultiplicativeAttributeModifier : ICharacterAttributeModifier
{
    float GetValue();

}
