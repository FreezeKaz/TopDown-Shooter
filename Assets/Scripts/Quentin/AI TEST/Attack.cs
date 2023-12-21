using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class Attack : ActionNode
{
    public float attackTime = 0.2f;
    public float attackCounter = 0f;

    public GameObject bulletPrefab;

    public override void Init()
    {
        type = NodeType.TASK;
    }

    public Attack(GameObject gameObject)
    {
        transform = gameObject.transform;
    }

    public override NodeState Evaluate(BTApp app)
    {
        Transform target = (Transform)GetData(GOType.TARGET);
        if (Vector3.Distance(app.transform.position, target.position) <= BTApp.range)
        {
            app.enemyManager.Actions.gameObject.GetComponent<Shooting>().StartShooting();
            Vector2 vector2 = target.position;
            app.Rb.transform.up = vector2 - new Vector2(app.Rb.transform.position.x, app.Rb.transform.position.y);
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.RUNNING;
        return state;
    }

}
