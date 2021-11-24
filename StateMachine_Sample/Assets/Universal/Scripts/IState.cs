using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This interface defines the architecture and functionalities for every State that entities will have
/// </summary>

public interface IState
{
    // enter the state and do things you only need to do once when you first enter the state
    void Enter();

    //handles core logic every frame, simulating Update() method without a MonoBehaviour attached
    void LogicUpdate();

    //handles physics logic and calculations, simulating FixedUpdate() method without a MonoBehaviour attached
    void PhysicsUpdate();

    //similar to Enter, this is where you do any clean-ups you only need to do once before the state changes
    void Exit();
    
}
