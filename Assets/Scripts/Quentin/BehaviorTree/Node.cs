using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public enum NodeType
    {
        TASK,
        VERIF,
    }

    public class Node : ScriptableObject
    {
        public string guid;

        public Transform GO;
        public NodeType type;

        protected NodeState state;

        public Vector2 positionOnView;

        public Node parent;
        public List<Node> children = new List<Node>();

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public Node()
        {
            parent = null;
        }
        public Node(List<Node> children)
        {
            foreach (Node child in children)
            {
                Attach(child);
            }
        }

        public void Attach(Node node) 
        {
            node.parent = this;
            children.Add(node);
        }

        public void Remove(Node node)
        {
            node.parent.Remove(this);
            children.Remove(node);
        }

        public virtual Node Clone()
        {
            return Instantiate(this);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;

            if (_dataContext.TryGetValue(key, out value))
                return value;

            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if(value != null) 
                    return value;
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }

        public virtual void CopyData(Node source)
        {
            positionOnView = source.positionOnView;
        }
    }
}

