using System.Collections.Generic;
using Unity.VisualScripting;
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

        public NodeType type;

        protected NodeState state;

        public Vector2 positionOnView;

        public Node parent;
        public List<Node> children = new List<Node>();

        protected Dictionary<GOType, object> _dataContext;

        public enum GOType
        {
            TARGET = 0,
            NONE = 1
        }

        public Dictionary<GOType, object> DataContext => _dataContext == null ? parent.DataContext : _dataContext;

        public int _executionOrder;

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
        public virtual void Init()
        {

        }
        public virtual NodeState Evaluate(BTApp app) => NodeState.FAILURE;

        public NodeState BTUpdate(BTApp app)
        {
            state = Evaluate(app);
            return state;
        }

        public void SetData(GOType key, object value)
        {
            DataContext[key] = value;
        }

        public object GetData(GOType key)
        {
            object value = null;

            //if(_dataContext == null)
            //    _dataContext = new Dictionary<string, object>();

            //Debug.Log(_dataContext.Count);

            if (DataContext.TryGetValue(key, out value))
                return value;

            return null;
        }

        public bool ClearData(GOType key)
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

        public int SortByOrder(Node n1, Node n2)
        {
            return n1._executionOrder.CompareTo(n2._executionOrder);
        }
    }
}

