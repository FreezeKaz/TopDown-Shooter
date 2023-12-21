using UnityEngine;
using BehaviorTree;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BTApp : MonoBehaviour
{
    public BTSave _save;
    Node root;
    public float Speed;
    public float FovRange;
    public float Range;
    public List<Transform> Waypoints;
    public Rigidbody2D Rb;
    public int CurrentWaypointIndex;
    public float WaitTime; // in seconds
    public float WaitCounter;
    public bool Waiting;

    public float AttackTime;
    public float AttackCounter;

    public GameObject BulletPrefab;

    private Node _root = null;
    protected Transform GO;

    private void Start()
    {
        _root = SetupTree();
        _root.Init();
        Rb = GetComponent<Rigidbody2D>();
        Speed = 5f;
        FovRange = 15f;
        Range = 14f;
        CurrentWaypointIndex = 0;
        WaitTime = 1f; // in seconds
        WaitCounter = 0f;
        Waiting = false;

        AttackTime = 0.2f;
        AttackCounter = 0f;
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
