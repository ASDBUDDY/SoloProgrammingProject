using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyAttackDetection : MonoBehaviour
{
    public FriendlyControllerScript AllyController;
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.layer == GameConstantsClass.ENEMY_LAYER)
        {
         
            EnemyBaseClass newEnemy = other.gameObject.transform.parent.transform.GetComponent<EnemyBaseClass>();
            if (newEnemy != null) {
                newEnemy.enemyHealthComponent.DamageHealth(AllyController.MeleeDamage);
            }
        }
    }

    public void AnimOver() { 
        
        this.gameObject.SetActive(false); 
        AllyController.AttackOver();
    }
}
