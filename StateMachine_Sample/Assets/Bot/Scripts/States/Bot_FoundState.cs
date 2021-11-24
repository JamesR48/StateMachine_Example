using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In this state the bot waits for some time, then transitions back to 'Idle'
/// </summary>

public class Bot_FoundState : IState
{
    Bot_StateMachine BotSMRef;

    Material BotAntennaMat;
    Color StartingAntennaColor;
    Color FoundAntennaColor = Color.green;

    // how long to wait in 'Found' state
    float FoundDelayDuration = 0.8f;
    float ElapsedTime = 0;
    bool bTimerActive = false;

    public Bot_FoundState(Bot_StateMachine BotSM, Material AntennaMat)
    {
        BotSMRef = BotSM;
        BotAntennaMat = AntennaMat;
    }

    public void Enter()
    {
        Debug.Log("STATE CHANGE - Found");

        // store starting antenna color so we can get it back
        StartingAntennaColor = BotAntennaMat.color;
        // show 'Found' visual
        BotAntennaMat.color = FoundAntennaColor;
        // start delay to wait some time before going back to idle
        StartTimer();
    }

    public void Exit()
    {
        // stop 'Found' visual
        BotAntennaMat.color = StartingAntennaColor;
    }

    public void PhysicsUpdate()
    {
        //
    }

    public void LogicUpdate()
    {
        // if timer is active, increment time
        if (bTimerActive)
        {
            ElapsedTime += Time.deltaTime;
        }

        // if the elapsed time has met the required duration, then go back to 'Idle'
        if (ElapsedTime > FoundDelayDuration)
        {
            StopTimer();
            BotSMRef.ChangeState(BotSMRef.IdleState);
        }
    }

    void StartTimer()
    {
        bTimerActive = true;
        ElapsedTime = 0;
    }

    void StopTimer()
    {
        bTimerActive = false;
    }
}

