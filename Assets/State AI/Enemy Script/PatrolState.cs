using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    int currentIndex = -1;
    Vector3 destination =  new Vector3(0, 0, 0);
    EnemyManager enemyManager;

    Rigidbody2D _rb;
    public PatrolState(GameObject _npc, Animator _anim,
                EnemyManager _enemyManager, Transform _player) : base( _npc, _anim, _enemyManager, _player)
    {
        name = STATE.PATROL;
        enemyManager = _enemyManager;
        _rb = _npc.GetComponent<Rigidbody2D>();
    }

    void NewDestination()
    {
        currentIndex++;
        if (currentIndex < 0)
            currentIndex = 0;
        if (currentIndex >= MapManager.Instance.map.wayPoints.waypoints.Count - 1)
            currentIndex = 0;
        //int i = Random.Range(0, GameEnvironment.Singleton.Checkpoints.Count - 1);

       // while (i == currentIndex)
          //  i = Random.Range(0, GameEnvironment.Singleton.Checkpoints.Count - 1);
        //currentIndex = i;
        if (MapManager.Instance.map.wayPoints.waypoints[currentIndex] != null)
            destination = MapManager.Instance.map.wayPoints.waypoints[currentIndex].transform.position;
    }

    public override void Enter()
    {
        anim.SetTrigger("isWalking");
        base.Enter();
        NewDestination();
    }

    public override void Update()
    {
        //Debug.Log("Patrol\n");
        if (Vector3.Distance(npc.transform.position, destination) < 1)
            NewDestination();
        _rb.transform.up = new Vector2(destination.x,   destination.y);
        npc.transform.position = Vector3.MoveTowards(npc.transform.position, destination, 2 * Time.deltaTime);

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
