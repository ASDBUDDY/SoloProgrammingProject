using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackDetection : MonoBehaviour
{
    public PlayerControllerScript PlayerController;
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.layer == GameConstantsClass.ENEMY_LAYER)
        {
         
            EnemyBaseClass newEnemy = other.gameObject.transform.parent.transform.GetComponent<EnemyBaseClass>();
            if (newEnemy != null) {
                newEnemy.enemyHealthComponent.DamageHealth(PlayerController.MeleeDamage);
            }
        }
    }
}
