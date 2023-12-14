using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelMenu;
    public GameObject settingsMenu;
    public List<Button> buttons = new List<Button>();
    private GameManager gameManager;

    private void Awake()
    {
        Time.timeScale = 1;

        for (int i= 0; i < GameManager.Instance.levelUnlockStatus.Count; i++)
        {
            buttons[i].GetComponent<LevelButton>().unlockedStatus = true;
            buttons[i].GetComponent<LevelButton>().trophy = GameManager.Instance.levelUnlockStatus[i];
        }
    }

    private void Start()
    {
        //Set Level Select -buttons to load levels with different indexes
        gameManager = GameManager.Instance;
        for(int i = 0; i < buttons.Count; i++)
        {
            int c = i;
            buttons[c].onClick.AddListener(() => gameManager.LoadLevel(c));
        }
    }

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

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
