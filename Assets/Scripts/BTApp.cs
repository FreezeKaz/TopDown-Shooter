using UnityEngine;
using BehaviorTree;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BTApp : MonoBehaviour
{
    public BTSave _save;
    //Node root;
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

    public GameObject BulletPrecondfab;

    protected Transform GO;

    public EnemyManager enemyManager;

    private Node _root = null;
    private void Awake()
    {
        Speed = 5f;
        FovRange = 15f;
        Range = 14f;
        CurrentWaypointIndex = 0;
        WaitTime = 1f; // in ses
        WaitCounter = 0f;
        Waiting = false;

        AttackTime = 0.2f;
        AttackCounter = 0f;
    }

    private void Start()
    {
        //Debug.Log(_save.root.children.Count);
        _root = SetupTree();
        _root.Init();
        Rb = GetComponent<Rigidbody2D>();
        GO = GetComponent<Transform>();
        enemyManager = GetComponent<EnemyManager>();
    }

    private void Update()
    {
        if (_root != null)
        {
            //_root = SetupTree();
            //_root.Init();
            _root.Evaluate(this);
            Debug.Log("BTapp");
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
                    //child.transform = GO;
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
        _root = _save.Clone().root;
        applyChildren(_root);
        return _root;
    }

}
