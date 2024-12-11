using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSectionDetection : MonoBehaviour
{
    private bool playerPresent = false;

    public bool isPlayerPresent => playerPresent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameConstantsClass.PLAYER_TAG))
        {
            playerPresent = true;  
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(GameConstantsClass.PLAYER_TAG))
        {
            playerPresent = false;


        }
    }

}
