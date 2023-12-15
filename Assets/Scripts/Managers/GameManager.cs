using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public int Wave { get; private set; }
    public int mapID { get; private set; }
    public bool Lost { get; private set; }
    /*public List<Map> {get; private set;}*/ //Map change handler

    private void Awake()
    {
        Wave = 0;
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }
}
