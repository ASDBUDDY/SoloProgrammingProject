using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static BaseClasses.EnemyBaseClass;
using static BaseClasses.EnemyStatsClass;

namespace BaseClasses
{
    public class EnemyBaseClass : MonoBehaviour
    {
        #region Variables

        
        [SerializeField] protected NavMeshAgent selfAgent;
        [SerializeField] protected GameObject targetPlayer;
        [SerializeField] protected float DeathTimer = 30f;
        [SerializeField] protected EnemyStatsListSO EnemyDataList;
        [SerializeField] protected EnemyAnimationHandler AnimHandler;
        [SerializeField] protected List<Renderer> BodyMats;

        [SerializeField] protected HitDetectionClass AttackRadius;
        [HideInInspector] public EnemyTypes enemyTypes = EnemyTypes.Melee;//Used to Identify the script during object instantiation

        private Coroutine hitCoroutine;
        private bool doFlash = false;

        protected Vector3 EnemyScale = new Vector3 (2f,2f,2f); // Just for current animations

        public EnemyStatsClass EnemyStats;
        /// <summary>
        /// How long the Enemy has been in Active state for
        /// </summary>
        protected float activeTimer = 0;
        
        /// <summary>
        /// Boolean to check if Player in Range
        /// </summary>
       [SerializeField] protected bool playerInRange = false;

        /// <summary>
        /// Time since last attack
        /// </summary>
        protected float attackTimer = 0f;

        /*  /// <summary>
          /// List of Skills that the enemy can undertake
          /// </summary>
          List<Skills> EnemySkills;*/


        /// <summary>
        /// Health Base Class for Enemy
        /// </summary>
        public HealthComponent enemyHealthComponent;

        private LifetimeStates previousState;



        #endregion

        #region StateMachines
        /// <summary>
        /// StateMachine that handles the lifetime phases of the enemy
        /// </summary>
        public EnemyLifetimeStateMachine LifetimeSM; // States - Spawning(Invul State + animation), Active(Movement, attacks, skills), Death(Invul State + animation + reset to object pool)

        /// <summary>
        /// StateMachine that handles all Actions during the Active phase of the Enemy
        /// </summary>
        public  EnemyActivityStateMachine ActivitySM; // States -  Movement(moving towards player state), AttackOnly(If enemy needs to stop and attack), MovingAttack( Attacking while moving)
        #endregion

        #region Functions
        protected void Awake()
        {
            selfAgent.enabled = false;
        }

        protected void Start()
        {
            SetupEnemy();
        }

        protected void Update()
        {
            CheckPlayerRange();
            ExecuteAttack();// Check for player and execute attack accordingly

            if (LifetimeSM != null)
            {
                if (LifetimeSM.CurrentStateType == LifetimeStates.Active) { // If Enemy is in active state, update the activity Timer
                    activeTimer += Time.deltaTime;

                    if (ActivitySM != null) {
                        //Debug.Log($"Current state {ActivitySM.CurrentStateType}");
                        if(ActivitySM.CurrentStateType != ActivityStates.Attacking)// If Enemy isn't attacking, update timer for attack
                        {
                            attackTimer += Time.deltaTime;

                        }
                    }
                
                }
            }
            CheckForEnemyHealth();
            
        }
       /// <summary>
       /// Handling Pause on Enemy Game Object
       /// </summary>
       /// <param name="pause"></param>
       public void TogglePause(bool pause)
        {
            if (pause) {
                selfAgent.enabled = false;
                previousState = LifetimeSM.CurrentStateType;
                LifetimeSM.SetState(LifetimeStates.Pause);
            }

            else {
                if (previousState != null)
                    LifetimeSM.SetState(previousState);
                else
                    LifetimeSM.SetState(LifetimeStates.Inactive);

                selfAgent.enabled = true;
            }
        }

        /// <summary>
        /// Turns off Mat Emission
        /// </summary>
        private void HitIndicationStop()
        {
            foreach (Renderer rend in BodyMats)
            {
                Material mat = rend.material;
                mat.DisableKeyword("_EMISSION");
                mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            }
        }
        /// <summary>
        /// Turns on Mat Emission
        /// </summary>
        private void HitIndicationStart()
        {
            foreach (Renderer rend in BodyMats)
            {
                Material mat = rend.material;
                mat.EnableKeyword("_EMISSION");
                mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;
            }
        }

        /// <summary>
        /// Coroutine for Hit Visualisation
        /// </summary>
        /// <returns></returns>
       private IEnumerator HitVisual()
        {
            HitIndicationStart();
            yield return new WaitForSeconds(0.2f);
            HitIndicationStop();
        }

        /// <summary>
        /// Initial Enemy setup
        /// </summary>
        protected void SetupEnemy()
        {
            if (EnemyDataList != null)
            {
                EnemyStats = EnemyDataList.GetStats(enemyTypes);
            }
            else
            {
                Debug.LogError("Enemy Data List not Found");
                EnemyStats = new EnemyStatsClass();
            }

            enemyHealthComponent = new HealthComponent(EnemyStats.maxHealth);
            selfAgent.speed = EnemyStats.enemySpeed;
            AttackRadius?.SetupDamage(EnemyStats.enemyAttackPower);
            if(enemyTypes == EnemyTypes.Ranged)
                EnemyDetector.Instance.RangedEnemyPresent++;
        }

