using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BehaviorTree
{
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        bool one = false;

        private void Awake()
        {
            type = NodeType.VERIF;
        }


        public override NodeState Evaluate(BTApp app)
        {
            if (!one)
            {               
                children.Sort(SortByOrder);
                one = true;
            }
            foreach (Node node in children)
            {
                switch (node.Evaluate(app))
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}

