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

    public float MeleeDamage = 5f;

    public GameObject MeleeObj;

    public AllyTask CurrentTask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activityStateMachine?.CallStateUpdate();
    }

    public void HoverPlayer()
    {
        if (!agent.enabled)
        {
            agent.enabled = true;
        }
        agent.SetDestination(playerController.gameObject.transform.parent.transform.position);
        this.gameObject.transform.LookAt(playerController.gameObject.transform.parent.transform.position);
        agent.stoppingDistance = 5f;
        animHandler.SetMoving(agent.velocity.magnitude > 0f);
    }

    public void CallAttack()
    {
       animHandler.SetAttacking(true);
        MeleeObj.SetActive(true);

    }

    public void AttackOver()
    {
        animHandler?.SetAttacking(false);
        CurrentTask.TaskOver();
    }


}
