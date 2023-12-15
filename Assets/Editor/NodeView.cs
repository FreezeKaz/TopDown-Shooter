using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

        public bool isDragged;
        public bool IsSelected;
        public Action<NodeView> OnClickNode;
        public Action<NodeView> OnClickUp;
        public Action<NodeView> OnRemoveNode;
        public Action<NodeView> OnDragNode;

        public NodeView(Rect rect, ConnectionPoint inPoint, ConnectionPoint outPoint, Action<NodeView, ConnectionPointType> onClickConnectionPoint, Node node
            , Action<NodeView> OnClickNode, Action<NodeView> OnClickUp, Action<NodeView> OnRemoveNode, Action<NodeView> OnDragNode)
        {
            this.node = node;
            this.Title = node.GetType().Name;
            this.rect = rect;
            this.inPoint = inPoint;
            this.outPoint = outPoint;
            this.OnClickNode = OnClickNode;
            this.OnClickUp = OnClickUp;
            this.OnRemoveNode = OnRemoveNode;
            this.OnDragNode = OnDragNode;
            
        }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            Click();
                            e.Use();
                        }
                    }

                    if (e.button == 1 && rect.Contains(e.mousePosition))
                    {
                        Remove();
                        e.Use();
                    }
                    break;
                case EventType.MouseUp:
                    if (e.button == 0 && rect.Contains(e.mousePosition))
                    {
                        ClickUp();
                        e.Use();
                    }
                    break;
                case EventType.MouseDrag:
                    if (e.button == 0 && IsSelected)
                    {
                        Drag(e.delta, true);
                        e.Use();
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }

        private void Click()
        {
            isDragged = true;
            GUI.changed = true;
            OnClickNode?.Invoke(this);
        }

        private void Remove()
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
            genericMenu.ShowAsContext();
        }

        private void ClickUp()
        {
            //Debug.Log("Click up on node " + rect.position);
            isDragged = false;
            OnClickUp?.Invoke(this);
        }

        public void Drag(Vector2 delta, bool invokeCallbacks)
        {
            SetPosition(rect.position + delta);
            if (invokeCallbacks)
                OnDragNode?.Invoke(this);
        }

        private void OnClickRemoveNode()
        {
            OnRemoveNode?.Invoke(this);
        }

        public void SetPosition(Vector2 position)
        {
            rect.position = position;
            //node.positionOnView.x = position.x;
            //node.positionOnView.y = position.y;
        }

        public void Draw()
        {
            inPoint?.Draw(this);
            outPoint?.Draw(this);
            GUI.Box(rect, Title);
        }
    }
}
