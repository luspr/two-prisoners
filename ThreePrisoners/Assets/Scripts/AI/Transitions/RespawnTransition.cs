using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTransition : AIStateTransition
{
    private Health health;

    public RespawnTransition(GameObject gameObject)
    {
        health = gameObject.GetComponent<Health>();
    }

    public bool CheckCondition()
    {
        return health.GetCurrentHealth() > 1;       //TODO: use proper, unique transition from respawn manager
    }

}
