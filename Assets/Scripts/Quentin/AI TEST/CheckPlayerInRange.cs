using UnityEngine;
using BehaviorTree;

public class CheckPlayerInRange : Node
{
    private static int _playerLayer = 1 << 6;

    float minDepth = -100f;
    float maxDepth = 100f;

    public override void Init()
    {
        type = NodeType.TASK;
    }

    public CheckPlayerInRange(GameObject gameObject) : base()
    {
    }

    public override NodeState Evaluate(BTApp app)
    {
        if (GetData(GOType.TARGET) == null)
        {
            Collider2D colliders = Physics2D.OverlapCircle(app.transform.position, app.FovRange, _playerLayer, minDepth, maxDepth);
            //RaycastHit2D[] colliders = Physics2D.CircleCastAll(app.transform.position, app.FovRange, app.Rb.transform.up, app.FovRange, _playerLayer);
            if (colliders != null)
            {
                Vector2 vector2 = colliders.transform.parent.position;
                app.Rb.transform.up = vector2 - new Vector2(app.Rb.transform.position.x, app.Rb.transform.position.y);
                SetData(GOType.TARGET, colliders.transform.parent);
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        else
        {
            state = NodeState.SUCCESS;
            return state;
        }
      
    }
}
