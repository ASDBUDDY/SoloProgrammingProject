using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<EnemyTask> EnemyTaskList;

    public bool SortIt = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddTask(EnemyTask task)
    {
        EnemyTaskList.Insert(0, task);
        SortTaskList();
    }

    private void SortTaskList()
    {
        EnemyTaskList = EnemyTaskList.OrderByDescending(task=>task.priority).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (SortIt)
        {
            SortTaskList();
            SortIt = false;
        }
    }
}

[Serializable]
public class EnemyTask
{
    public TaskPriority priority;
    public EnemyDirective directive;

    EnemyTask(TaskPriority priority, EnemyDirective directive)
    {
        this.priority = priority;
        this.directive = directive;
    }
}

[Serializable]
public enum EnemyDirective
{
    AttackMelee=0,
    AttackRanged=1,
    Healing=2,
    Shield=3,
    ProjectileShield=4,
    Decoy=5
}

[Serializable]
public enum TaskPriority
{
    Default=0,
    Medium=1,
    High=2
}

