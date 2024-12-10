using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifetimeInactiveScript : EnemyLifetimeState
{
    public override LifetimeStates Type => LifetimeStates.Inactive;

    public EnemyLifetimeInactiveScript(EnemyLifetimeStateMachine stateMachine, EnemyAnimationHandler animHandler, EnemyBaseClass enemyBase) : base(stateMachine, animHandler, enemyBase)
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
    }

    internal override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
