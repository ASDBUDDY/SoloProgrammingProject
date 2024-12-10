using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivityStateMachine : StateMachine<EnemyActivityState, ActivityStates> //State Machine For All Enemy actions while the Enemy is Active
{
    /// <summary>
    /// Animation Handler for Enemy Animator
    /// </summary>
    [SerializeField] EnemyAnimationHandler EnemyAnimHandler;
    [SerializeField] EnemyBaseClass EnemyClass;
    private void Awake()
    {
        // Sets up Essential components
        EnemyAnimHandler = this.GetComponent<EnemyAnimationHandler>();
        EnemyClass = this.GetComponentInParent<EnemyBaseClass>();

        //Array of States to be initialized for StateMachine to be set to
        states = new EnemyActivityState[3];
        states[0] = new EnemyActivityDefaultState(this, EnemyAnimHandler, EnemyClass);
        states[1] = new EnemyActivityAttackState(this, EnemyAnimHandler, EnemyClass);
        states[2] = new EnemyActivityMovementState(this, EnemyAnimHandler, EnemyClass);
      


        //Set to Default State Post Initialisation
        SetState(ActivityStates.Movement);
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
    //Set Activity StateMachine to Movement State
    public void ReturnToMovement() => EnemyClass.SetupActivityState(ActivityStates.Movement);  

    public void ToggleHitDetectionOn()=> EnemyClass.HitDetection(true);
    public void ToggleHitDetectionOff()=> EnemyClass.HitDetection(false);
}
/// <summary>
/// Enum of States for Activity StateMachine
/// </summary>
public enum ActivityStates
{
        Default,
        Attacking,
        Movement
        
}

/// <summary>
/// Activity State Base Class
/// </summary>
public abstract class EnemyActivityState : State<ActivityStates>
{
    private EnemyAnimationHandler EnemyAnimHandler;
   private EnemyActivityStateMachine StateMachine;
    private EnemyBaseClass EnemyClass;

    /// <summary>
    /// Constructor for Enemy Activity State Base Class
    /// </summary>
    /// <param name="stateMachine"></param>
    /// <param name="enemyAnimHandler"></param>
    /// <param name="enemyClass"></param>
    protected EnemyActivityState(EnemyActivityStateMachine stateMachine, EnemyAnimationHandler enemyAnimHandler,EnemyBaseClass enemyClass)
    {
       this.EnemyAnimHandler = enemyAnimHandler;
        this.StateMachine = stateMachine;
        this.EnemyClass = enemyClass;
    }

    /// <summary>
    /// Checks for if Movement Animation Started
    /// </summary>
    /// <returns></returns>
    protected bool CheckForMovementStart()
    {
        if (EnemyAnimHandler != null)
        {

            return EnemyAnimHandler.GetMovement;
        }
        return false;
    }

    /// <summary>
    /// Triggers Attack Functionality
    /// </summary>
    protected void SetupAttack()
    {
        EnemyAnimHandler.SetMovement(false);
        EnemyClass.AttackFunction();
        EnemyAnimHandler.TriggerAttackState();
    }

    /// <summary>
    /// Sets Activity Machine State
    /// </summary>
    /// <param name="state"></param>
    protected void SetState(ActivityStates state)
    {

        StateMachine.SetState(state);
    }
    /// <summary>
    /// Calls Movement Functionality
    /// </summary>
    protected void CallMovement()
    {
        EnemyClass.MovementFunction();
    }


}