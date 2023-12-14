using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    int currentIndex = -1;
    Vector3 destination =  new Vector3(0, 0, 0);

    public PatrolState(GameObject _npc, NavMeshAgent _agent, Animator _anim,
                Transform _player) : base( _npc, _agent, _anim, _player)
    {
        name = STATE.PATROL;
        //agent.speed = 2;
        //agent.isStopped = false;
    }

    public override void Enter()
    {
        float lastDist = Mathf.Infinity;
        for (int i = 0; i < GameEnvironment.Singleton.Checkpoints.Count; i++) {
            float distance = Vector3.Distance(npc.transform.position,
            GameEnvironment.Singleton.Checkpoints[i].transform.position);
            if  (distance < lastDist) {
                currentIndex = i;
                lastDist = distance;
            }
        }
        anim.SetTrigger("isWalking");
        base.Enter();
        destination = GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position;        
    }

    public override void Update()
    {
        
        if (Vector3.Distance(npc.transform.position, destination) < 1) {
            if (currentIndex >= GameEnvironment.Singleton.Checkpoints.Count - 1) {
                currentIndex = 0;
            } else
                currentIndex++;
            destination = GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position;
        }
        if (CanSeePlayer()) {
            nextState = new PursueState(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        npc.transform.position = Vector3.MoveTowards(npc.transform.position, destination, 2 * Time.deltaTime);
        if (npc.transform.position.x > destination.x)
            npc.transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            npc.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public override void Exit()
    {
        anim.ResetTrigger("isWalking");
        base.Exit();
    }
}
