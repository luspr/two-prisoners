using UnityEngine;

using System.Collections.Generic;
/*
Attribute modification for implementing buffs / debuffs.
*/

public abstract class AttributeModification
{

    public string Name;
    protected List<ICharacterAttributeModifier> modifiers;
    protected PlayerAttributeManager attachedPlayerAttributeManager;

    public AttributeModification(string name)
    {
        this.Name = name;
    }

    public abstract bool IsActive();

    public bool IsInactive()
    {
        return !IsActive();
    }

    public string GetName()
    {
        return Name;
    }

    public abstract void Tick(float deltaTime);

    public void Apply(PlayerAttributeManager playerAttributeManager)
    {
        attachedPlayerAttributeManager = playerAttributeManager;
        foreach (var modifier in modifiers)
        {
            modifier.ApplyToAttribute(playerAttributeManager);
        }
    }

    public void RemoveModificationFromCharacter()
    {
        foreach (var modifier in modifiers)
        {
            modifier.RemoveFromAttribute(attachedPlayerAttributeManager);
        }
    }

    public void SetModifierList(List<ICharacterAttributeModifier> modifiers)
    {
        this.modifiers = modifiers;
    }
}