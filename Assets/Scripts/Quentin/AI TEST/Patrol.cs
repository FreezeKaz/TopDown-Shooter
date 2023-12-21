using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Patrol : Node
    {        public Patrol(GameObject gameObject, List<Transform> waypoints) { }
        

        private void Awake()
        {
        }

        public override void Init()
        {
            type = NodeType.TASK;
        }

        public override NodeState Evaluate(BTApp app)
        {
            if (app.Waiting)
            {
                app.WaitCounter += Time.deltaTime;
                if (app.WaitCounter >= app.WaitTime)
                {
                    app.Waiting = false;
                }
            }
            else
            {
                Transform wp = app.waypoints[app.CurrentWaypointIndex];
                if (Vector3.Distance(app.transform.position, wp.position) < 0.01f)
                {
                    app.transform.position = wp.position;
                    app.WaitCounter = 0f;
                    app.Waiting = true;

                    app.CurrentWaypointIndex = (app.CurrentWaypointIndex + 1) % app.waypoints.Count; 
                }
                else
                {
                    app.transform.position = Vector3.MoveTowards(app.transform.position, wp.position, 5f * Time.deltaTime);
                    Vector2 vector2 = wp.position;
                    app.Rb.transform.up = vector2 - new Vector2(app.Rb.transform.position.x, app.Rb.transform.position.y);
                    //_transform.LookAt(wp.position);
                }
            }

            state = NodeState.RUNNING;
            return state;
        }
    }
}

