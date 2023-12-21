using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null;
        protected Transform GO;

        private void Start()
        {
            Init();
            _root = SetupTree();
            _root.Init();
        }

        private void Update()
        {
            if (_root != null)
            {
                //_root = SetupTree();
                //_root.Init();
                _root.Evaluate();
                //applyChildren(_root);
            }
        }

        public void applyChildren(Node node)
        {
            foreach (Node child in node.children)
            {
                if (child != null)
                {
                    if (child.type == NodeType.TASK)
                    {
                        child.transform = GO;
                    }
                    else
                    {
                        applyChildren(child);
                    }
                }
            }
        }

        protected abstract Node SetupTree();

        protected abstract void Init();
    }
}
