using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public List<GameObject> EnemyCountRingDefault;
    public List<GameObject> EnemyCountRingMedium;
    public List<GameObject> EnemyCountRingHigh;

    public float RingMediumDistance = 20f;
    public float RingHighDistance = 10f;

    public float TimeBetweenChecks = 1f;
    private float checkTimer = 0f;

    private void Start()
    {
        EnemyCountRingDefault = new List<GameObject>();
        EnemyCountRingMedium = new List<GameObject>();  
        EnemyCountRingHigh = new List<GameObject>();
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
    private void LateUpdate()
    {
        if (checkTimer > TimeBetweenChecks)
        {
            CheckForEnemies();
            checkTimer = 0f;
        }
        else
        {
            checkTimer += Time.deltaTime;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"I AM HIT by {other.gameObject.name}");
        if ((EnemyCountRingDefault.Contains(other.gameObject)))
            return;

        EnemyCountRingDefault.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)  
    {
        if (EnemyCountRingDefault.Remove(other.gameObject))
            return;
        if (EnemyCountRingMedium.Remove(other.gameObject))
            return;

        EnemyCountRingHigh.Remove(other.gameObject);
    }
  
}
