using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class ListWrapper
{
    public List<float> InnerList;
    public float this[int key]
    {
        get
        {
            return InnerList[key];
        }
        set
        {
            InnerList[key] = value;
        }
    }
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> levelPrefabs;
    public GameObject gameplayCanvas;
    public int currentLevelIndex = -1;

    public List<int> levelUnlockStatus = new List<int>() { 0 };
    public List<ListWrapper> levelTrophyTimes = new List<ListWrapper>();

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this.gameObject); }
        else { Instance= this; }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    public void StartLevelInstantiating()
    {
        if (currentLevelIndex != -1)
            InstantiateLevel();
    }



    /* Update is called once per frame
    void Update()
    {
        
    }
    */
    public void LoadLevel(int index)
    {
        currentLevelIndex = index;
        Debug.Log("Index at load: " + index.ToString());
        SceneManager.LoadScene("Gameplay");
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }

    private void InstantiateLevel()
    {
        GameObject g = Instantiate(levelPrefabs[currentLevelIndex]);
        g.transform.position = Vector3.zero;
        Instantiate(gameplayCanvas);
    }

    public void SetLevelTrophy(float time)
    {
        if (time <= levelTrophyTimes[currentLevelIndex][0])
            levelUnlockStatus[currentLevelIndex] = 3;
        else if (time >= levelTrophyTimes[currentLevelIndex][0] && time <= levelTrophyTimes[currentLevelIndex][2])
            levelUnlockStatus[currentLevelIndex] = 2;
        else if (time >= levelTrophyTimes[currentLevelIndex][2])
            levelUnlockStatus[currentLevelIndex] = 1;

        if (currentLevelIndex + 1 == levelUnlockStatus.Count)
            levelUnlockStatus.Add(0);
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("LevelListCount", levelUnlockStatus.Count);
        for (int i = 0; i < levelUnlockStatus.Count; i++)
        {
            PlayerPrefs.SetInt("Level" + i, levelUnlockStatus[i]);
        }
        Debug.Log(levelUnlockStatus.Count);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {

        if (PlayerPrefs.HasKey("LevelListCount"))
        {
            levelUnlockStatus.Clear();
            for (int i = 0; i < PlayerPrefs.GetInt("LevelListCount"); i++)
            {
                levelUnlockStatus.Add(PlayerPrefs.GetInt("Level" + i));
            }
        }
    }
}
