using UnityEngine;

public class LowHealthStateTransition : AIStateTransition
{
    private Health health;
    private int lowHealthThreshold;

    public LowHealthStateTransition(GameObject gameObject, int lowHealthThreshold)
    {
        this.lowHealthThreshold = lowHealthThreshold;
        health = gameObject.GetComponent<Health>();
    }

    public bool CheckCondition()
    {
        return health.GetCurrentHealth() < lowHealthThreshold;
    }

}