using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOnCollide : MonoBehaviour
{
    [SerializeField] private BulletDamage _myDamage;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Entity _entity = collision.gameObject.transform.parent.GetComponent<EnemyManager>().myEntityStats;
            _entity.TakeDamage((int)_myDamage.damage);
        }
        else if (collision.gameObject.layer == 6)
        {
            Entity _entity = collision.gameObject.transform.parent.GetComponent<PlayerManager>().myEntityStats;
            _entity.TakeDamage((int)_myDamage.damage);
        }
             

        gameObject.SetActive(false);
    }
}
