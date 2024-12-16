using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIScript : MonoBehaviour
{
    public Slider ArmourSlider;

    public Slider HealthSlider;

    public TMP_Text ArmourText;

    public TMP_Text HealthText;

    public PlayerControllerScript playerController;

    public GameObject DeathScreen;


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
        if(playerController.healthComponent.CurrentHealth <= 0)
        {
            if(!DeathScreen.activeSelf)
                DeathScreen.SetActive(true);
            return;
        }
        ArmourSlider.value = playerController.healthComponent.CurrentArmour / playerController.healthComponent.MaxArmour;
        HealthSlider.value = playerController.healthComponent.CurrentHealth / playerController.healthComponent.MaxHealth;

        ArmourText.text = $"{playerController.healthComponent.CurrentArmour}/{playerController.healthComponent.MaxArmour}";
        HealthText.text = $"{playerController.healthComponent.CurrentHealth}/{playerController.healthComponent.MaxHealth}";
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
