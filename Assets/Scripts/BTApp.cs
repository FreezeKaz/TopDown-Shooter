using UnityEngine;
using BehaviorTree;

public class BTApp : BehaviorTree.Tree
{
    [SerializeField] private BTSave _save;
    public UnityEngine.Transform[] waypoints;
    protected override Node SetupTree()
    {
        Node root = _save.root;

        return root;
    }
}
