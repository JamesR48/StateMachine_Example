using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This State Machine class is going to be abstract since the idea is to extend it and allow child classes to implement and override functionality,
/// not creating objects of this specific type. Also, this inherits from MonoBehaviour since the State interface needs a way to call its Update and
/// FixedUpdate custom methods
/// </summary>

public abstract class StateMachine : MonoBehaviour
{
    public IState CurrentState { get; private set; }

    // initializes the state machine by setting the active state for the first time
    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    //handles transitions between States
    public void ChangeState(IState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();
    }

    // pass down Update ticks to States, since they don't have a MonoBehaviour
    public void Update()
    {
        // simulate update ticks in states
        if (CurrentState != null)
	    {
            CurrentState.LogicUpdate();
	    }
    }

    public void FixedUpdate()
    {
        // simulate fixedUpdate ticks in states
        if (CurrentState != null)
	    {
            CurrentState.PhysicsUpdate();
	    }
    }
}
