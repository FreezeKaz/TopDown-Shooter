using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    int currentIndex = -1;
    Vector3 destination =  new Vector3(0, 0, 0);

    public PatrolState(GameObject _npc, Animator _anim,
                Transform _player) : base( _npc, _anim, _player)
    {
        name = STATE.PATROL;
    }

    void NewDestination()
    {
        int i = Random.Range(0, GameEnvironment.Singleton.Checkpoints.Count);

        while (i == currentIndex)
            i = Random.Range(0, GameEnvironment.Singleton.Checkpoints.Count);
        currentIndex = i;
        destination = GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position;
    }

    public override void Enter()
    {
        float lastDist = Mathf.Infinity;
        anim.SetTrigger("isWalking");
        base.Enter();
        NewDestination();
    }

    public override void Update()
    {
        if (Vector3.Distance(npc.transform.position, destination) < 1)
            NewDestination();
        /*if (CanSeePlayer()) {
            nextState = new PursueState(npc, anim, player);
            stage = EVENT.EXIT;
        }*/
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

    public override void Leave()
    {
        stage = EVENT.EXIT;
    }

    public override bool CanEnterState()
    {
        if (!CanSeePlayer() && !CanAttackPlayer())
            return true;
        return false;
    }
}
