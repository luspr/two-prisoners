using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct TransitionStatePair
{
    // State transition with the associated successor state. 
    public AIStateTransition transition;
    public AIState state;
}

public class StateMachine : MonoBehaviour
{

    private Dictionary<AIState, List<TransitionStatePair>> transitionMap;
    private AIState currentState;


    public void Initialize(
        Dictionary<AIState, List<TransitionStatePair>> dict,
        AIState initialState)
    {
        transitionMap = dict;
        currentState = initialState;
    }

    private void Start()
    {
        currentState.OnEnter();
    }

    void Update()
    {
        var transitions = transitionMap[currentState];
        foreach (TransitionStatePair pair in transitions)
        {
            if (pair.transition.CheckCondition())
            {
                currentState.OnExit();
                currentState = pair.state;

                Debug.Log("switched to " + currentState);
                currentState.OnEnter();
            }
        }
        currentState.OnUpdate();
    }

}