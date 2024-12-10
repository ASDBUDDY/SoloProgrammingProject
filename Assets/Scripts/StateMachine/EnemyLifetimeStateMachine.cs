using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifetimeStateMachine : StateMachine<EnemyLifetimeState, LifetimeStates>//State Machine For All Enemy States during it's Lifetime
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
        states = new EnemyLifetimeState[5];
        states[0] = new EnemyLifetimeInactiveScript(this,EnemyAnimHandler, EnemyClass);
        states[1] = new EnemyLifetimeSpawningScript(this, EnemyAnimHandler, EnemyClass);
        states[2] = new EnemyLifetimeActiveScript(this, EnemyAnimHandler, EnemyClass);
        states[3] = new EnemyLifetimeDeathScript(this, EnemyAnimHandler, EnemyClass);
        states[4] = new EnemyLifetimePauseScript(this, EnemyAnimHandler, EnemyClass);


        //Set to Default State Post Initialisation
        SetState(LifetimeStates.Spawning);
    }
    /// <summary>
    /// Calling Update of State if it Exists
    /// </summary>
    private void Update()
    {
        CurrentState?.Update();

    }
    /// <summary>
    /// Calling Fixed Update of State if it Exists
    /// </summary>
    private void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }

    /// <summary>
    /// Resets Enemy before deactivation
    /// </summary>
    public void ResetEnemy() => EnemyClass.ResetSelf();
}

// <summary>
/// Enum of States for Lifetime StateMachine
/// </summary>
public enum LifetimeStates
{
        Inactive,
        Spawning,
        Active,
        Death,
        Pause
}

/// <summary>
/// Lifetime State Base Class
/// </summary>
public abstract class EnemyLifetimeState : State<LifetimeStates>
{
    private EnemyAnimationHandler EnemyAnimHandler;
   private EnemyLifetimeStateMachine StateMachine;
    private EnemyBaseClass EnemyClass;

    /// <summary>
    /// Constructor for Enemy Lifetime State Base Class
    /// </summary>
    /// <param name="stateMachine"></param>
    /// <param name="enemyAnimHandler"></param>
    /// <param name="enemyClass"></param>
    protected EnemyLifetimeState(EnemyLifetimeStateMachine stateMachine, EnemyAnimationHandler enemyAnimHandler,EnemyBaseClass enemyClass)
    {
       this.EnemyAnimHandler = enemyAnimHandler;
        this.StateMachine = stateMachine;
        this.EnemyClass = enemyClass;
    }

    /// <summary>
    /// Checks if Spawn Animation is over
    /// </summary>
    /// <returns></returns>
    protected bool CheckForSpawn()
    {
        if (EnemyAnimHandler != null)
        {
           
            return EnemyAnimHandler.GetSpawnOver;
        }
        return false;
    }

    /// <summary>
    /// Sets Spawn Anim over
    /// </summary>
    /// <param name="spawn"></param>
    protected void SetSpawn(bool spawn = true) => EnemyAnimHandler.SetSpawn(spawn);

    /// <summary>
    /// Triggers Death Animation
    /// </summary>
    protected void DeathTrigger() => EnemyAnimHandler.TriggerDeathState();
    
    /// <summary>
    /// Calls Activity State's Update function
    /// </summary>
    protected void CallActivityStateUpdate() => EnemyClass.CallActivityStateUpdate();
    /// <summary>
    /// Sets Activity State on Activity State Machine
    /// </summary>
    /// <param name="state"></param>
    protected void SetActivityState(ActivityStates state = ActivityStates.Movement) => EnemyClass.SetupActivityState(state);

    /// <summary>
    /// Sets Lifetime State on Lifetime State Machine
    /// </summary>
    /// <param name="state"></param>
    protected void SetState(LifetimeStates state) { 

        StateMachine.SetState(state);
    }

    protected void ToggleAnimator(bool flag =true) => EnemyAnimHandler.ToggleAnimator(flag);
}