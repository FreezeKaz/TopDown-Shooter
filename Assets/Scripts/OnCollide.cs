using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollide : MonoBehaviour
{
    [SerializeField] private Entity entity;
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
    }
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {   
            Bullet bulletInfo = collision.gameObject.GetComponent<Bullet>();
            entity.TakeDamage((int)bulletInfo.Damage); 
        }
    }
}