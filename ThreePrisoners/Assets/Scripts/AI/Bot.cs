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

        // Define Transitions.
        TransitionStatePair patrollingToRetreat;
        patrollingToRetreat.state = retreatState;
        patrollingToRetreat.transition = new EnemySpottedStateTransition(gameObject);

        TransitionStatePair patrollingToRetreatLowHealth;
        patrollingToRetreatLowHealth.state = retreatState;
        patrollingToRetreatLowHealth.transition = new LowHealthStateTransition(gameObject, 50);

        var patrollingStateSuccessors = new List<TransitionStatePair>()
        {
            patrollingToRetreat,
            patrollingToRetreatLowHealth
        };

        TransitionStatePair retreatNullTransitionPair;
        retreatNullTransitionPair.transition = new NullTransition();
        retreatNullTransitionPair.state = null;

        var retreatStateSuccessors = new List<TransitionStatePair>()
        {
            retreatNullTransitionPair
        };

        var stateMapping = new Dictionary<AIState, List<TransitionStatePair>>()
        {
            [patrollingState] = patrollingStateSuccessors,
            [retreatState] = retreatStateSuccessors
        };

        stateMachine.Initialize(stateMapping, patrollingState);
    }
}