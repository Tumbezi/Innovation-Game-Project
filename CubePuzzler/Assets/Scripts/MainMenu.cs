using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelMenu;
    public GameObject settingsMenu;

    public void Play()
    {
        levelMenu.SetActive(true);
        mainMenu.SetActive(false);
        //SceneManager.LoadSceneAsync("Stage1");
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }


    public void BackSettings()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void BackLevel()
    {
        levelMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