        /// <summary>
        /// Sets Target Player after spawning
        /// </summary>
        /// <param name="targetPlayer"></param>
        public void SetTargetPlayer(GameObject targetPlayer) => this.targetPlayer = targetPlayer;

        /// <summary>
        /// Movement Function called every frame
        /// </summary>
           public void MovementFunction()
        {

            if (!selfAgent.isActiveAndEnabled)  
                selfAgent.enabled = true;
            
            if (targetPlayer != null) {
                selfAgent.SetDestination(targetPlayer.transform.position);
                this.transform.LookAt(targetPlayer.transform.position);
                    }
            
           
        }

/// <summary>
/// Toggle Hit Detection on attack
/// </summary>
/// <param name="flag"></param>
       public void HitDetection(bool flag) => AttackRadius?.gameObject.SetActive(flag);

        
        /// <summary>
        /// Checks Target in range every frame
        /// </summary>
        public void CheckPlayerRange()
        {
            if (targetPlayer != null) {
                float distance = Vector3.Distance(this.transform.position, targetPlayer.transform.position);

                playerInRange = (distance >= EnemyStats.minAttackRange && distance < EnemyStats.maxAttackRange);
               
            }
           
        }
        /// <summary>
        /// Calculates when attacks can be executed and executes them
        /// </summary>
        public void ExecuteAttack()
        {   if (ActivitySM.CurrentStateType == ActivityStates.Attacking)
                return;
           
            if(attackTimer > EnemyStats.enemyAttackRate && playerInRange) {
          
                ActivitySM.SetState(ActivityStates.Attacking);
                attackTimer = 0;
            
            }

        }

        /// <summary>
        /// Called at the start of an attack
        /// </summary>
        public virtual void AttackFunction()
        {
            selfAgent.enabled = false;
            this.transform.LookAt(targetPlayer.transform.position);
            
        }
        /// <summary>
        /// Calls Update of the current Activity State
        /// </summary>
        public void CallActivityStateUpdate() => ActivitySM.CallStateUpdate();
        /// <summary>
        /// Sets state for Activity State Machine
        /// </summary>
        public void SetupActivityState(ActivityStates state=ActivityStates.Movement) => ActivitySM.SetState(state);

        protected void CheckForEnemyHealth()
        {
            if (enemyHealthComponent.CurrentHealth <= 0 && LifetimeSM.CurrentStateType!=LifetimeStates.Death)
                ExecuteDeath();
        }

        public void TakeDamage(float damage)
        {
            enemyHealthComponent.DamageHealth(damage);
            /*if(hitCoroutine != null)
                StopCoroutine(hitCoroutine);
            hitCoroutine = StartCoroutine(HitVisual());*/
        }

        /// <summary>
        /// Executes Death of Enemy
        /// </summary>
        protected void ExecuteDeath()
        {

            //EnemyTracker.Instance.RemoveEnemy(animalTypeForScript);
            LifetimeSM.SetState(LifetimeStates.Death);
            activeTimer = 0;
            
         
        }

        /// <summary>
        /// Resets Enemy after death animation death
        /// </summary>
        public void ResetSelf() {
            /*StopCoroutine(HitVisual());
            HitIndicationStop();*/
            LifetimeSM.SetState(LifetimeStates.Inactive);
            LifetimeSM.gameObject.transform.localScale = EnemyScale;
            enemyHealthComponent = new HealthComponent(EnemyStats.maxHealth);
            EnemyDetector.Instance.RemoveEnemyOnDeath(LifetimeSM.gameObject);
            if (enemyTypes == EnemyTypes.Ranged)
                EnemyDetector.Instance.RangedEnemyPresent--;
            this.gameObject.SetActive(false);
            
            
        }

        

        #endregion
    }

    [System.Serializable]
    public  class EnemyStatsClass 
    {

        public string enemyName;
        public EnemyTypes enemyType;
      
        /// <summary>
        /// Enemy Movement Speed
        /// </summary>
        public float enemySpeed;
        /// <summary>
        /// Maximum Health
        /// </summary>
        public float maxHealth;
        /// <summary>
        /// Attack Power of the enemy
        /// </summary>
        public float enemyAttackPower;

        /// <summary>
        /// Time between enemy attacks
        /// </summary>
        public float enemyAttackRate;

        /// <summary>
        /// Minimum Distance to execute attack
        /// </summary>
        public float minAttackRange;

        /// <summary>
        /// Maximum Distance to execute attack
        /// </summary>
        public float maxAttackRange;

        public enum EnemyTypes
        {
            Melee,
            Ranged,
            MeleeSlow
        }

        
        /// <summary>
        /// Clones and Returns a clone of the class
        /// </summary>
        /// <returns></returns>
        public EnemyStatsClass Clone()
        {
            EnemyStatsClass stats = new EnemyStatsClass();
            stats.enemyName = this.enemyName;
            stats.enemyType = this.enemyType;
            stats.maxHealth = this.maxHealth;
            stats.enemySpeed = this.enemySpeed;
            stats.enemyAttackPower = this.enemyAttackPower;
            stats.enemyAttackRate = this.enemyAttackRate;
            stats.minAttackRange = this.minAttackRange;
            stats.maxAttackRange = this.maxAttackRange;

            return stats;
        }

    }
}
