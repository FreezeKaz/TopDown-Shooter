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

    float visDist = 5.0f;
   // float visAngle = 30.0f;
    float shootDist = 0.5f;

    public State(GameObject _npc, Animator _anim,
                Transform _player)
    {
        npc = _npc;
        anim = _anim;
        player = _player;
        stage = EVENT.ENTER;
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
        switch (stage) {
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

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        Debug.Log(angle + ";\n");
        if (direction.magnitude < visDist /*&& angle < visAngle*/)
            return true;
        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;

        if (direction.magnitude < shootDist)
            return true;
        return false;
    }
}
