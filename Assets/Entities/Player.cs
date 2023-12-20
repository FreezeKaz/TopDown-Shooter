using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Entity _player;
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.RegisterPlayer(_player);
    }

}
