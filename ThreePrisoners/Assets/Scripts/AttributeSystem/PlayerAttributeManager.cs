﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


public class PlayerAttributeManager : MonoBehaviour
{
    [SerializeField]
    private float initialSpeed;
    [SerializeField]
    private float initialJump;

    public SpeedAttribute Speed { get; private set; }
    public JumpAttribute Jump { get; private set; }


    private List<CharacterAttribute> attributes = new List<CharacterAttribute>();

    private List<AttributeModification> modifications;

    private void Awake()
    {
        Speed = new SpeedAttribute(initialSpeed);
        Jump = new JumpAttribute(initialJump);

        attributes = new List<CharacterAttribute>(){
            Speed,
            Jump
        };
        modifications = new List<AttributeModification>();
    }

    public CharacterAttribute GetAttribute(AttributeEnum attribute)
    {
        switch (attribute)
        {
            case AttributeEnum.Jump:
                return this.Jump;
            case AttributeEnum.Speed:
                return this.Speed;
            default:
                return null;
        }
    }

    private void LateUpdate()
    {
        modifications.ForEach(mod => mod.Tick(Time.deltaTime));
        modifications.RemoveAll(mod => mod.IsInactive());

        attributes.ForEach(attr => attr.Tick(Time.deltaTime));
    }

    public void RegisterModification(AttributeModification modification)
    {
        modification.Apply(this);
        modifications.Add(modification);
    }

}