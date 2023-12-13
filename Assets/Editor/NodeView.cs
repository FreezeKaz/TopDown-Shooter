using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable]
    public class NodeView
    {
        public Rect rect;
        public string Title;
        public Node node;
        
        public ConnectionPoint inPoint;
        public ConnectionPoint outPoint;

        public NodeView(Rect rect, ConnectionPoint inPoint, ConnectionPoint outPoint, Action<NodeView, ConnectionPointType> onClickConnectionPoint)
        {
            this.rect = rect;
            this.inPoint = inPoint;
            this.outPoint = outPoint;
        }

        public void Draw()
        {
            inPoint?.Draw(this);
            outPoint?.Draw(this);
            GUI.Box(rect, Title);
        }
    }
}
