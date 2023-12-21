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
                //Debug.Log(node);
                node.Init();
            }
        }
        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
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

