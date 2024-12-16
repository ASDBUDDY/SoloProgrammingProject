using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyAIActivityAttackRangedState : FriendlyAIActivityState
{
    public override FriendlyAIDirective Type => FriendlyAIDirective.AttackRanged;

    public FriendlyAIActivityAttackRangedState(FriendlyAIActivityStateMachine stateMachine, FriendlyControllerScript friendlyController) : base(stateMachine, friendlyController)
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
        CastProjectile();


    }

    internal override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
