using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public class CheckCanShoot : Node
    {
        public override void Init()
        {
            type = NodeType.TASK;
        }

        public CheckCanShoot(GameObject gameObject)
        {

        }

        public override NodeState Evaluate(BTApp app)
        {
            Transform target = (Transform)GetData(GOType.TARGET);

            int mask = 1 << 9;

            Vector2 temp = target.GetComponent<Rigidbody2D>().velocity;

            //target.GetComponent<Rigidbody2D>().GetRelativePointVelocity(temp);
            if(app.hit.collider != null)
            {
               if(app.hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                 {
                    Vector2 vector2 = target.position;
                    app.Rb.transform.up = vector2 - new Vector2(app.Rb.transform.position.x, app.Rb.transform.position.y);


                    //if (temp.x > 0) 
                    //{
                    //    app.Rb.AddForceX(-temp.normalized.x);
                    //}
                    //else
                    //{
                    //    app.Rb.AddForceX(temp.normalized.x);
                    //}

                    //if(temp.y > 0)
                    //{
                    //    app.Rb.AddForceY(-temp.normalized.y);
                    //}
                    //else
                    //{
                    //    app.Rb.AddForceY(temp.normalized.y);
                    //}

                    // Il y a un obstacle entre l'ennemi et le joueur

                    // Trouve un nouvel angle de tir en contournant l'obstacle

                    app.navMeshAgent.SetDestination(target.position);

                    Debug.Log("Can't shoot");

                    state = NodeState.SUCCESS;
                    return state;
                }
                else
                {
                    state = NodeState.RUNNING;
                    return state;
                }
            }
            else
            {
                state = NodeState.RUNNING;
                return state;
            }
        }
    }
}
