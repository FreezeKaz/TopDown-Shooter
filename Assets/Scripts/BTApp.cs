using UnityEngine;
using BehaviorTree;
using UnityEditor;
using System;

public class BTApp : BehaviorTree.Tree
{
    [SerializeField] private BTSave _save;
    public Transform GO;
    Node root;
    //public Transform[] waypoints;

    public void applyChildren(Node node)
    {
        foreach (Node child in node.children)
        {
            if (child != null)
            {
                if(child.type == NodeType.TASK)
                {
                    child.transform = GO;
                }
                else
                {
                    applyChildren(child);
                }
            }
        }
    }
    protected override Node SetupTree()
    {
        GO = GetComponent<Transform>();
        root = _save.root;
        applyChildren(root);
        return root;
    }
    public CustomBTEditor blackboard = new CustomBTEditor();
 
}
