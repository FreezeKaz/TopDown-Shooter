using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null;

        private void Start()
        {
            _root = SetupTree();
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
