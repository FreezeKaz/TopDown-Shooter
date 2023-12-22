using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneButton : MonoBehaviour
{

    [Scene] public string Scene;
    // Start is called before the first frame update
    public void ChangeScene()
    {
        ScenesManager.Instance.SetScene(Scene);
        if(Scene == "Game")
        {
            GameManager.Instance.StartGame();
        }
    }
    public void InitPool()
    {
        ObjectPoolManager.Instance.InitPool();
    }
}
