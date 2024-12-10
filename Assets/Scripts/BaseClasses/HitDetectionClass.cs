using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionClass : MonoBehaviour
{
    private float damageStat;
    //[SerializeField] FloatEventChannelSO playerDamageEvent;
    
      public void SetupDamage(float _damage) => damageStat = _damage;

      private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log($"I hit player! {damageStat}");
            //playerDamageEvent.RaiseEvent(damageStat);
            
        }
      }

}
