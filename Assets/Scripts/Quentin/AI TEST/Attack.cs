using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class Attack : Node
{
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        state = NodeState.RUNNING;
        return state;
    }

}
