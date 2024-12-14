using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BaseClasses.EnemyStatsClass;

public class PumpkinRangedEnemyScript : EnemyBaseClass
{
    public GameObject ProjectileClass;

    private new void Awake()
    {
        base.Awake();
        enemyTypes = EnemyStatsClass.EnemyTypes.Ranged;
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
    public override void AttackFunction()
    {
        base.AttackFunction();

        
        Vector3 spawnPos = this.transform.position + this.transform.forward * 0.5f;
        GameObject bullet = Instantiate(ProjectileClass, spawnPos, this.transform.rotation);
        var projectile = bullet.GetComponent<ProjectileBaseClass>();
        projectile.SetupDamage(EnemyStats.enemyAttackPower);
        var rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) { rb.AddForce(transform.forward * 125f); }

    }

}
