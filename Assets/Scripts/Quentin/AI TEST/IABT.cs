using System;
using System.Collections.Generic;
using BehaviorTree;

public class IABT : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;
    public static float fovRange = 6f;
    public static float range = 4f;
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckPlayerInRange(transform),
                new GoInRange(transform),
            }),
            new Patrol(transform, waypoints),
        });
        return root;
    }

}
