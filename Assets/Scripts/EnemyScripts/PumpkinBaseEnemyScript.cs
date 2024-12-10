using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BaseClasses.EnemyStatsClass;

public class PumpkinBaseEnemyScript : EnemyBaseClass
{

    private new void Awake()
    {
        base.Awake();
        enemyTypes = EnemyStatsClass.EnemyTypes.Melee;
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
}
