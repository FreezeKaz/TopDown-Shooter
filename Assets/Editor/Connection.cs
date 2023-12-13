using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    public class Connection
    {
        public NodeView InNode;
        public NodeView OutNode;
        public Action<Connection> OnRemove;

        public Connection(NodeView inNode, NodeView outNode, Action<Connection> onRemove)
        {
            InNode = inNode;
            OutNode = outNode;
            OnRemove = onRemove;
        }

        public void Draw()
        {
            //Handles.DrawBezier(InNode.inPoint.rect.center, OutNode.outPoint.rect.center, InNode.inPoint.rect.center, OutNode.outPoint.rect.center, Color.black, null, 2f);
            Handles.DrawLine(InNode.inPoint.rect.center, OutNode.outPoint.rect.center);

            if (true && Handles.Button((InNode.rect.center + OutNode.rect.center) * .5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap))
            {
                OnRemove?.Invoke(this);
            }
        }
    }
}
