using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(GameObject _npc, Animator _anim,
                Transform _player) : base( _npc, _anim, _player)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        anim.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer()) {
            nextState = new PursueState(npc, anim, player);
            stage = EVENT.EXIT;
        }
        if (Random.Range(0, 100) < 10) {
            nextState = new PatrolState(npc, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isIdle");
        base.Exit();
    }
}
