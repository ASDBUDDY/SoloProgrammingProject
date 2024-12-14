using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAllocator : MonoBehaviour
{
    public PlayerControllerScript PlayerController;
    public FriendlyControllerScript FriendlyController;

    private float RecheckRate = 1f;

    private float RecheckTimer = 0f;



    private void Update()
    {
        if (RecheckTimer > RecheckRate)
        {
            RecheckTimer = 0f;
            CheckForPlayerHealth();
            CheckForPlayerArmour();
            CheckForAttackingTarget();
        }
        else
            RecheckTimer += Time.deltaTime;
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

    private void CheckForPlayerHealth()
    {
        int percentageHealth = (int)((PlayerController.healthComponent.CurrentHealth/PlayerController.healthComponent.MaxHealth) * 100);

        AllyTask allyTask = new AllyTask(TaskPriority.Default,FriendlyAIDirective.Healing);
        if (percentageHealth < 20)
        {
            allyTask.ResetPriority(TaskPriority.High);
        }
        else if (percentageHealth > 20 && percentageHealth < 50) { 
            allyTask.ResetPriority(TaskPriority.Medium);
        }
        else if(percentageHealth > 70)
        {
            return;
        }

        TaskManager.Instance.AddTask(allyTask);
    }


    private void CheckForPlayerArmour()
    {
        if (PlayerController.healthComponent.CurrentArmour > 0)
            return;

        AllyTask allyTask = new AllyTask(TaskPriority.Default,FriendlyAIDirective.Shield);
        if (EnemyDetector.Instance.HighRingIntensity == IntensityCriteria.High)
            allyTask.ResetPriority(TaskPriority.High);
        else if (EnemyDetector.Instance.HighRingIntensity == IntensityCriteria.Medium || EnemyDetector.Instance.MediumRingIntensity == IntensityCriteria.High)
            allyTask.ResetPriority(TaskPriority.Medium);
        else if(EnemyDetector.Instance.MediumRingIntensity == IntensityCriteria.Low && EnemyDetector.Instance.DefaultRingIntensity == IntensityCriteria.High)
                { }
        else
        {
            return;
        }

        TaskManager.Instance.AddTask(allyTask);
        
    }
}
