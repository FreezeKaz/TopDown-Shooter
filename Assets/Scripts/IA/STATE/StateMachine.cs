using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine //: MonoBehaviour
{
    public State currentEnemyState { get; set; }
    public List<State> statesList;

    public StateMachine()
    {
        statesList =  new List<State>();
        currentEnemyState = null;
    }

    public void Initialize(State startingState)
    {
        currentEnemyState = startingState;
       /* currentEnemyState.Process();*/
    }

    public void ChangeState(State nextState)
    {
        currentEnemyState.Exit();
        currentEnemyState = nextState;
        currentEnemyState.Enter();
    }

    public void addState(State newState)
    {
        statesList.Add(newState);
    }

    public void Process()
    {
        for(int i = 0; i < statesList.Count; i++)  { 
            if (statesList[i].CanEnterState() && currentEnemyState != statesList[i]) {
                ChangeState(statesList[i]);
                break;
            }
        } 
        currentEnemyState.Process();
    }

    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
