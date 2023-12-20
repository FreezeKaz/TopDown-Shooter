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
        Debug.Log("Je suis en CheckInRange");
        //Debug.Log(GetData("target"));

        object t = GetData(GOType.TARGET);
        if (GetData(GOType.TARGET) == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, BTApp.fovRange, _playerLayer, minDepth, maxDepth);

            Debug.Log(_playerLayer);
            if (colliders.Length > 0)
            {
                SetData(GOType.TARGET, colliders[0].transform);
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        else
        {
            state = NodeState.SUCCESS;
            Debug.Log("target != null");
            Debug.Log(t);
            return state;
        }
      
    }
}
