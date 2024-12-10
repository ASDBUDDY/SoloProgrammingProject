using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifetimePauseScript : EnemyLifetimeState
{
    public override LifetimeStates Type => LifetimeStates.Pause;
    public EnemyLifetimePauseScript(EnemyLifetimeStateMachine stateMachine, EnemyAnimationHandler animHandler, EnemyBaseClass enemyBase) : base(stateMachine, animHandler, enemyBase)
    {
       
    }
    internal override void OnEnter()
    {
        base.OnEnter();
        ToggleAnimator(false);
      

    }
    internal override void OnExit()
    {
        base.OnExit();
        ToggleAnimator(true);

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
