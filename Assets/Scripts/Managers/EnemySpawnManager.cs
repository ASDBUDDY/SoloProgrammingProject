using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;
    /// <summary>
    /// Pre determined Spawn Points
    /// </summary>
    [SerializeField] List<GameObject> SpawnLocations = new List<GameObject>();
    /// <summary>
    /// Player Object Reference for Setup
    /// </summary>
    [SerializeField] GameObject PlayerObject;
    /// <summary>
    /// Maximum limits for Vicinity
    /// </summary>
    [SerializeField] float VicinityMax = 4f;
    /// <summary>
    /// Minimum limits for Vicinity
    /// </summary>
    [SerializeField] float VicinityMin = 2f;
    

    /// <summary>
    /// Resets all spawn points to off
    /// </summary>
    private void ResetSpawnPoints()
    {
        foreach (var spawnPoint in SpawnLocations)
        {
            spawnPoint.SetActive(false);
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ResetSpawnPoints();
    }
    private void Update()
    {
        CheckforPlayerVicinity();
    }
     
    /// <summary>
    /// Checks if Player is in the Vicinity Limits
    /// </summary>
    private void CheckforPlayerVicinity()
    {
        if (PlayerObject != null)
        {
            foreach (var spawnPoint in SpawnLocations)
            {

                float distanceFromPoint = Vector3.Distance(spawnPoint.transform.position, PlayerObject.transform.position);

                spawnPoint.SetActive(distanceFromPoint <= VicinityMax && distanceFromPoint>=VicinityMin ? true : false);
            }

        }

    }
    

   
    /// <summary>
    /// Retrieves available spawn location
    /// </summary>
    /// <returns> Transform of the spawn point</returns>
    public Transform GetSpawnLocation()
    {
        List<GameObject> activeLocations = new List<GameObject>();

        foreach (GameObject location in SpawnLocations)
        {
            if (location.activeInHierarchy)
                activeLocations.Add(location);
        }

        if (activeLocations.Count == 0)
        {
            Debug.LogError($"NO SPAWN LOCATION AVAILABLE");
            return null;
        }

        int randomID = Random.Range(0, activeLocations.Count);

        return(activeLocations[randomID].transform);
    }
}
