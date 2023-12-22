using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public Entity Player { get; private set; }
    public int Wave { get; set; }
    private float DelayBeforeWave { get; set; }
    public int mapID { get; set; }
    public bool Lost { get; set; }
    public bool GameStart { get; set; }
    /*public List<Map> {get; private set;}*/ //Map change handler

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

    public void StartGame()
    {
        Wave = 0;
        PrepareNextWave(5f);
    }

    public void RegisterPlayer(Entity player)
    {
        Player = player;
    }
    public void PrepareNextWave(float timeBeforeWave)
    {
        DelayBeforeWave = timeBeforeWave;
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        float timer = DelayBeforeWave;

        while (timer > 0f)
        {
            // Update the timer display (you can replace this with updating a UI Text component)

            yield return null; // Wait for the next frame
            timer -= Time.deltaTime;
        }

        // When the timer reaches zero, start the wave
        WaveGenerator.Instance.StartWave();
    }

    public void HandleEnemyDefeat(Entity enemyDefeated)
    {
        Player.XP += (int)enemyDefeated.XPGiven;
        Debug.Log("I win " + enemyDefeated.XPGiven + "XP");
    }
    public void GameOver()
    {
        WaveGenerator.Instance.StopWaves();
        EnemyPoolManager.Instance.ResetPool();
    }
}
