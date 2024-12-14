using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyControllerScript : MonoBehaviour
{
    public NavMeshAgent agent;

    public PlayerControllerScript playerController;

    public AllyAnimHandler animHandler;

    public GameObject TargetEnemy;

    public FriendlyAIActivityStateMachine activityStateMachine;
    public GameObject MeleeObj;
    public GameObject HealingObj;
    public GameObject ShieldObj;

    public AllyTask CurrentTask;

    #region StatsVariable
    [HideInInspector]
    public float MeleeDamage = 2f;

    private float HealAmount = 5f;

    private float AttackRate = 5f;

    private float AttackTimer = 0f;

    private float ArmourAmount = 15f;
    #endregion
    private bool CanAttack = true;
    private bool IsHealing = false;
    private bool IsShielding = false;
    // Start is called before the first frame update
    void Start()
    {
        CurrentTask.TaskOver();
        
    }

    // Update is called once per frame
    void Update()
    {

        GetNewTask();
        activityStateMachine?.CallStateUpdate();
        TimerFunction();
    }

    #region Default State
    public void HoverPlayer()
    {
        if (!agent.enabled)
        {
            agent.enabled = true;
        }
        agent.SetDestination(playerController.gameObject.transform.parent.transform.position);
        this.gameObject.transform.LookAt(playerController.gameObject.transform.parent.transform.position);
        agent.speed = 25f;
        agent.stoppingDistance = 5f;
        animHandler.SetMoving(agent.velocity.magnitude > 0f);
    }
    #endregion

    #region AttackMelee State
    public void CheckForTarget()
    {
        if (animHandler.GetAttacking())
            return;
        if (!CanAttack)
        {
            HoverPlayer();
            return;
        }
        if (!agent.enabled)
        {
            agent.enabled=true;
        }

        Debug.Log("I am attack?");
        if(TargetEnemy != null && TargetEnemy.activeInHierarchy)
        {
            Debug.Log("I am go attack?");
            agent.SetDestination(TargetEnemy.transform.position);
            this.gameObject.transform.LookAt(TargetEnemy.transform.position);
            agent.stoppingDistance = 2f;
            agent.speed = 10f;
            animHandler.SetMoving(agent.velocity.magnitude > 0f);

            if(Vector3.Distance(this.transform.position, TargetEnemy.transform.position) < agent.stoppingDistance + 2)
            {
                CallAttack();
            }
        }
        else
        {
            CurrentTask.TaskOver();
            TargetEnemy = null;
        }
    }
    public void CallAttack()
    {
       animHandler?.SetAttacking(true);
        MeleeObj.SetActive(true);
        CanAttack = false;
        CurrentTask.TaskOver();

    }

    public void AttackOver()
    {
        animHandler?.SetAttacking(false);
        CurrentTask.TaskOver();
    }
    #endregion

    #region Healing State

    public void StartHealing()
    {
     
        if (!agent.enabled) { agent.enabled = true; }
        HoverPlayer();

        if (Vector3.Distance(this.transform.position, playerController.transform.position) < agent.stoppingDistance && !IsHealing)
        {
            CallHealing();
        }

    }

    public void CallHealing()
    {
        IsHealing = true;
        animHandler.SetHealing(IsHealing);
        HealingObj.SetActive(true);

    }

    public void HealingOver()
    {
        IsHealing = false;
        animHandler.SetHealing(IsHealing);
        HealingObj.SetActive(false);
        playerController.healthComponent.IncreaseHealth(HealAmount);
        CurrentTask.TaskOver();
    }

    #endregion

    #region Shielding State
    public void StartSheilding()
    {

        if (!agent.enabled) { agent.enabled = true; }
        HoverPlayer();

        if (Vector3.Distance(this.transform.position, playerController.transform.position) < agent.stoppingDistance && !IsShielding)
        {
            CallShielding();
        }

    }

    public void CallShielding()
    {
        IsShielding = true;
        animHandler.SetHealing(IsShielding);
        ShieldObj.SetActive(true);

    }

    public void ShieldingOver()
    {
        IsShielding = false;
        animHandler.SetHealing(IsShielding);
        ShieldObj.SetActive(false);
        playerController.healthComponent.GiveArmour(ArmourAmount);
        CurrentTask.TaskOver();
    }

    #endregion
    public void GetNewTask()
    {
        if (CurrentTask == null || CurrentTask.isOver)
        {
            AllyTask newTask = TaskManager.Instance.GetTask();
            if (newTask != null)
            {
                CurrentTask = newTask;
                SetupTask();
            }
            else
            {
                activityStateMachine.SetState(FriendlyAIDirective.Default);
            }


        }
    }

    private void SetupTask()
    {
        activityStateMachine.SetState(CurrentTask.directive);

    }

    private void TimerFunction()
    {
        if (AttackTimer > AttackRate)
        {
            CanAttack = true;
            AttackTimer = 0f;
        }
        else
            AttackTimer += Time.deltaTime;
    }
}
