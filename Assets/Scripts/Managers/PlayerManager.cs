using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public Entity myEntityStats;

    public void SetPos(Transform transform)
    {
        gameObject.transform.position = transform.position;
    }
}
