using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    int currentIndex = -1;
    Vector3 destination =  new Vector3(0, 0, 0);

    public PatrolState(GameObject _npc, Animator _anim,
                EnemyManager _enemyManager, Transform _player) : base( _npc, _anim, _enemyManager, _player)
    {
        name = STATE.PATROL;
    }

    void NewDestination()
    {
        currentIndex++;
        if (currentIndex < 0)
            currentIndex = 0;
        if (currentIndex >= GameEnvironment.Singleton.Checkpoints.Count - 1)
            currentIndex = 0;
        //int i = Random.Range(0, GameEnvironment.Singleton.Checkpoints.Count - 1);

       // while (i == currentIndex)
          //  i = Random.Range(0, GameEnvironment.Singleton.Checkpoints.Count - 1);
        //currentIndex = i;
        if (GameEnvironment.Singleton.Checkpoints[currentIndex] != null)
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
        Debug.Log("Patrol\n");
        if (Vector3.Distance(npc.transform.position, destination) < 1)
            NewDestination();
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
