using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public Transform player;
    Animator anim;
    public StateMachine stateMachine { get; set; }
    public IdleState idleState { get; set; }
   // public EnemyAttackState AttackState { get; set; }
    public PatrolState patrolState { get; set; }
    public PursueState pursueState { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();

        stateMachine = new StateMachine();
        
        idleState = new IdleState(this.gameObject, anim, player);
        patrolState = new PatrolState(this.gameObject, anim, player);
        pursueState = new PursueState(this.gameObject, anim, player);
        
        stateMachine.Initialize(idleState);

        Debug.Log("Start");

        stateMachine.addState(idleState);
        stateMachine.addState(patrolState);
        stateMachine.addState(pursueState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Process();
    }
}
