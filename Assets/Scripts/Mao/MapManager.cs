using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance => _instance;

    public GameObject MapObject;
    public Map map;
    [SerializeField] private PlayerManager _player;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this);
    }

    public void ChangeMap(GameObject newMap)
    {
        SoundFXManager.instance.StopBGM();
        string[] mapName = newMap.name.Split("#");
        switch (mapName[1])
        {
            case "0":
                SoundFXManager.instance.PlayBGM(0);
                break;
            case "1":
                SoundFXManager.instance.PlayBGM(1);
                break;
        }
        if (MapObject != null) Destroy(MapObject);
        Debug.Log("Changing map");
        MapObject = Instantiate(newMap, transform.position, Quaternion.identity);
        MapObject.transform.parent = gameObject.transform;
        map = MapObject.GetComponent<Map>();
        _player.SetPos(map.playerSpawnPoint.transform);
    }
}
