using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    
    public static ScenesManager Instance { get; private set; }

    private string sceneName;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

    }

    public void SetScene(string scene)
    {
        sceneName = scene ;
        ChangeScene();
    }

    private void Update()
    {
        if (GameManager.Instance.Lost)
        {
            sceneName = "GameOver";
            SceneManager.LoadScene(sceneName);
        }
    }
}
