using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSectionMonitor : MonoBehaviour
{
    public static LevelSectionMonitor Instance;
    public List<SubSectionDetection> LevelSubsections;
    public SubSectionDetection ActiveSubSection;
   public GameObject GetActiveSection() => ActiveSubSection.gameObject;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    

    private void CheckForActiveSection()
    {
        foreach (SubSectionDetection section in LevelSubsections)
        {
            if (section.isPlayerPresent)
            {
                ActiveSubSection = section;
                break;
            }
        }
    }

    public void FixedUpdate()
    {
        CheckForActiveSection();
    }
   
    public SubSectionDetection GetRandomSubsection() => LevelSubsections[Random.Range(0, LevelSubsections.Count)];
   

}
