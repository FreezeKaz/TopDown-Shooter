using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Patrol : Node
    {

        [SerializeField] private List<Transform> _waypoints;
        private Rigidbody2D _rb;

        private int _currentWaypointIndex = 0;

        private float _waitTime = 1f; // in seconds
        private float _waitCounter = 0f;
        private bool _waiting = false;
        private bool _first = true;

        public Patrol(GameObject gameObject, List<Transform> waypoints)
        {
            
            _waypoints = waypoints;
            _rb = transform.GetComponent<Rigidbody2D>();
        }

        private void Awake()
        {
            type = NodeType.TASK;
        }

        private void Init()
        {
            //_transform = GO.GetComponent<Transform>();
            _rb = transform.GetComponent<Rigidbody2D>();
        }

        public override NodeState Evaluate()
        {
            Debug.Log(base.transform.name);
            if (_first)
            {
                Init();
            }
            _first = true;
            Debug.Log("uwu");
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;
                if (_waitCounter >= _waitTime)
                {
                    _waiting = false;
                }
            }
            else
            {
                Transform wp = _waypoints[_currentWaypointIndex];
                if (Vector3.Distance(transform.position, wp.position) < 0.01f)
                {
                    transform.position = wp.position;
                    _waitCounter = 0f;
                    _waiting = true;

                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count; 
                }
                else
                {
                    Debug.Log("icicmove");
                    transform.position = Vector3.MoveTowards(transform.position, wp.position, 5f * Time.deltaTime);
                    Vector2 vector2 = wp.position;
                    _rb.transform.up = vector2 - new Vector2(_rb.transform.position.x, _rb.transform.position.y);
                    //_transform.LookAt(wp.position);
                }
            }

            state = NodeState.RUNNING;
            return state;
        }
    }
}

