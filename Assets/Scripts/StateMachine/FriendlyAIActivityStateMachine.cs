using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyAIActivityStateMachine : StateMachine<FriendlyAIActivityState, FriendlyAIDirective> //State Machine For All Enemy actions while the Enemy is Active
{
    /// <summary>
    /// Animation Handler for Enemy Animator
    /// </summary>
    [SerializeField] FriendlyControllerScript FriendlyClass;
    private void Awake()
    {
        // Sets up Essential components

        FriendlyClass = this.GetComponentInParent<FriendlyControllerScript>();

        //Array of States to be initialized for StateMachine to be set to
        states = new FriendlyAIActivityState[7];
        states[0] = new FriendlyAIActivityDefaultState(this,FriendlyClass);
        states[1] = new FriendlyAIActivityAttackMeleeState(this, FriendlyClass);
        states[2] = new FriendlyAIActivityAttackRangedState(this, FriendlyClass);
        states[3] = new FriendlyAIActivityHealingState(this, FriendlyClass);
        states[4] = new FriendlyAIActivityShieldState(this, FriendlyClass);
        states[5] = new FriendlyAIActivityProjectileShieldState(this, FriendlyClass);
        states[6] = new FriendlyAIActivityDecoyState(this, FriendlyClass);



        //Set to Default State Post Initialisation
        SetState(FriendlyAIDirective.Default);
    }

    /// <summary>
    /// Calling Update of State if it Exists
    /// </summary>
    public void CallStateUpdate()
    {
        CurrentState?.Update();

    }
    /// <summary>
    /// Calling Fixed Update of State if it Exists
    /// </summary>
    public void CallStateFixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }
}


/// <summary>
/// Activity State Base Class
/// </summary>
public abstract class FriendlyAIActivityState : State<FriendlyAIDirective>
{
    private FriendlyControllerScript FriendlyBaseClass;
    private FriendlyAIActivityStateMachine StateMachine;

    /// <summary>
    /// FriendlyAI states Constructor
    /// </summary>
    /// <param name="stateMachine"></param>
    /// <param name="friendlyController"></param>
    protected FriendlyAIActivityState(FriendlyAIActivityStateMachine stateMachine, FriendlyControllerScript friendlyController)
    {
        this.FriendlyBaseClass = friendlyController;
        this.StateMachine = stateMachine;
    }

    protected void CallDefaultMovement() => FriendlyBaseClass.HoverPlayer();

    protected void CheckForAttack() => FriendlyBaseClass.CheckForTarget();

    protected void ManageHealing() => FriendlyBaseClass.StartHealing();

    protected void ManageShielding() => FriendlyBaseClass.StartShielding();

    protected void CastProjectileShield() => FriendlyBaseClass.StartProjectileShielding();
}