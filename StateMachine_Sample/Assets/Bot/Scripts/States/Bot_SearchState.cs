using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In this state the bot follows the target until reaching a certain distance to it, then
/// it goes into 'Found' State
/// </summary>

public class Bot_SearchState : IState
{
    Bot_StateMachine BotSMRef;
    Rigidbody BotRigidBody;

    Material BotAntennaMat;
    Color StartingAntennaColor;
    Color SearchingAntennaColor = Color.red;

    // if we need specific components we should send them through the constructor
    public Bot_SearchState(Bot_StateMachine searchBotSM, Material AntennaMat, Rigidbody BotRB)
    {
        BotSMRef = searchBotSM;
        BotAntennaMat = AntennaMat;
        BotRigidBody = BotRB;
    }

    public void Enter()
    {
        Debug.Log("STATE CHANGE - Search");
        // Search State visuals
        StartingAntennaColor = BotAntennaMat.color;
        BotAntennaMat.color = SearchingAntennaColor;
    }

    public void Exit()
    {
        // undo Search State visuals
        BotAntennaMat.color = StartingAntennaColor;
    }

    public void LogicUpdate()
    {
        //
    }

    public void PhysicsUpdate()
    {
        // if we're close enough to the target, enter Found State
        // otherwise, keep moving
        float distanceFromTarget = Vector3.Distance(BotSMRef.TargetPosition, BotRigidBody.position);
        if (distanceFromTarget <= 0.2f)
        {
            BotSMRef.ChangeState(BotSMRef.FoundState);
        }
        else
        {
            RotateTowardsTarget();
            MoveTowardsTarget();
        }
    }

    // smoothly rotates the bot to make it face the target
    void RotateTowardsTarget()
    {
        Quaternion lookRotation = Quaternion.LookRotation(BotSMRef.TargetPosition - BotRigidBody.position);
        lookRotation = Quaternion.Slerp(BotRigidBody.rotation, lookRotation, BotSMRef.RotateSpeed * Time.deltaTime);
        BotRigidBody.MoveRotation(lookRotation);
    }

    // translates the bot's rigidbody towards the target
    void MoveTowardsTarget()
    {
        Vector3 moveOffset = BotSMRef.transform.forward * BotSMRef.MoveSpeed;
        BotRigidBody.MovePosition(BotRigidBody.position + moveOffset);
    }
}
