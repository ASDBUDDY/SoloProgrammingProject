using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager Instance;

    /// <summary>
    /// List of Pooled Enemy Objects
    /// </summary>
    private List<EnemyBaseClass> pooledObjectList = new List<EnemyBaseClass>();

    /// <summary>
    /// List of Droped Enemy objects
    /// </summary>
  
   

    /// <summary>
    /// Limit of Pooling Each Type of object
    /// </summary>
    private int poolingLimit = 30;

    /// <summary>
    /// Base Classes to Pool enemies
    /// </summary>
    [SerializeField] private List<EnemyBaseClass> poolingObjects;

    /// <summary>
    /// Base classes to Pool drops
    /// </summary>
   

   

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        SetupObjectPool();
    
    }


    /// <summary>
    /// Instantiates Enemy Pooling Objects at the start of the level
    /// </summary>
    private void SetupObjectPool()
    {
        foreach (EnemyBaseClass enemy in pooledObjectList)
        {
            Destroy(enemy.gameObject);
        }
        pooledObjectList.Clear();
        foreach (var enemy in poolingObjects)
        {
            for (int i = 0; i < poolingLimit; i++)
            {

                EnemyBaseClass obj = Instantiate(enemy);
                obj.gameObject.SetActive(false);
                pooledObjectList.Add(obj);
            }
        }
    
        
    }
    
    

    /// <summary>
    /// Checks for all Enemy are inactive/dead
    /// </summary>
    /// <returns> Presence of Active Enemy </returns>
    public bool GetEnemyActive()
    {
       foreach(EnemyBaseClass enemy in pooledObjectList)
        {
            if (enemy.gameObject.activeInHierarchy)
                return true;
        }
        return false;
    }
    /// <summary>
    /// Check and return available Pooled Enemy Object of the type specified.
    /// </summary>
    /// <param name="animalType"></param>
    /// <returns></returns>
    public EnemyBaseClass GetPooledObject(EnemyStatsClass.EnemyTypes enemyTypes)
    {
        for(int i = 0;i < pooledObjectList.Count; i++)
        {
            if (!pooledObjectList[i].gameObject.activeInHierarchy && pooledObjectList[i].enemyTypes == enemyTypes)
            {
                return pooledObjectList[i];
            }
        }
        return null;
    }
    /// <summary>
    /// Check and return available Drop Pooled Object of the type specified
    /// </summary>
    /// <param name="dropType"></param>
    /// <returns></returns>
   

}
