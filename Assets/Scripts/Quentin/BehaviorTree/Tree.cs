using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null;

        private void Start()
        {
            _root = SetupTree();
            _root.Init();
        }

        private void Update()
        {
            if (_root != null)
            {
                _root.Evaluate();
            }
        }
        protected abstract Node SetupTree();
    }
}
