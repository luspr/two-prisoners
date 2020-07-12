using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


public class PermanentAttributeModification : AttributeModification
{
    public PermanentAttributeModification(string name) : base(name)
    {

    }

    public override bool IsActive()
    {
        return true;
    }

    public override void Tick(float deltaTime)
    {

    }
}