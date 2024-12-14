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

    public float MeleeDamage = 2f;

    public GameObject MeleeObj;

    public AllyTask CurrentTask;


    private float AttackRate = 3f;

    private float AttackTimer = 0f;

    private bool CanAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        CurrentTask.TaskOver();
        
    }

    // Update is called once per frame
    void Update()
    {

        activityStateMachine?.CallStateUpdate();
        GetNewTask();
        TimerFunction();
    }

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

    }

    public void AttackOver()
    {
        animHandler?.SetAttacking(false);
        CurrentTask.TaskOver();
    }

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
