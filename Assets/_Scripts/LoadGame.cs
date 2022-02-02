using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    public CanvasGroup startPanel;

    public static bool accelerometer;
    public Toggle accelerometerToggle;

    public void StartGame()
    { 
        startPanel.gameObject.SetActive(false);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ToggleAccelerometer()
    {
        if (accelerometerToggle.isOn)
        {   
            accelerometer = true;
            DontDestroyOnLoad(this);
        }
        else
        {
            accelerometer = false;
            DontDestroyOnLoad(this);

        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
