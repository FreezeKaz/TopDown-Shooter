using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    [Scene] public string Scene;

    public void ChangeScene()
    {
        SceneManager.LoadScene(Scene, LoadSceneMode.Additive);
    }
}
