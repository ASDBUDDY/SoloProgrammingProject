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
        /*if (other.gameObject.layer == GameConstantsClass.PLAYER_LAYER || other.gameObject.layer == GameConstantsClass.LEVEL_LAYER)
        {
            if (other.gameObject.CompareTag(GameConstantsClass.PLAYER_TAG))
            {
                //Debug.Log($"I hit player! {damageStat}");
                playerDamageEvent.RaiseEvent(damageStat);
                CallDestroy();
            }
            else
            {
                CallDestroy();
            }
        }*/
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
}
