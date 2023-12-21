using System.Collections.Generic;
using System.Linq;
using Unity.XR.OpenVR;
using UnityEditor.Rendering;
using UnityEngine;
namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

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
        public override NodeState Evaluate()
        {
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
