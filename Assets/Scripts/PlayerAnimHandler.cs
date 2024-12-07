
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimHandler : MonoBehaviour
{
   
    public Animator PlayerAnimator;

    //Constants
    public const string VelocityX = "VelocityX";
    public const string VelocityZ = "VelocityZ";
    public const string IsMoving = "isMoving";
    public  const string IsRunning = "isSprinting";
    public const string IsBlocking = "isBlocking";
    public const string IsAttacking = "isAttacking";
    public  const string AttackPattern = "AttackPattern";
    private int AttackPatternLimit = 1;
    private int CurrentAttackPattern = 0;

    private void Awake()
    {
       
        PlayerAnimator = GetComponent<Animator>();

    }

    public void UpdateVelocity(Vector3 vel, bool isMove = false,bool isRun = false)
    {
        PlayerAnimator.SetFloat(VelocityX, vel.x);
        PlayerAnimator.SetFloat(VelocityZ, vel.z);
        PlayerAnimator.SetBool(IsMoving, isMove);
        PlayerAnimator.SetBool(IsRunning, isRun);
    }
    public void CallBlock(bool block=false) => PlayerAnimator.SetBool(IsBlocking, block);

    public void CallAttack()
    {
        PlayerAnimator.SetBool(IsAttacking, true);
        PlayerAnimator.SetInteger(AttackPattern, CurrentAttackPattern);
        CurrentAttackPattern++;
        if (CurrentAttackPattern > AttackPatternLimit)
        {
            CurrentAttackPattern = 0;
        }
        
    }

    public void EndAttack()
    {
        PlayerAnimator.SetBool(IsAttacking, false);
    }
   
}
