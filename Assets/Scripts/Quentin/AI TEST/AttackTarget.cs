using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class AttackTarget : Node
{
    private Transform _transform;

    public AttackTarget(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if(Vector3.Distance(_transform.position, target.position) > IABT.range)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, IABT.speed * Time.deltaTime);
        }

        state = NodeState.RUNNING;
        return state;
    }
}
