using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class GoInRange : Node
{
    private Transform _transform;
    private Rigidbody2D _rb;
    private EnemyManager _enemyManager;

    public GoInRange(GameObject gameObject)
    {
        _transform = gameObject.transform;
        _rb = _transform.GetComponent<Rigidbody2D>();
        _enemyManager = _transform.GetComponent<EnemyManager>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        if (Vector3.Distance(_transform.position, target.position) > IABT.range)
        {
            _enemyManager.Actions.gameObject.GetComponent<Shooting>().StopShooting();
            Vector2 vector2 = target.position;
            _rb.transform.up = vector2 - new Vector2(_rb.transform.position.x, _rb.transform.position.y);
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, IABT.speed * Time.deltaTime);
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
