using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckPlayerInRange : Node
{
    private static int _playerLayer = 1 << 6;

    float minDepth = -Mathf.Infinity;
    float maxDepth = Mathf.Infinity;

    private void Awake()
    {
        type = NodeType.TASK;
    }

    public CheckPlayerInRange(GameObject gameObject) : base()
    {
        transform = gameObject.transform;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("Je suis en CheckInRange sale fdp");
        //Debug.Log(GetData("target"));

        object t = GetData("target");
        if (t == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, BTApp.fovRange, _playerLayer, minDepth, maxDepth);

            //Debug.Log(_playerLayer);
            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
