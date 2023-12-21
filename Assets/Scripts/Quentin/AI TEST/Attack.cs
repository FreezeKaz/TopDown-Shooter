using UnityEngine;

using BehaviorTree;

public class Attack : Node
{
    public override void Init()
    {
        type = NodeType.TASK;
    }

    public Attack(GameObject gameObject)
    {

    }

    public override NodeState Evaluate(BTApp app)
    {
        Transform target = (Transform)GetData(GOType.TARGET);

        //Debug.Log(app.hit.transform);

        if(app.hit.collider != null) 
        {
            if (Vector3.Distance(app.transform.position, target.position) <= app.Range && app.hit.collider.CompareTag(LayerMask.LayerToName(6)))
            {
                app.enemyManager.Actions.gameObject.GetComponent<Shooting>().StartShooting();
                Vector2 vector2 = target.position;
                app.Rb.transform.up = vector2 - new Vector2(app.Rb.transform.position.x, app.Rb.transform.position.y);

                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                state = NodeState.RUNNING;
                return state;
            }
        }
        else
        {
            state = NodeState.RUNNING;
            return state;
        }

    }

}
