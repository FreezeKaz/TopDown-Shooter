using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        bool one = false;

        private void Awake()
        {
            type = NodeType.VERIF;
        }

        public override NodeState Evaluate()
        {
            if (!one)
            {
                children.Sort(SortByOrder);
                one = true;
            }
            bool anyChildIsRunning = false;
            foreach(Node node in children)
            {
                switch(node.Evaluate())
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
