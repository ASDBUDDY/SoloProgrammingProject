using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/ DataList / EnemyStatsList")]
public class EnemyStatsListSO : ScriptableObject
{

    /// <summary>
    /// A Data list of Enemy Stat Classes, meant to function as base stats
    /// </summary>
    public List<EnemyStatsClass> DataList;

    public EnemyStatsClass GetStats(EnemyStatsClass.EnemyTypes _enemy)
    {
        foreach (EnemyStatsClass item in DataList)
        {
            if (item.enemyType == _enemy)
                return item;
        }
        return null;
    }

    
}
