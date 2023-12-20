using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class Attack : ActionNode
{
    private Rigidbody2D _rb;

    public float attackTime = 0.2f;
    public float attackCounter = 0f;

    public GameObject bulletPrefab;

    private EnemyManager _enemyManager;


    public override void Init()
    {
        type = NodeType.TASK;
        _enemyManager = transform.GetComponent<EnemyManager>();

    }

    public Attack(GameObject gameObject)
    {
        transform = gameObject.transform;
    }

    public override NodeState Evaluate()
    {

   

        Transform target = (Transform)GetData(GOType.TARGET);
        if (Vector3.Distance(transform.position, target.position) <= BTApp.range)
        {
            _enemyManager.Actions.gameObject.GetComponent<Shooting>().StartShooting();
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.RUNNING;
        return state;
    }

}
