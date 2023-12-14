using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;
    Animator anim;
    State currentState;
    // Start is called before the first frame update
    void Start()
    {
        agent = new NavMeshAgent();
        anim = this.GetComponent<Animator>();
        currentState = new IdleState(this.gameObject, agent, anim, player);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}
