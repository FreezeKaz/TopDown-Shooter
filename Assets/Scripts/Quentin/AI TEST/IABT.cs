using System;
using System.Collections.Generic;
using BehaviorTree;

public class IABT : Tree
{
    public UnityEngine.Transform[] waypoints;


    public static float speed = 2f;
    public static float fovRange = 15f;
    public static float range = 15f;
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckPlayerInRange(gameObject),
                new GoInRange(gameObject),
                new Attack(gameObject),
            }),
            new Patrol(gameObject, waypoints),
        });
        return root;
    }
}
