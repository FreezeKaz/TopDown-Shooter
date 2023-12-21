using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class GoInRange : Node
{
    public override void Init()
    {
        type = NodeType.TASK;
    }

    public GoInRange(GameObject gameObject)
    {
    }

    public override NodeState Evaluate(BTApp app)
    {            

        Transform target = (Transform)GetData(GOType.TARGET);
        //Debug.Log(Vector3.Distance(transform.position, target.position));

        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        if (Vector3.Distance(app.transform.position, target.position) > app.Range)
        {
            app.enemyManager.Actions.gameObject.GetComponent<Shooting>().StopShooting();
            Vector2 vector2 = target.position;
            app.Rb.transform.up = vector2 - new Vector2(app.Rb.transform.position.x, app.Rb.transform.position.y);
            app.transform.position = Vector3.MoveTowards(app.transform.position, target.position, app.Speed * Time.deltaTime);
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
