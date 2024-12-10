using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivityMovementState : EnemyActivityState
{
    public override ActivityStates Type => ActivityStates.Movement;

    public EnemyActivityMovementState(EnemyActivityStateMachine stateMachine,EnemyAnimationHandler animHandler, EnemyBaseClass enemyBase) : base(stateMachine, animHandler, enemyBase)
    {
       
    }
    internal override void OnEnter()
    {
        base.OnEnter();

    }
    internal override void OnExit()
    {
        base.OnExit();
        

    }
    internal override void Update()
    {
        
        base.Update();
        CallMovement();


    }

    internal override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
