using UnityEngine;
using BehaviorTree;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BTApp : BehaviorTree.Tree
{
    [SerializeField] private BTSave _save;
    private BTSave _self;
    Node root;
    public static float speed = 5f;
    public static float fovRange = 15f;
    public static float range = 14f;
    [HideInInspector] public List<Transform> waypoints;

    protected override void Init()
    {
        
    }



    protected override Node SetupTree()
    {
        GO = GetComponent<Transform>();
        root = _save.Clone().root;
        applyChildren(root);
        return root;
    }
 
}
