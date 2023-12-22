using UnityEngine;
using BehaviorTree;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.AI;

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

    public RaycastHit2D hit;


    protected Transform GO;

    public EnemyManager enemyManager;

    public LayerMask ignore;

    //public NavMeshAgent navMeshAgent;

    //int mask = 1 << 0 | 1 << 9;

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
        //navMeshAgent = GetComponent<NavMeshAgent>();
        //hit = Physics2D.Raycast(transform.position, Vector2.up, FovRange, LayerMask.NameToLayer("Enemy"));
    }

    private void FixedUpdate()
    {
        if (_root != null)
        {
            //_root = SetupTree();
            //_root.Init();
            //Debug.Log(LayerMask.NameToLayer("Enemy"));
            hit = Physics2D.Raycast(transform.position, Rb.transform.up, FovRange, ignore);
            Debug.DrawLine(transform.position, transform.position  + Rb.transform.up * FovRange);
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
