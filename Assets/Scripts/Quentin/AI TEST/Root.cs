using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Root : Node
    {
        

        public Root() : base() 
        {
            _dataContext = new Dictionary<GOType, object>();
        }
        public Root(List<Node> children) : base(children) 
        {
            _dataContext = new Dictionary<GOType, object>();
        }

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
            foreach (Node node in children)
            {
                //Debug.Log(node);
                node.Init();
            }
        }
        public override NodeState Evaluate(BTApp app)
        {
            GetData(GOType.TARGET);
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

