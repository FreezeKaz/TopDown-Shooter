using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    //float rotationSpeed = 2.0f;
    Shooting shooter;
    Transform go;
    Transform player;
    Rigidbody2D _rb;
    GameObject _gameObject;


    public AttackState(GameObject _npc, Animator _anim,
                EnemyManager _enemyManager, Transform _player) : base( _npc, _anim, _enemyManager, _player)
    {
        name = STATE.ATTACK;
        shooter = enemyManager.Actions.gameObject.GetComponent<Shooting>();
        go = npc.transform;
        player = _player;
        _rb = _npc.GetComponent<Rigidbody2D>();
        _gameObject = _npc;
    }

    public override void Enter()
    {
        if (anim != null) anim.SetTrigger("isShooting");
        shooter.StartShooting();
        base.Enter();
    }

    public override void Update()
    {
        Vector2 vector2 = player.position;
        _rb.transform.up = vector2 - new Vector2(_rb.transform.position.x, _rb.transform.position.y);
       
    }

    public override void Exit()
    {
        anim.ResetTrigger("isShooting");
        shooter.StopShooting();
        base.Exit();
    }

    public override bool CanEnterState()
    {
        if (CanAttackPlayer())
            return true;
        return false;
    }
}
