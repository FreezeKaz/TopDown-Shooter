using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Transform player;
    public EnemyManager enemy;
    Animator anim;
   State currentState;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        currentState = new IdleState(this.gameObject, anim,enemy, player) ;
    }

    // Update is called once per frame
    void Update()
    {
       currentState = currentState.Process();
    }
}
