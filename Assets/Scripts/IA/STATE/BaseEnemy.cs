using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    Transform player;
    public List<Transform> waypoints;


    public StateMachine stateMachine { get; set; }
    public IdleState idleState { get; set; }
   // public EnemyAttackState AttackState { get; set; }
    public PatrolState patrolState { get; set; }
    public PursueState pursueState { get; set; }
    public AttackState attackState { get; set; }
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        stateMachine = new StateMachine();
        idleState = new IdleState(this.gameObject, anim, enemyManager, player);
        patrolState = new PatrolState(this.gameObject, anim, enemyManager, player);
        pursueState = new PursueState(this.gameObject, anim, enemyManager, player);
        attackState = new AttackState(this.gameObject, anim, enemyManager, player);
        
        stateMachine.Initialize(idleState);
        stateMachine.addState(idleState);
        stateMachine.addState(patrolState);
        stateMachine.addState(pursueState);
        stateMachine.addState(attackState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Process();
    }
}
