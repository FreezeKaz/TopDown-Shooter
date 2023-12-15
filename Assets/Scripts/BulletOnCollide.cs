using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOnCollide : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}
