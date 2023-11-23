using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> levelPrefabs;
    public GameObject gameplayCanvas;
    public int currentLevelIndex = -1;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this.gameObject); }
        else { Instance= this; }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (currentLevelIndex != -1)
            InstantiateLevel();
        Debug.Log("Index at start: " + currentLevelIndex.ToString());
    }

    public void StartLevelInstantiating()
    {
        if (currentLevelIndex != -1)
            InstantiateLevel();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int index)
    {
        currentLevelIndex = index;
        Debug.Log("Index at load: " + index.ToString());
        SceneManager.LoadScene("Gameplay");
    }

    private void InstantiateLevel()
    {
        GameObject g = Instantiate(levelPrefabs[currentLevelIndex]);
        g.transform.position = Vector3.zero;
        Instantiate(gameplayCanvas);
        currentLevelIndex = -1;
        Debug.Log("Object intantiated");
    }
}
