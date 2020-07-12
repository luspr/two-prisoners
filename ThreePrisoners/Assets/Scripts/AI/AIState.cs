using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AIState
{

    void OnEnter();

    void OnExit();

    void OnUpdate();

}