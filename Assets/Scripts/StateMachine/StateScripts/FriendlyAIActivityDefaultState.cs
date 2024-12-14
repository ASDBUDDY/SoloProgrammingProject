using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyAIActivityDefaultState : FriendlyAIActivityState
{
    public override FriendlyAIDirective Type => FriendlyAIDirective.Default;

    public FriendlyAIActivityDefaultState(FriendlyAIActivityStateMachine stateMachine, FriendlyControllerScript friendlyController) : base(stateMachine, friendlyController)
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
        CallDefaultMovement();


    }

    internal override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
