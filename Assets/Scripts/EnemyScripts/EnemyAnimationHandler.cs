using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    [SerializeField] EnemyLifetimeStateMachine LifetimeSM; //Lifetime State Machine
    [SerializeField] EnemyActivityStateMachine ActivitySM;// Activity State Machine



    #region Animator Parameters

    public const string IsSpawningOver = "isSpawningOver";
    public const string IsAttackingOver = "isAttackingOver";
    public const string StartedMovement = "isMoving";
    public const string TriggerDeath = "TriggerDeath";
    public const string TriggerAttack = "TriggerAttack";
    #endregion

    #region Functions
    private void Awake()
    {
        //Assigning Essential components
        enemyAnimator = this.GetComponent<Animator>();
        LifetimeSM = this.GetComponent<EnemyLifetimeStateMachine>();
        ActivitySM = this.GetComponent<EnemyActivityStateMachine>();
    }

    public void SetSpawn(bool spawn = true) => enemyAnimator.SetBool(IsSpawningOver, spawn); //Sets when the spawning animation is finished

    public void SetMovement(bool move = true) => enemyAnimator.SetBool(StartedMovement, move); //Sets when the movement animation starts
    public void SetAttacking(bool attack = true) => enemyAnimator.SetBool(IsAttackingOver, attack); //Sets when the attacking animation is finished
    public void TriggerDeathState() => enemyAnimator.SetTrigger(TriggerDeath);// Trigger to transition to Death Animation
    public void TriggerAttackState() => enemyAnimator.SetTrigger(TriggerAttack);// Trigger to transition to Attack Animation
    public bool GetSpawnOver => enemyAnimator.GetBool(IsSpawningOver);// Getter to check for Spawning Animation over
    public bool GetMovement => enemyAnimator.GetBool(StartedMovement);//Getter to check for Movement Anim started

    public void ToggleAnimator(bool animated = true) => enemyAnimator.speed = animated ? 1 : 0;
   
    #endregion
}
