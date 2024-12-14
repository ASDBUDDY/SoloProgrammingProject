using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    /// Maximum Armour of Character
    /// </summary>
    public float MaxArmour { get; private set; }

    /// <summary>
    /// Current Armour of Character
    /// </summary>
    public float CurrentArmour { get; private set; }

    /// <summary>
    /// Constructor for Health Component Setup
    /// </summary>
    /// <param name="maxHealth"></param>
    public HealthComponent(float maxHealth) {
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
        MaxArmour = 25f;
        CurrentArmour = 0;
    }

    /// <summary>
    /// Function to Deal Damage to Health
    /// </summary>
    /// <param name="damage"></param>
    public void DamageHealth(float damage)
    {
        if (CurrentHealth > 0)
        {
            if (CurrentArmour > 0) { 
                
                CurrentArmour -= damage;

                if (CurrentArmour < 0)
                {

                    damage = Mathf.Abs(CurrentArmour);
                    CurrentArmour = 0;


                }
                else
                    return;
            }


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

    /// <summary>
    /// Function to Give Armour to Player
    /// </summary>
    /// <param name="armour"></param>
    public void GiveArmour(float armour)
    {
        MaxArmour = armour;
        CurrentArmour = armour;
    }

}
