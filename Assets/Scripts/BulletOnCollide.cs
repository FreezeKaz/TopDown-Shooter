using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOnCollide : MonoBehaviour
{
    public void Awake()
    {
        Physics2D.IgnoreLayerCollision(7, 6);
        Physics2D.IgnoreLayerCollision(7, 8);
    }
    [SerializeField] Bullet bullet;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);

    }
}
