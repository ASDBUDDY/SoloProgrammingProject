using BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBaseClass : MonoBehaviour
{
    /// <summary>
    /// Damage done per attack
    /// </summary>
    private float damageStat;
    //[SerializeField] FloatEventChannelSO playerDamageEvent;
    private Rigidbody rigidBody;
    private Vector3 currentVelocity;
    private float activeTimer = 10f;
    private float timerCheck = 0f;
    public ProjectileUser projectileUser;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //TogglePause(TimeManager.Instance.TimePaused);
    }
    /// <summary>
    /// Handle velocity on Pause
    /// </summary>
    /// <param name="pause"></param>
    public void TogglePause(bool pause)
    {
        if (pause)
        {
            currentVelocity = rigidBody.velocity;
            rigidBody.velocity = Vector3.zero; 
        }
        else
        {
            rigidBody.velocity = currentVelocity;
        }
    }
    /// <summary>
    /// Setup Damage Values
    /// </summary>
    /// <param name="_damage"></param>
    public void SetupDamage(float _damage) => damageStat = _damage;
  private void OnTriggerEnter(Collider other)
    {
        switch (projectileUser)
        {
            case ProjectileUser.Player: CallEnemyHit(other); break;
            case ProjectileUser.Enemy: CallPlayerHit(other); break;
            default: break;
        }
       
    }
    private void CallEnemyHit(Collider other)
    {
        if (other.gameObject.layer == GameConstantsClass.ENEMY_LAYER)
        {
            EnemyBaseClass newEnemy = other.gameObject.transform.parent.transform.GetComponent<EnemyBaseClass>();
            if (newEnemy != null)
            {
                newEnemy.enemyHealthComponent.DamageHealth(damageStat);
            }

            CallDestroy();
        }
    }
    private void CallPlayerHit(Collider other)
    {
        if (other.gameObject.layer == GameConstantsClass.PLAYER_LAYER)
        {
            if (other.gameObject.CompareTag(GameConstantsClass.PLAYER_TAG))
            {
                PlayerControllerScript player = other.gameObject.GetComponentInChildren<PlayerControllerScript>();
                if (player != null)
                {
                    player.DamagePlayer(damageStat,true);
                }

            }

            CallDestroy();
        }
    }
    public void CallDestroy()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        LifetimeProcess();
    }
    /// <summary>
    /// Resets activity Timer Check
    /// </summary>
    public void ResetActivityTimer() => timerCheck = 0;

    private void LifetimeProcess()
    {
        if (timerCheck > activeTimer)
        {
            ResetActivityTimer();
            CallDestroy();

        }
        timerCheck += Time.deltaTime;
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("I hit player on Collision!");
            Destroy(gameObject);
        }
    }*/

    [Serializable]
    public enum ProjectileUser
    {
        Player=0,
        Enemy=1
    }
}
