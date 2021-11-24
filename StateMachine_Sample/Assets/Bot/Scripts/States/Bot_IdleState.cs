using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In this state the bot is waiting to receive a new target. Once it gets the notification of a new target
/// it enters the 'Searching' state
/// </summary>

public class Bot_IdleState : IState
{
    Bot_StateMachine BotSMRef;
    TargetAssigner TargetAssignerRef;

    Material BotAntennaMat;
    Color StartingAntennaColor;
    Color IdleAntennaColor = Color.white;

    public Bot_IdleState(Bot_StateMachine BotSM, Material AntennaMat, TargetAssigner targetAssigner)
    {
        BotSMRef = BotSM;
        BotAntennaMat = AntennaMat;
        TargetAssignerRef = targetAssigner;
    }

    // we can start listening for events in Enter, and stop listening in Exit
    public void Enter()
    {
        Debug.Log("STATE CHANGE - Idle");
        // save starting antenna color so we can set it back
        StartingAntennaColor = BotAntennaMat.color;
        // show state visual
        BotAntennaMat.color = IdleAntennaColor;

        // start listening for new target mouse click
        TargetAssignerRef.NewTargetAcquired += OnNewTargetAcquired;
    }

    public void Exit()
    {
        // set back antenna color
        BotAntennaMat.color = StartingAntennaColor;
        // stop listening for new target mouse click
        TargetAssignerRef.NewTargetAcquired -= OnNewTargetAcquired;
    }

    public void PhysicsUpdate()
    {
        //
    }

    public void LogicUpdate()
    {
        //
    }

    void OnNewTargetAcquired(Vector3 newPosition)
    {
        BotSMRef.ChangeState(BotSMRef.SearchState);
    }
}
