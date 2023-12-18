using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    //float rotationSpeed = 2.0f;
    Shooting shooter;
    GameObject firepoint;

    public AttackState(GameObject _npc, Animator _anim,
                EnemyManager _enemyManager, Transform _player) : base( _npc, _anim, _enemyManager, _player)
    {
        name = STATE.ATTACK;
        shooter = enemyManager.Actions.gameObject.GetComponent<Shooting>();
        firepoint = npc.transform.Find("FirePoints").gameObject;
    }

    public override void Enter()
    {
        anim.SetTrigger("isShooting");
       // shoot.Play();
        base.Enter();
    }

    public override void Update()
    {
        Debug.Log("Update\n");
        
        shooter.StartShooting();
        Vector3 direction = player.position - npc.transform.position;
        float angle = Mathf.Atan2(direction.y , direction.x) * Mathf.Rad2Deg + 45;
        firepoint.transform.rotation = Quaternion.Euler(0, 0, angle -180);
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
