using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public enum STATE
    {
        IDLE, PATROL, PURSUE, ATTACK, SLEEP
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected EnemyManager enemyManager;


    float visDist = 15.0f;
    // float visAngle = 30.0f;
    float shootDist = 10f;

    public State(GameObject _npc, Animator _anim,
                EnemyManager _enemyManager, Transform _player)
    {
        npc = _npc;
        anim = _anim;
        player = _player;
        stage = EVENT.ENTER;
        enemyManager = _enemyManager;
        visDist = enemyManager.myEntityStats.RangeOfSight;
        shootDist = enemyManager.myEntityStats.RangeofAttack;
    }

    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public State Process()
    {
        switch (stage)
        {
            case EVENT.ENTER:
                Enter();
                break;
            case EVENT.UPDATE:
                Update();
                break;
            case EVENT.EXIT:
                Exit();
                return nextState;
        }
        return this;
    }

    public virtual void Leave()
    {
        stage = EVENT.EXIT;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        // Debug.Log(angle + ";\n");
        if (direction.magnitude < visDist /*&& angle < visAngle*/)
            return true;
        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;

        if (direction.magnitude <= shootDist)
            return true;
        return false;
    }

    public virtual bool CanEnterState()
    {
        return false;
    }
}
