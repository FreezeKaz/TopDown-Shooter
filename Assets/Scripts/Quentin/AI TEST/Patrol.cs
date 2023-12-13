using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Patrol : Node
    {
        private Transform _transform;
        private Transform[] _waypoints;
        private Rigidbody2D _rb;

        private int _currentWaypointIndex = 0;

        private float _waitTime = 1f; // in seconds
        private float _waitCounter = 0f;
        private bool _waiting = false;

        public Patrol(Transform transform, Transform[] waypoints)
        {
            _transform = transform;          
            _waypoints = waypoints;
            _rb = _transform.GetComponent<Rigidbody2D>();
        }

        public override NodeState Evaluate()
        {
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
                if (Vector3.Distance(_transform.position, wp.position) < 0.01f)
                {
                    _transform.position = wp.position;
                    _waitCounter = 0f;
                    _waiting = true;

                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length; 
                }
                else
                {
                    _transform.position = Vector3.MoveTowards(_transform.position, wp.position, IABT.speed * Time.deltaTime);
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

