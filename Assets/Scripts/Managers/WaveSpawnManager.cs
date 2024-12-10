using BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveSpawnManager : MonoBehaviour
{

    public static WaveSpawnManager Instance;
    /// <summary>
    /// PLayer obj to Set Target
    /// </summary>
    public GameObject PlayerObj;
    /// <summary>
    /// Current Wave ID
    /// </summary>
    [SerializeField] int WaveNumber =0;
    /// <summary>
    /// Break time Between Each Wave
    /// </summary>
    [SerializeField] float ResetWaveTime = 5f;
    /// <summary>
    /// Internal Timer for Resetting
    /// </summary>
    float resetTimer = 0;

    /// <summary>
    /// Internal Storage for Current Wave Timer
    /// </summary>
    [SerializeField] WaveData CurrentWaveData;



    public UnityEvent NewWave;

    public WaveSpawningDataListSO WaveSpawningDataListSO;
    private bool isWaveOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        SetupWaveData();
    }


    private void Update()
    {
        SpawnMechanism();
        CheckForResetWave();
    }
    /// <summary>
    /// Returns Current Wave Index
    /// </summary>
    public int GetCurrentWave => WaveNumber;
    /// <summary>
    /// Procures data from SO and stores for Current Wave
    /// </summary>
    private void SetupWaveData()
    {
        if (WaveNumber >= WaveSpawningDataListSO.DataList.Count)
            WaveNumber = WaveSpawningDataListSO.DataList.Count - 1;
        WaveData waveData = WaveSpawningDataListSO.GetWaveData(WaveNumber);
        CurrentWaveData = new WaveData();
        CurrentWaveData.WaveID = waveData.WaveID;
        CurrentWaveData.EnemyWaveList = new List<WaveEnemyData>();
        foreach(var data in waveData.EnemyWaveList)
        {
           WaveEnemyData newWaveEnemy = new WaveEnemyData();
            newWaveEnemy.enemyTypes = data.enemyTypes;
            newWaveEnemy.EnemyCount = data.EnemyCount;
            newWaveEnemy.SpawnTimer = data.SpawnTimer;
            newWaveEnemy.CurrentSpawnTimer = data.CurrentSpawnTimer;    
            newWaveEnemy.SpawnQuantity = data.SpawnQuantity;  
            
            CurrentWaveData.EnemyWaveList.Add(newWaveEnemy);    

        }
    }
    /// <summary>
    /// Spawns Enemies as per current wave Data
    /// </summary>
    public void SpawnMechanism()
    {
        if (CurrentWaveData != null)
        {
            foreach (var enemy in CurrentWaveData.EnemyWaveList)// Iterates through enemy list in current wave data
            {

                if (enemy.EnemyCount<enemy.SpawnQuantity)
                {
                    if (enemy.CurrentSpawnTimer > enemy.SpawnTimer)// if Enemy's spawn limit hasn't reached and the time between spawning has exceeded the limit
                    {

                        
                        if(SpawnSet(ObjectPoolingManager.Instance.GetPooledObject(enemy.enemyTypes), EnemySpawnManager.Instance.GetSpawnLocation()))// Spawn Enemy type checked above
                        {
                            enemy.EnemyCount++;
                            enemy.CurrentSpawnTimer = 0;
                        }

                    }
                    else
                        enemy.CurrentSpawnTimer += Time.deltaTime;//increment Spawn Timer

                }
            }
            
            
        }
    }

    /// <summary>
    /// Checks if Enemy Active and Sets Wave Over Accordingly
    /// </summary>
    private void CheckEnemyActivity()
    {
        if (!ObjectPoolingManager.Instance.GetEnemyActive())
            isWaveOver = true;
        else
            return;

        foreach (var enemy in CurrentWaveData.EnemyWaveList)
        {
            if (enemy.EnemyCount < enemy.SpawnQuantity)
                isWaveOver = false;
        }

    }
    /// <summary>
    /// Reset the Wave if Wave is Over
    /// </summary>
    public void CheckForResetWave()
    {
        CheckEnemyActivity();

        if (isWaveOver)
        {
            if (resetTimer > ResetWaveTime)
            {
                
                Debug.Log("RESETING WAVE");
                WaveNumber++;
                SetupWaveData();
                resetTimer = 0f;
                isWaveOver = false;

                NewWave.Invoke(); //Invokes an event for starting a new wave
            }
            else
                resetTimer += Time.deltaTime;
        }
    }

    /// <summary>
    /// Spawns Enemy based on Enemy Type and Spawn Location 
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="spawnLoc"></param>
    /// <returns>Returns if Spawning successful or not</returns>
    private bool SpawnSet(EnemyBaseClass enemy, Transform spawnLoc)
    {
        if (enemy != null && spawnLoc != null)
        {

            
            
            enemy.transform.position = spawnLoc.position;
            enemy.gameObject.SetActive(true);
            enemy.LifetimeSM.SetState(LifetimeStates.Spawning);
            enemy.SetTargetPlayer(PlayerObj);
           
            /*enemy.LifetimeSM.SetState(LifetimeStates.Spawning);*/
            return true;

        }
        return false;
    }

}
[Serializable]
public class WaveData
{
    /// <summary>
    /// Wave Number
    /// </summary>
    public int WaveID=0;
    public List<WaveEnemyData> EnemyWaveList;

}

[Serializable]
public class WaveEnemyData
{
    /// <summary>
    /// Animal Type
    /// </summary>
    public EnemyStatsClass.EnemyTypes enemyTypes;
    /// <summary>
    /// Enemies of this type to be spawned in this wave
    /// </summary>
    public int SpawnQuantity;
    /// <summary>
    /// Spawn Timer for specific Enemy Type
    /// </summary>
    public float SpawnTimer = 1f;
    /// <summary>
    /// Amount of enemies already spawned (only to be used in wave spawner)
    /// </summary>
    [HideInInspector] public int EnemyCount=0;
    /// <summary>
    /// Concurrent Spawn timer (only to be used in wave spawner)
    /// </summary>
    [HideInInspector] public float CurrentSpawnTimer=0;

}
