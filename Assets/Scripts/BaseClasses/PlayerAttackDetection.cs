using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackDetection : MonoBehaviour
{
    public PlayerControllerScript PlayerController;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Who tf?{other.gameObject.name}");
        if (other.gameObject.layer == GameConstantsClass.ENEMY_LAYER)
        {
            Debug.Log("Who tf?");
            EnemyBaseClass newEnemy = other.gameObject.transform.parent.transform.GetComponent<EnemyBaseClass>();
            if (newEnemy != null) {
                newEnemy.enemyHealthComponent.DamageHealth(PlayerController.MeleeDamage);
            }
        }
    }
}
