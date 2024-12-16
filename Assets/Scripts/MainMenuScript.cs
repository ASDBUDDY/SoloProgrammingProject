using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private bool PressOnce = false;
  


    public void StartGame()
    {
        if (!PressOnce)
        {
            PressOnce = true;
            SceneManager.LoadSceneAsync(1);
            SceneManager.UnloadSceneAsync(0);
        }
    }
}
