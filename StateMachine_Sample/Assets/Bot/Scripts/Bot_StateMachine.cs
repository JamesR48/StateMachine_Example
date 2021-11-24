using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bot's State Machine. All of its States are created and initialized here.
/// Any data or references that the States needs can be accessed through here as well
/// </summary>

public class Bot_StateMachine : StateMachine
{
    // Bot States
    public Bot_IdleState IdleState { get; private set; }
    public Bot_SearchState SearchState { get; private set; }
    public Bot_FoundState FoundState { get; private set; }

    [Header("Components")]
    [SerializeField] TargetAssigner TargetAssignerRef = null;
    [SerializeField] Rigidbody BotRigidBody = null;

    [Header("Movement")]
    public float RotateSpeed = 4;
    public float MoveSpeed = 4;

    [Header("Visuals")]
    [SerializeField] MeshRenderer BotAntennaMesh = null;

    // if we have data that multiple states need access to, we can keep it here for sharing
    public Vector3 TargetPosition { get; set; }

    void Awake()
    {
        // Send required data to each State through the constructor
        IdleState = new Bot_IdleState(this, BotAntennaMesh.material, TargetAssignerRef);
        SearchState = new Bot_SearchState(this, BotAntennaMesh.material, BotRigidBody);
        FoundState = new Bot_FoundState(this, BotAntennaMesh.material);
    }

    private void OnEnable()
    {
        TargetAssignerRef.NewTargetAcquired += OnNewTargetAcquired;
    }

    private void OnDisable()
    {
        TargetAssignerRef.NewTargetAcquired -= OnNewTargetAcquired;
    }

    private void Start()
    {
        Initialize(IdleState);
    }

    void OnNewTargetAcquired(Vector3 newTarget)
    {
        Debug.Log("Acquired new target: " + newTarget.ToString());
        TargetPosition = newTarget;
    }
}
