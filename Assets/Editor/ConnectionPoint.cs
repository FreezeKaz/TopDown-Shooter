using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public enum ConnectionPointType
    {
        In, Out
    }

    [Serializable]

    public class ConnectionPoint
    {
        public Rect rect;
        public ConnectionPointType CPtype;
        //public GUIStyle style;
        public Action<NodeView, ConnectionPointType> onClickConnectionPoint;

        public ConnectionPoint(Vector2 size, ConnectionPointType cPtype, Action<NodeView, ConnectionPointType> onClickConnectionPoint)
        {
            this.rect = new Rect(0,0,size.x,size.y);
            CPtype = cPtype;
            this.onClickConnectionPoint = onClickConnectionPoint;
        }

        public void Draw(NodeView node)
        {
            rect.y = node.rect.y + node.rect.height * .5f - rect.height * .5f;

            switch (CPtype)
            {
                case ConnectionPointType.In:
                    rect.x = node.rect.x - rect.width + 8f;
                    break;
                case ConnectionPointType.Out:
                    rect.x = node.rect.x + node.rect.width - 8f;
                    break;
                default:
                    break;
            }
            if(GUI.Button(rect, ""))
            {
                onClickConnectionPoint?.Invoke(node, CPtype);
            }
        }
    }
}
