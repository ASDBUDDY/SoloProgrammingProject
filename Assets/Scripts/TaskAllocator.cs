using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAllocator : MonoBehaviour
{
    public PlayerControllerScript PlayerController;
    public FriendlyControllerScript FriendlyController;




    private void Update()
    {
        CheckForAttackingTarget();
    }
    private void CheckForAttackingTarget()
    {
        if (EnemyDetector.Instance.EnemyCountRingHigh.Count > 0)
        {

            GameObject target = EnemyDetector.Instance.EnemyCountRingHigh[0];
            AllyTask allyTask = new AllyTask(TaskPriority.Default, FriendlyAIDirective.AttackMelee);
            TaskManager.Instance.AddTask(allyTask);
            FriendlyController.TargetEnemy = target;

           
        }
        else if (EnemyDetector.Instance.EnemyCountRingMedium.Count > 0)
        {

            GameObject target = EnemyDetector.Instance.EnemyCountRingMedium[0];
            AllyTask allyTask = new AllyTask(TaskPriority.Default, FriendlyAIDirective.AttackMelee);
            TaskManager.Instance.AddTask(allyTask);
            FriendlyController.TargetEnemy = target;


        }
        else
        {

        }
        

    }
}
