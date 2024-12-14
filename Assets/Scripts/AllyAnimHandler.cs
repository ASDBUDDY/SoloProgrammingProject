using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyAnimHandler : MonoBehaviour
{
    private const string isAttacking = "isAttacking";
    private const string isMoving = "isMoving";
    private const string isHealing = "isHealing";

    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMoving(bool flag) => animator.SetBool(isMoving, flag);
    public void SetAttacking(bool flag) => animator.SetBool(isAttacking, flag);
    public void SetHealing(bool flag) => animator.SetBool(isHealing, flag);



}
