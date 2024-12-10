using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifetimeDeathScript : EnemyLifetimeState
{
    public override LifetimeStates Type => LifetimeStates.Death;

    public EnemyLifetimeDeathScript(EnemyLifetimeStateMachine stateMachine, EnemyAnimationHandler animHandler, EnemyBaseClass enemyBase) : base(stateMachine, animHandler, enemyBase)
    {
       
    }
    internal override void OnEnter()
    {
        base.OnEnter();
        DeathTrigger();

    }
    internal override void OnExit()
    {
        base.OnExit();
        SetSpawn(false);
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
