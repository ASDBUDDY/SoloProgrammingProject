using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public static EnemyDetector Instance;

    public List<GameObject> EnemyCountRingDefault;
    public List<GameObject> EnemyCountRingMedium;
    public List<GameObject> EnemyCountRingHigh;

    public float RingMediumDistance = 20f;
    public float RingHighDistance = 10f;

    public float TimeBetweenChecks = 1f;
    private float checkTimer = 0f;

    public IntensityCriteria DefaultRingIntensity;
    public IntensityCriteria MediumRingIntensity;
    public IntensityCriteria HighRingIntensity;

    public int RangedEnemyPresent = 0;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        EnemyCountRingDefault = new List<GameObject>();
        EnemyCountRingMedium = new List<GameObject>();  
        EnemyCountRingHigh = new List<GameObject>();
    }

    public void RemoveEnemyOnDeath(GameObject obj)
    {
        if (EnemyCountRingDefault.Contains(obj))
        {
            EnemyCountRingDefault.Remove(obj);
            return;
        }
        if(EnemyCountRingMedium.Contains(obj))
        {
            EnemyCountRingMedium.Remove(obj);
            return;
        }
        if (EnemyCountRingHigh.Contains(obj))
        {
            EnemyCountRingHigh.Remove(obj);
            return;
        }
    }
    private void CheckForEnemies()
    {
        if(EnemyCountRingDefault.Count > 0)
        foreach(GameObject enemy in EnemyCountRingDefault.ToList())
        {
            if(Vector3.Distance( this.gameObject.transform.position,enemy.gameObject.transform.position ) <= RingMediumDistance)
            {
                EnemyCountRingMedium.Add(enemy);
                EnemyCountRingDefault.Remove(enemy);
            }
        }
        if (EnemyCountRingMedium.Count > 0)
            foreach (var enemy in EnemyCountRingMedium.ToList())
        {
            if (Vector3.Distance(this.gameObject.transform.position, enemy.gameObject.transform.position) > RingMediumDistance)
            {
                EnemyCountRingDefault.Add(enemy);
                EnemyCountRingMedium.Remove(enemy);
            }
            else if (Vector3.Distance(this.gameObject.transform.position, enemy.gameObject.transform.position) <= RingHighDistance)
            {
                EnemyCountRingHigh.Add(enemy);
                EnemyCountRingMedium.Remove(enemy);
            }
        }
        if (EnemyCountRingHigh.Count > 0)
            foreach (var enemy in EnemyCountRingHigh.ToList())
        {
            if (Vector3.Distance(this.gameObject.transform.position, enemy.gameObject.transform.position) > RingHighDistance)
            {
                EnemyCountRingMedium.Add(enemy);
                EnemyCountRingHigh.Remove(enemy);
            }
        }

    }

    private IntensityCriteria SetIntensityValues(int intensity)
    {
       

        if (intensity > 20)
            return IntensityCriteria.High;
        else if (intensity > 10)
            return IntensityCriteria.Medium;
        else if (intensity > 5)
            return IntensityCriteria.Low;
        else
            return IntensityCriteria.Default;



    }
    private void LateUpdate()
    {
        if (checkTimer > TimeBetweenChecks)
        {
            CheckForEnemies();
            checkTimer = 0f;
            DefaultRingIntensity = SetIntensityValues(EnemyCountRingDefault.Count);
            MediumRingIntensity = SetIntensityValues(EnemyCountRingMedium.Count);
            HighRingIntensity = SetIntensityValues(EnemyCountRingHigh.Count);
        }
        else
        {
            checkTimer += Time.deltaTime;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == GameConstantsClass.ENEMY_LAYER )
        {
            //Debug.Log($"I AM HIT by {other.gameObject.name}");
            if ((EnemyCountRingDefault.Contains(other.gameObject)))
                return;

            EnemyCountRingDefault.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)  
    {
        if (other.gameObject.layer == GameConstantsClass.ENEMY_LAYER)
        {
            if (EnemyCountRingDefault.Remove(other.gameObject))
                return;
            if (EnemyCountRingMedium.Remove(other.gameObject))
                return;

            EnemyCountRingHigh.Remove(other.gameObject);
        }
    }
  
}
[Serializable]

public enum IntensityCriteria
{
    Default,
    Low,
    Medium,
    High,
}
