using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueState : State
{
    Transform player;
    Rigidbody2D _rb;
    GameObject _gameObject;
    EnemyManager enemyManager;
    Vector3 destination =  new Vector3(0, 0, 0);
    public PursueState(GameObject _npc, Animator _anim,
                EnemyManager _enemyManager, Transform _player) : base( _npc, _anim, _enemyManager, _player)
    {
        _rb = _npc.GetComponent<Rigidbody2D>();
        player = _player;
        name = STATE.PURSUE;
        _gameObject = _npc;
        enemyManager = _enemyManager;
    }

    public override void Enter()
    {
        anim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        Vector2 vector2 = player.position;
        _rb.transform.up = vector2 - new Vector2(_rb.transform.position.x, _rb.transform.position.y);
        _gameObject.transform.position = Vector3.MoveTowards(_gameObject.transform.position, player.position, enemyManager.myEntityStats.MoveSpeedRatio * Time.deltaTime);
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
