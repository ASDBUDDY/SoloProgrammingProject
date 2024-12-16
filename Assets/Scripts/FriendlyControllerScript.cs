using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
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
    public GameObject ShieldPresetObj;
    public GameObject AllyProjectile;

    public GameObject ProjectilShieldPrefab;

    public AllyTask CurrentTask;

    public TMP_Text AIDirective;
    #region StatsVariable
    [HideInInspector]
    public float MeleeDamage = 2f;
    [HideInInspector]
    public float RangeDamage = 3f;

    private float HealAmount = 5f;

    private float AttackRate = 5f;

    private float AttackTimer = 0f;
    private float ThrowRate = 2f;

    private float ThrowTimer = 0f;
    private float projectileSpeed = 600f;

    private float ArmourAmount = 15f;
    private float NewTaskTime = 0.5f;
    #endregion
    private bool CanAttack = true;
    private bool CanThrow = true;
    private bool IsHealing = false;
    private bool IsShielding = false;
    private bool IsProjShielding = false;
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
        

        if (!agent.enabled)
        {
            agent.enabled=true;
        }

     
        if(TargetEnemy != null && TargetEnemy.activeInHierarchy && CanAttack)
        {
          
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
            activityStateMachine.SetState(FriendlyAIDirective.Default);
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
        activityStateMachine.SetState(FriendlyAIDirective.Default);
        CurrentTask.TaskOver();
    }
    #endregion
    #region AttackRanged State
    public void CheckForThrowTarget()
    {
        if (animHandler.GetAttacking())
            return;
       

        if (!agent.enabled)
        {
            agent.enabled = true;
        }


        if (TargetEnemy != null && TargetEnemy.activeInHierarchy)
        {
          
            agent.SetDestination(TargetEnemy.transform.position);
            this.gameObject.transform.LookAt(TargetEnemy.transform.position);
            agent.stoppingDistance = 8f;
            agent.speed = 10f;
            animHandler.SetMoving(agent.velocity.magnitude > 0f);

            if (Vector3.Distance(this.transform.position, TargetEnemy.transform.position) < agent.stoppingDistance && CanThrow)
            {
                CallRangedAttack();
            }
        }
        else
        {
            activityStateMachine.SetState(FriendlyAIDirective.Default);
            CurrentTask.TaskOver();
            TargetEnemy = null;
        }
    }
    public void CallRangedAttack()
    {
        animHandler?.SetAttacking(true);
        //MeleeObj.SetActive(true);
        CanThrow = false;

        ProjectileFunction();

    }
    public void ProjectileFunction()
    {
        Vector3 spawnPos = this.transform.position;
        GameObject bullet = Instantiate(AllyProjectile, spawnPos, this.transform.rotation);
        var projectile = bullet.GetComponent<ProjectileBaseClass>();
        projectile.SetupDamage(RangeDamage);
        var rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) { rb.AddForce(transform.forward * projectileSpeed); }
        Invoke(nameof(RangedAttackOver), 0.5f);
    }
    public void RangedAttackOver()
    {
        animHandler?.SetAttacking(false);
        activityStateMachine.SetState(FriendlyAIDirective.Default);
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
        activityStateMachine.SetState(FriendlyAIDirective.Default);
        CurrentTask.TaskOver();
    }

    #endregion

    #region Shielding State
    public void StartShielding()
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
        activityStateMachine.SetState(FriendlyAIDirective.Default);
        CurrentTask.TaskOver();
    }

    #endregion
    #region Projectile Shielding State
    public void StartProjectileShielding()
    {
        
            

        if (!agent.enabled) { agent.enabled = true; }
        HoverPlayer();

        if (TaskManager.Instance.ExistingProjectileShield)
        {
            CurrentTask.TaskOver();
            return;
        }

            if (Vector3.Distance(this.transform.position, playerController.transform.position) < agent.stoppingDistance && !IsProjShielding)
            {
                CallProjectileShielding();
            }

    }

    public void SetStateMachineState(FriendlyAIDirective directive)
    {
        activityStateMachine.SetState(directive);

        if(directive == FriendlyAIDirective.AttackMelee)
            if(!CanAttack)
                directive = FriendlyAIDirective.Default;

        AIDirective.text = directive switch
        {
            FriendlyAIDirective.Default => GameConstantsClass.ALLY_DEFAULT,
            FriendlyAIDirective.Healing => GameConstantsClass.ALLY_HEALING,
            FriendlyAIDirective.AttackMelee => GameConstantsClass.ALLY_MELEE,
            FriendlyAIDirective.AttackRanged => GameConstantsClass.ALLY_RANGED,
            FriendlyAIDirective.Shield => GameConstantsClass.ALLY_SHIELD,
            FriendlyAIDirective.ProjectileShield => GameConstantsClass.ALLY_PROJ_SHIELD,
            FriendlyAIDirective.Decoy => GameConstantsClass.ALLY_DECOY,
            _ => GameConstantsClass.ALLY_DEFAULT
        };

    }
    public void CallProjectileShielding()
    {
        IsProjShielding = true;
        animHandler.SetHealing(IsProjShielding);
        ShieldPresetObj.SetActive(true);
        TaskManager.Instance.ExistingProjectileShield = true;

    }

    public void ProjectileShieldingOver()
    {
        IsProjShielding = false;
        animHandler.SetHealing(IsProjShielding);
        Instantiate(ProjectilShieldPrefab, transform.position, transform.rotation);
        
        ShieldPresetObj.SetActive(false);
        activityStateMachine.SetState(FriendlyAIDirective.Default);
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
                Debug.Log("New task here");
                Invoke(nameof(SetupTask),NewTaskTime);
                
                
            }
            else
            {
                SetStateMachineState(FriendlyAIDirective.Default);
            }


        }
    }

    private void SetupTask()
    {
        SetStateMachineState(CurrentTask.directive);
        Debug.Log("New task called!");

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

        if (ThrowTimer > ThrowRate)
        {
            CanThrow = true;
            ThrowTimer= 0f;
        }
        else
            ThrowTimer += Time.deltaTime;
    }
}
