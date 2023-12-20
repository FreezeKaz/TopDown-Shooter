using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Root : Node
    {
        public Root() : base() {}
        public Root(List<Node> children) : base(children) {}

        private void Awake()
        {
            type = NodeType.VERIF;
            children.Sort(SortByOrder);
            _dataContext = new Dictionary<GOType, object>();
        }

        public override void Init()
        {
            type = NodeType.VERIF;
            children.Sort(SortByOrder);
            _dataContext = new Dictionary<GOType, object>();
            foreach (Node node in children)
            {
                node.Init();
            }
        }
        public override NodeState Evaluate()
        {

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

