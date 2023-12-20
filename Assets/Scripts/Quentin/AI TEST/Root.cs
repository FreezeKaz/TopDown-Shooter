using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Root : Node
    {
        public Root() : base() { 
            _dataContext = new Dictionary<string, object>();
        }
        public Root(List<Node> children) : base(children) {
            _dataContext = new Dictionary<string, object>();
        }

        bool one = false;

        private void Awake()
        {
            type = NodeType.VERIF;
        }
        public override NodeState Evaluate()
        {
            //Debug.Log(GetData("target"));
            if (!one)
            {
                children.Sort(SortByOrder);                
                one = true;
            }

            bool anyChildIsRunning = false;

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}

