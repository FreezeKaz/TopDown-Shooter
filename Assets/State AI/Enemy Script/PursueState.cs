using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueState : State
{
    Vector3 destination =  new Vector3(0, 0, 0);
    public PursueState(GameObject _npc, Animator _anim,
                EnemyManager _enemyManager, Transform _player) : base( _npc, _anim, _enemyManager, _player)
    {
        name = STATE.PURSUE;
    }

    public override void Enter()
    {
        anim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        destination = player.position;
        npc.transform.position = Vector3.MoveTowards(npc.transform.position, destination, 4 * Time.deltaTime);
        if (npc.transform.position.x > destination.x)
            npc.transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            npc.transform.rotation = Quaternion.Euler(0, 0, 0);

        /*if (CanAttackPlayer()) {
            nextState = new AttackState(npc, anim, player);
            stage = EVENT.EXIT;
        } else if (!CanSeePlayer()) {
            nextState = new PatrolState(npc, anim, player);
            stage = EVENT.EXIT;
        }*/
    }

    public override void Exit()
    {
       anim.ResetTrigger("isRunning");
       base.Exit();
    }

    public override bool CanEnterState()
    {
        if (CanSeePlayer() && !CanAttackPlayer())
            return true;
        return false;
    }
}
