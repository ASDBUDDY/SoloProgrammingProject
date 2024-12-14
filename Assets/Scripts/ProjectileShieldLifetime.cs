using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShieldLifetime : MonoBehaviour
{
    private float LifetimePeriod = 10f;

    private float LifetimeTimer = 0f;
  
    void Update()
    {
        if (LifetimeTimer > LifetimePeriod)
        {
            TaskManager.Instance.ExistingProjectileShield = false;
            Destroy(this.gameObject);
        }

        LifetimeTimer += Time.deltaTime;
    }
}
