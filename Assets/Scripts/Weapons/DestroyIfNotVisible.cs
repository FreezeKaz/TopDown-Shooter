using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfNotVisible : MonoBehaviour
{
    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
