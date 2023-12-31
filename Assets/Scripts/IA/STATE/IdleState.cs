using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(GameObject _npc, Animator _anim,
                EnemyManager _enemyManager, Transform _player) : base( _npc, _anim, _enemyManager, _player)
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
        Debug.Log("Idle\n");
    }

    public override void Exit()
    {
       
        if(anim != null) anim.ResetTrigger("isIdle");
        base.Exit();
    }

    public override void Leave()
    {
        stage = EVENT.EXIT;
    }

    public override bool CanEnterState()
    {
        return false;
    }
}
