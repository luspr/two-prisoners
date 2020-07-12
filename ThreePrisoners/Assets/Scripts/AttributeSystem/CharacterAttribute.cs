using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Base class for all attributes.
public abstract class CharacterAttribute
{
    public float Value
    {
        get { return _currentValue; }
    }

    protected List<IAdditiveAttributeModifier> _additiveModifiers;
    protected List<IMultiplicativeAttributeModifier> _multiplicativeModifiers;
    protected float _initialValue;
    protected float _currentValue;

    public CharacterAttribute(float initialValue)
    {
        this._initialValue = initialValue;
        this._currentValue = initialValue;
        _additiveModifiers = new List<IAdditiveAttributeModifier>();
        _multiplicativeModifiers = new List<IMultiplicativeAttributeModifier>();
    }

    public void Tick(float deltaTime)
    {
        // Update modifiers and Remove all modifiers that are "expired".
        // _additiveModifiers.ForEach(mod => mod.Tick(deltaTime));
        // _multiplicativeModifiers.ForEach(mod => mod.Tick(deltaTime));
        // _additiveModifiers.RemoveAll(mod => !mod.IsActive());
        // _multiplicativeModifiers.RemoveAll(mod => !mod.IsActive());
        ComputeCurrentValue();
    }

    float ComputeCurrentValue()
    {
        // NOTE: I don't know performance is worse for this fancy functional style syntax.
        // Maybe revert back to for loop for optimization. 
        // Debug.Log("Compute current value, add " + _additiveModifiers.Count + " mult " + _multiplicativeModifiers.Count);
        float additiveModification = 0f;
        for (int i = 0; i < _additiveModifiers.Count; i++)
        {
            additiveModification += _additiveModifiers[i].GetValue();
        }
        float multiplicativeModification = 1f;
        for (int i = 0; i < _multiplicativeModifiers.Count; i++)
        {
            multiplicativeModification += _multiplicativeModifiers[i].GetValue();
        }
        _currentValue = (_initialValue + additiveModification) * multiplicativeModification;
        return _currentValue;
    }

    public void RegisterModifier(IAdditiveAttributeModifier modifier)
    {
        _additiveModifiers.Add(modifier);
    }

    public void RemoveModifier(IAdditiveAttributeModifier modifier)
    {
        _additiveModifiers.Remove(modifier);
    }

    public void RegisterModifier(IMultiplicativeAttributeModifier modifier)
    {
        _multiplicativeModifiers.Add(modifier);
    }

    public void RemoveModifier(IMultiplicativeAttributeModifier modifier)
    {
        _multiplicativeModifiers.Remove(modifier);
    }
}