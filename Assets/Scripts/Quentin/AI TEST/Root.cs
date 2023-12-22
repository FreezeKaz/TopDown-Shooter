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
            foreach (Node child in children)
            {
                Attach(child);
            }
        }
        public Root(List<Node> children) : base(children) 
        {
            _dataContext = new Dictionary<GOType, object>();
            foreach (Node child in children)
            {
                Attach(child);
            }
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

