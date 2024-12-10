using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/ DataList / WaveSpawningDataList")]
public class WaveSpawningDataListSO : ScriptableObject
{

    /// <summary>
    /// A List of data regarding all the waves and the enemies they contain
    /// </summary>
    public List<WaveData> DataList;

    public WaveData GetWaveData(int waveNumber)
    {
        foreach (WaveData item in DataList)
        {
            if (item.WaveID == waveNumber)
                return item;
        }
        return null;
    }

    
}
