using UnityEngine;
using BehaviorTree;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BTApp : MonoBehaviour
{
    [SerializeField] private BTSave _save;
    private BTSave _self;
    Node root;
    public static float speed = 5f;
    public static float fovRange = 15f;
    public static float range = 14f;
    [HideInInspector] public List<Transform> waypoints;

    private Node _root = null;
    protected Transform GO;

    private void Start()
    {
        _root = SetupTree();
        _root.Init();
    }

    private void Update()
    {
        if (_root != null)
        {
            //_root = SetupTree();
            //_root.Init();
            _root.Evaluate(this);
            //applyChildren(_root);
        }
    }

    public void applyChildren(Node node)
    {
        foreach (Node child in node.children)
        {
            if (child != null)
            {
                if (child.type == NodeType.TASK)
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




    protected Node SetupTree()
    {
        GO = GetComponent<Transform>();
        root = _save.Clone().root;
        applyChildren(root);
        return root;
    }
 
}
