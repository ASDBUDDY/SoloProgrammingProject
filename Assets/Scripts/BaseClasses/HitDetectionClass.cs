using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionClass : MonoBehaviour
{
    private float damageStat;
    //[SerializeField] FloatEventChannelSO playerDamageEvent;
    
      public void SetupDamage(float _damage) => damageStat = _damage;

      private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag(GameConstantsClass.PLAYER_TAG))
        {
           PlayerControllerScript player = other.gameObject.GetComponentInChildren<PlayerControllerScript>();
            if (player != null)
            {
                player.DamagePlayer(damageStat);
            }
            
        }
      }

}
