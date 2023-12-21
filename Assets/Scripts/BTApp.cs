using UnityEngine;
using BehaviorTree;
using System.Collections.Generic;

public class BTApp : BehaviorTree.Tree
{
    [SerializeField] private BTSave _save;
    public Transform GO;
    Node root;
    public static float speed = 5f;
    public static float fovRange = 15f;
    public static float range = 14f;
    [HideInInspector] public List<Transform> waypoints;

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
 
}
