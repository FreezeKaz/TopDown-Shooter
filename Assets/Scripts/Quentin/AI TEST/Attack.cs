using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class Attack : Node
{
    private Transform _transform;
    private Rigidbody2D _rb;

    public float attackTime = 0.2f;
    public float attackCounter = 0f;

    public GameObject bulletPrefab;

    public EnemyManager _enemyManager;

    public Attack(GameObject gameObject)
    {
        _transform = gameObject.transform;
        _enemyManager = _transform.GetComponent<EnemyManager>(); 
        _rb = _transform.GetComponent<Rigidbody2D>();
    }

    public override NodeState Evaluate()
    {

        Transform target = (Transform)GetData("target");
        if (Vector3.Distance(_transform.position, target.position) <= IABT.range)
        {
            Vector2 vector2 = target.position;
            _rb.transform.up = vector2 - new Vector2(_rb.transform.position.x, _rb.transform.position.y);
            _enemyManager.Actions.gameObject.GetComponent<Shooting>().StartShooting();
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.RUNNING;
        return state;
    }

}
