using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifetimeActiveScript : EnemyLifetimeState
{
    public override LifetimeStates Type => LifetimeStates.Active;
    public EnemyLifetimeActiveScript(EnemyLifetimeStateMachine stateMachine, EnemyAnimationHandler animHandler, EnemyBaseClass enemyBase) : base(stateMachine, animHandler, enemyBase)
    {
       
    }
    internal override void OnEnter()
    {
        base.OnEnter();
 
       SetActivityState(ActivityStates.Movement);

    }
    internal override void OnExit()
    {
        base.OnExit();
        //SetActivityState(ActivityStates.Default);

    }
    internal override void Update()
    {
        base.Update();
        CallActivityStateUpdate();
    }

    internal override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
