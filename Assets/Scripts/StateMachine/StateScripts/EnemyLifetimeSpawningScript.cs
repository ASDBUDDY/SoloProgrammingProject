using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifetimeSpawningScript : EnemyLifetimeState
{
    public override LifetimeStates Type => LifetimeStates.Spawning;

    public EnemyLifetimeSpawningScript(EnemyLifetimeStateMachine stateMachine,EnemyAnimationHandler animHandler, EnemyBaseClass enemyBase) : base(stateMachine, animHandler, enemyBase)
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
        if (CheckForSpawn())
            SetState(LifetimeStates.Active);


    }

    internal override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
