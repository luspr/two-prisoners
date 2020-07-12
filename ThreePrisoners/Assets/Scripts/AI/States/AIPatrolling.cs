using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIPatrolling : AIState
{

    private int currentTargetIdx = 0;
    private Vector3 currentTarget;
    private NavMeshAgent agent;
    private Bot bot;
    private GameObject gameObject;
    private Transform transform;

    public AIPatrolling(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
        bot = gameObject.GetComponent<Bot>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void OnEnter()
    {
        currentTargetIdx = 0;
        currentTarget = bot.NavTargets[0].position;
        agent.SetDestination(currentTarget);
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
        if (Vector3.Distance(transform.position, agent.pathEndPosition) < bot.TargetDistance)
        {
            currentTargetIdx = (currentTargetIdx + 1) % bot.NavTargets.Length;
            agent.SetDestination(bot.NavTargets[currentTargetIdx].position);
        }
    }
}
