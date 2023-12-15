using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    //float rotationSpeed = 2.0f;

    public AttackState(GameObject _npc, Animator _anim,
                EnemyManager _enemyManager, Transform _player) : base( _npc, _anim, _enemyManager, _player)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        anim.SetTrigger("isShooting");
       // shoot.Play();
        base.Enter();
    }

    public override void Update()
    {
        //npc.GetComponent<EnemyManager>().Actions.gameObject.GetComponent<Shooting>().StartShooting();
        /*Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        direction.y = 0;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                Quaternion.LookRotation(direction), 
                                Time.deltaTime * rotationSpeed);*/
    }

    public override void Exit()
    {
        anim.ResetTrigger("isShooting");
       // shoot.Stop();
        base.Exit();
    }

    public override bool CanEnterState()
    {
        if (CanAttackPlayer())
            return true;
        return false;
    }
}
