using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyAIActivityProjectileShieldState : FriendlyAIActivityState
{
    public override FriendlyAIDirective Type => FriendlyAIDirective.ProjectileShield;

    public FriendlyAIActivityProjectileShieldState(FriendlyAIActivityStateMachine stateMachine, FriendlyControllerScript friendlyController) : base(stateMachine, friendlyController)
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
