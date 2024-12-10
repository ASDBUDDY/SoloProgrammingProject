using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent
{
    /// <summary>
    /// Maximum Health of the Character
    /// </summary>
   public float MaxHealth {  get; private set; }

    /// <summary>
    /// Current Health of the Character
    /// </summary>
    public float CurrentHealth { get; private set; }

    /// <summary>
    /// Constructor for Health Component Setup
    /// </summary>
    /// <param name="maxHealth"></param>
    public HealthComponent(float maxHealth) {
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
    }

    /// <summary>
    /// Function to Deal Damage to Health
    /// </summary>
    /// <param name="damage"></param>
    public void DamageHealth(float damage)
    {
        if (CurrentHealth > 0)
        {

            CurrentHealth -= damage;

            if (CurrentHealth < 0)
                CurrentHealth = 0;
        }
    }

    /// <summary>
    /// Function to Heal the Character's Current Health
    /// </summary>
    /// <param name="increase"></param>
    public void IncreaseHealth(float increase) {

        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth += increase;

            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
        }
    }

}
