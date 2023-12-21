using UnityEngine;
using BehaviorTree;

public class CheckPlayerInRange : Node
{
    private static int _playerLayer = 1 << 6;

    float minDepth = -Mathf.Infinity;
    float maxDepth = Mathf.Infinity;

    public override void Init()
    {
        type = NodeType.TASK;
    }

    public CheckPlayerInRange(GameObject gameObject) : base()
    {
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("Je suis en CheckInRange");
        if (GetData(GOType.TARGET) == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, BTApp.fovRange, _playerLayer, minDepth, maxDepth);

            //Debug.Log(_playerLayer);
            if (colliders.Length > 0)
            {
                //Debug.Log(colliders[0].transform.parent);
                SetData(GOType.TARGET, colliders[0].transform.parent);
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
