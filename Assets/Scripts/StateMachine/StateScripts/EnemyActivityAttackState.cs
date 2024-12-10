using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivityAttackState : EnemyActivityState
{
    public override ActivityStates Type => ActivityStates.Attacking;

    public EnemyActivityAttackState(EnemyActivityStateMachine stateMachine,EnemyAnimationHandler animHandler, EnemyBaseClass enemyBase) : base(stateMachine, animHandler, enemyBase)
    {
       
    }
    internal override void OnEnter()
    {
        base.OnEnter();
        SetupAttack();

    }
    internal override void OnExit()
    {
        base.OnExit();

    }
    internal override void Update()
    {
        
        base.Update();
        if (CheckForMovementStart())
            SetState(ActivityStates.Movement);
       


    }

    internal override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
