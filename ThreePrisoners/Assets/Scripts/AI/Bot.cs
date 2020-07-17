using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Bot : MonoBehaviour
{

    // [SerializeField]
    public Transform[] NavTargets;
    // [SerializeField]
    public Transform Retreat;
    // [SerializeField]
    public float TargetDistance = 0.3f;

    public Transform CurrentEnemy
    {
        get; set;
    }

    private StateMachine stateMachine;


    private void Awake()
    {
        // Init State machine with transitions and states
        stateMachine = gameObject.AddComponent<StateMachine>();
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        AIPatrolling patrollingState = new AIPatrolling(this.gameObject);

        AIRetreatToSecureLocation retreatState = new AIRetreatToSecureLocation(gameObject, Retreat);

        AIWaitForRespawn deathState = new AIWaitForRespawn(gameObject);

        // Define Transitions.
        TransitionStatePair patrollingToRetreat;
        patrollingToRetreat.state = retreatState;
        patrollingToRetreat.transition = new EnemySpottedStateTransition(gameObject);

        TransitionStatePair patrollingToRetreatLowHealth;
        patrollingToRetreatLowHealth.state = retreatState;
        patrollingToRetreatLowHealth.transition = new LowHealthStateTransition(gameObject, 50);

        TransitionStatePair patrollingToDeath;
        patrollingToDeath.state = deathState;
        patrollingToDeath.transition = new DeathStateTransition(gameObject);

        var patrollingStateSuccessors = new List<TransitionStatePair>()
        {
            patrollingToRetreat,
            patrollingToRetreatLowHealth,
            patrollingToDeath,
        };

        TransitionStatePair retreatToDeath;
        retreatToDeath.state = deathState;
        retreatToDeath.transition = new DeathStateTransition(gameObject);

        var retreatStateSuccessors = new List<TransitionStatePair>()
        {
            retreatToDeath
        };

        TransitionStatePair respawner;
        respawner.state = patrollingState;
        respawner.transition = new RespawnTransition(gameObject);

        var deathStateSuccessors = new List<TransitionStatePair>()
        {
            respawner
        };

        var stateMapping = new Dictionary<AIState, List<TransitionStatePair>>()
        {
            [patrollingState] = patrollingStateSuccessors,
            [retreatState] = retreatStateSuccessors,
            [deathState] = deathStateSuccessors
        };

        stateMachine.Initialize(stateMapping, patrollingState);
    }
}