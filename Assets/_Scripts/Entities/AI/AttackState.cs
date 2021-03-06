﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackState : IEnemyState
{

    private readonly AIStatePattern myObject;

    public AttackState(AIStatePattern statePatternEnemy)
    {
        myObject = statePatternEnemy;
    }

    public void StartState()
    {
        myObject.meshRendererFlag.material.color = Color.red;
    }

    public void UpdateState()
    {
        Attack();
    }

    public void ToPatrolState()
    {
        myObject.currentState = myObject.patrolState;
        myObject.currentState.StartState();
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
        Debug.Log("Can't transition to same state");
    }
    
    public void ToFleeState()
    {
        myObject.currentState = myObject.fleeState;
        myObject.currentState.StartState();
    }


    private void Attack()
    {
        if (myObject._attackArea.GetTargetsInView().Count != 0 && myObject.currentTarget != null)
        {
            //Debug.Log("Attack!");
            myObject.transform.LookAt(myObject.currentTarget);
            myObject._enemyAttack.StartAttackAnimation();
        }
        else
        {
            ToChaseState();
        }
    }
}