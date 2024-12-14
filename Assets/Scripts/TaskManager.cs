using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }
    public List<AllyTask> AllyTaskList;

    public bool SortIt = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        AllyTaskList = new List<AllyTask>();
    }

    public void AddTask(AllyTask task)
    {
        AllyTaskList.Insert(0, task);
        SortTaskList();
    }
    public AllyTask GetTask()
    {
        if(AllyTaskList.Count == 0)
            return null;

        AllyTask task = AllyTaskList[0];
        if (task != null)
        {
            AllyTaskList.Remove(task);

        }
        return task;
    }
    private void SortTaskList()
    {
        AllyTaskList = AllyTaskList.OrderByDescending(task=>task.priority).ToList();
        if(AllyTaskList.Count > 10)
        {
            AllyTaskList.RemoveAt(10);
        }
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
public class AllyTask
{
    public TaskPriority priority;
    public FriendlyAIDirective directive;
    public bool isOver { get; private set; }

   public AllyTask(TaskPriority priority, FriendlyAIDirective directive)
    {
        this.priority = priority;
        this.directive = directive;
    }

    public void TaskOver() => isOver = true;
}

[Serializable]
public enum FriendlyAIDirective
{
    Default= -1,
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

