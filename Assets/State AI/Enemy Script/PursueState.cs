using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueState : State
{
    Vector3 destination =  new Vector3(0, 0, 0);
    public PursueState(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Animator _anim,
                Transform _player) : base( _npc, _agent, _anim, _player)
    {
        name = STATE.PURSUE;
      //  agent.speed = 5;
        //agent.isStopped = false;
    }

    public override void Enter()
    {
        anim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        destination = player.position;
        npc.transform.position = Vector3.MoveTowards(npc.transform.position, destination, 3 * Time.deltaTime);
        if (npc.transform.position.x > destination.x)
            npc.transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            npc.transform.rotation = Quaternion.Euler(0, 0, 0);

        //npc.transform.Translate(player.position);
        //agent.SetDestination(player.position);

        //if (agent.hasPath) {
            if (CanAttackPlayer()) {
                nextState = new AttackState(npc, agent, anim, player);
                stage = EVENT.EXIT;
            } else if (!CanSeePlayer()) {
                nextState = new PatrolState(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
       // }
    }

    public override void Exit()
    {
       anim.ResetTrigger("isRunning");
       base.Exit();
    }
}
