using UnityEngine;
using UnityEngine.AI;

public class AIRetreatToSecureLocation : AIState
{
    public Transform RetreatTransform
    {
        get; set;
    }

    private GameObject gameObject;
    private Transform transform;
    private NavMeshAgent agent;

    public AIRetreatToSecureLocation(GameObject gameObject, Transform retreat)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
        RetreatTransform = retreat;
    }

    public void OnEnter()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
    }

    public void OnExit()
    {
        if (agent != null)
        {
            agent.isStopped = true;
        }
    }

    public void OnUpdate()
    {
        if (agent != null)
        {
            agent.SetDestination(RetreatTransform.position);
        }
    }
}