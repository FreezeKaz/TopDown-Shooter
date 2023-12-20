using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class GoInRange : Node
{
    private Rigidbody2D _rb;
    private EnemyManager _enemyManager;
    bool one = true;

    private void Awake()
    {
        type = NodeType.TASK;
    }

    public GoInRange(GameObject gameObject)
    {
        transform = gameObject.transform;
        _rb = transform.GetComponent<Rigidbody2D>();
        _enemyManager = transform.GetComponent<EnemyManager>();
    }

    public override NodeState Evaluate()
    {
        if(one)
        {
            _rb = transform.GetComponent<Rigidbody2D>();
            _enemyManager = transform.GetComponent<EnemyManager>();
            one = false;
        }
        Debug.Log("Je suis en GoInRange");
        Transform target = (Transform)DataContext["target"];
        //Debug.Log(Vector3.Distance(transform.position, target.position));

        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        if (Vector3.Distance(transform.position, target.position) > BTApp.range)
        {
            Debug.Log("uwu");
            _enemyManager.Actions.gameObject.GetComponent<Shooting>().StopShooting();
            Vector2 vector2 = target.position;
            _rb.transform.up = vector2 - new Vector2(_rb.transform.position.x, _rb.transform.position.y);
            transform.position = Vector3.MoveTowards(transform.position, target.position, BTApp.speed * Time.deltaTime);
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
