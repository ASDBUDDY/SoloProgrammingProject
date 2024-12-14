using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIScript : MonoBehaviour
{
    public Slider ArmourSlider;

    public Slider HealthSlider;

    public TMP_Text ArmourText;

    public TMP_Text HealthText;

    public PlayerControllerScript playerController;


    private void Start()
    {
        UpdateHealthHUD();


    }

    private void Update()
    {
        UpdateHealthHUD();
    }
    private void UpdateHealthHUD()
    {
        ArmourSlider.value = playerController.healthComponent.CurrentArmour / playerController.healthComponent.MaxArmour;
        HealthSlider.value = playerController.healthComponent.CurrentHealth / playerController.healthComponent.MaxHealth;

        ArmourText.text = $"{playerController.healthComponent.CurrentArmour}/{playerController.healthComponent.MaxArmour}";
        HealthText.text = $"{playerController.healthComponent.CurrentHealth}/{playerController.healthComponent.MaxHealth}";
    }
}
