﻿using UnityEngine;
using System.Collections;

public class PatrolState : IEnemyState
{
    private readonly AIStatePattern myObject;
    private int nextWayPoint = 0;

    public PatrolState(AIStatePattern statePattern)
    {
        myObject = statePattern;
    }

    public void StartState()
    {
        myObject.meshRendererFlag.material.color = Color.green;
    }

    public void UpdateState()
    {
        Look();
        Patrol();
    }

    public void ToPatrolState()
    {
        Debug.Log("Can't transition to same state");
    }

    public void ToChaseState()
    {
        myObject.currentState = myObject.chaseState;
        myObject.currentState.StartState();
    }

    public void ToWanderState()
    {
        myObject.currentState = myObject.wanderState;
        myObject.currentState.StartState();
    }

    public void ToIdleState()
    {
        myObject.currentState = myObject.idleState;
        myObject.currentState.StartState();
    }

    public void ToAttackState()
    {
        myObject.currentState = myObject.attackState;
        myObject.currentState.StartState();
    }

    public void ToFleeState()
    {
        myObject.currentState = myObject.fleeState;
        myObject.currentState.StartState();
    }

    private void Look()
    {
        if (myObject._lookArea.GetTargetsInView().Count != 0)
        {
            myObject.currentTarget = myObject._lookArea.GetTargetsInView()[0].transform;
            if (myObject.canChase)
                ToChaseState();
        }
    }

    void Patrol()
    {
        myObject.navMeshAgent.destination = myObject.wayPoints[nextWayPoint].position;
        myObject.navMeshAgent.Resume();

        if (myObject.navMeshAgent.remainingDistance <= myObject.navMeshAgent.stoppingDistance && !myObject.navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % myObject.wayPoints.Length;
            ToIdleState();
        }
    }
}