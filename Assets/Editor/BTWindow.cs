using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace BehaviorTree
{
    public class BTWindow : EditorWindow
    {
        BTSave BTSave;

        internal override void PopulateView()
        {
            if (file == null) BTSave = CreateInstance<BTSave>();
            else BTSave = (file as BTSave).Clone();

            BTSave = file as BehaviorTree;

            if (BTSave == null) BTSave = CreateInstance<BTSave>();

            nodeViews = new List<NodeView>();

            if (BTSave.root == null) BTSave.root = (RootNode)BTSave.CreateNode(typeof(RootNode));
            Vector2 rootPos = BTSave.root.child == null ? GetDefaultRootPosition() : BTSave.root.child.positionOnView + new Vector2(0, -50f);
            CreateNodeView(BTSave.root, rootPos);

            for (int i = 0; i < BTSave.nodes.Count; i++)
            {
                CreateNodeView(BTSave.nodes[i].Clone(), BTSave.nodes[i].positionOnView, false);
            }
        }

        [MenuItem("Tools/Behavior Tree editor")]
        public static BTWindow OpenBTWindow()
        {
            BTWindow window = CreateWindow<BTWindow>("BT");
            return window;
        }

        internal virtual void OnGUI()
        {
            DrawNodes();
            DrawConnections();
            ProcessEvents(Event.current);

            ProcessNodeEvents(Event.current);
            if (GUI.changed)
            {
                Repaint();
            }
        }


        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if(e.button == 0)
                    {
                        OnClickBackground();
                    }

                    if (e.button == 1)
                    {
                        GenericMenu genericMenu = new GenericMenu();
                        var nodeType = TypeCache.GetTypesDerivedFrom<Node>();
                        foreach (var type in nodeType)
                        {
                            genericMenu.AddItem(new GUIContent("Actions/" + type.Name), false, () => CreateNodeView(CreateNode(type), e.mousePosition));
                        }
                        genericMenu.ShowAsContext();
                    }
                    break;
                case EventType.MouseUp:

                    break;
                default:
                    break;
            }
            return false;
        }

        internal virtual void ProcessNodeEvents(Event e)
        {
            if (nodesView == null) return;

            for (int i = nodesView.Count - 1; i >= 0; i--)
            {
                bool guiChanged = nodesView[i].ProcessEvents(e);

                if (guiChanged) GUI.changed = true;
            }
        }


        public Node CreateNode(Type type)
        {
            return 
        }

        internal virtual void DrawNodes()
        {
            if (nodesView == null) return;

            for (int i = 0; i < nodesView.Count; i++)
            {
                nodesView[i].Draw();
            }
        }

        internal virtual void DrawConnections()
        {
            if (connections == null) return;
            //Debug.Log("Connections: " + connections.Count);
            //Debug.Log(connections.Count);
            for (int i = 0; i < connections.Count; i++)
            {
                connections[i].Draw();
            }
        }

        internal virtual NodeView CreateNodeView(Node node, Vector2 position)
        {
            position.x -= 200 * .5f;
            position.y -= 50 * .5f;
            NodeView nodeView = new NodeView(new Rect(position, new Vector2(200, 50)),
                new ConnectionPoint(new Vector2(20f, 20f), ConnectionPointType.In, OnClickConnectionPoint),
                new ConnectionPoint(new Vector2(20f, 20f), ConnectionPointType.Out, OnClickConnectionPoint),
                OnClickConnectionPoint, node, OnClickNode, OnClickUpNode, OnClickRemoveNode, OnDragNode);
            AddNodeView(nodeView);
            return nodeView;

        }


        internal NodeView selectedNodeIn;
        internal NodeView selectedNodeOut;
        public List<Node> nodes = new List<Node>();
        public List<NodeView> nodesView = new List<NodeView>();
        public List<Connection> connections = new List<Connection>();

        bool isDragging;
        List<NodeView> SelectedNodes { get => nodesView.FindAll(x => x.IsSelected); }


        internal virtual void AddNodeView(NodeView nodeView)
        {
            if (nodesView == null) nodesView = new List<NodeView>();
            nodesView.Add(nodeView);
            //Debug.Log("Add node view");
            if (selectedNodeIn != null && selectedNodeOut == null)
            {
                //Debug.Log("Has node in selected");
                CreateConnection(nodeView, selectedNodeIn);
            }
            if (selectedNodeIn == null && selectedNodeOut != null)
            {
                //Debug.Log("Has node out selected");
                CreateConnection(selectedNodeOut, nodeView);
            }
            ClearConnectionSelection();
            //hasUnsavedChanges = true;
        }

        internal virtual void OnClickConnectionPoint(NodeView node, ConnectionPointType type)
        {
            switch (type)
            {
                case ConnectionPointType.In:
                    selectedNodeIn = node;
                    if (selectedNodeOut == null) return;
                    break;
                case ConnectionPointType.Out:
                    selectedNodeOut = node;
                    if (selectedNodeIn == null) return;
                    break;
                default:
                    break;
            }
            if (selectedNodeOut != selectedNodeIn)
            {
                if (connections.Exists(c => c.InNode == selectedNodeIn && c.OutNode == selectedNodeOut))
                    Debug.LogWarning("A connection has already been established between these two nodes.");
                else
                {
                    CreateConnection(selectedNodeOut, selectedNodeIn);
                }
            }
            ClearConnectionSelection();
        }

        internal virtual void CreateConnection(NodeView nodeOut, NodeView nodeIn)
        {
            int indexOfNodeOut = nodesView.IndexOf(nodeOut);
            if (indexOfNodeOut < 0) return;
            int indexOfNodeIn = nodesView.IndexOf(nodeIn);
            if (indexOfNodeIn < 0) return;
            if (connections == null)
                connections = new List<Connection>();
            //if(nodeOut.node == null)
            //{
            //    return;
            //}
            connections.Add(new Connection(nodeIn, nodeOut, OnClickRemoveConnection));
            //Debug.Log(connections.Count);
            //nodeOut.node.Attach(nodeIn.node);
        }

        internal virtual List<NodeView> GetChildren(NodeView nodeView)
        {
            List<NodeView> children = new List<NodeView>();
            foreach (var connection in connections)
            {
                if (connection.OutNode == nodeView)
                {
                    children.Add(connection.InNode);
                }
            }
            return children;
        }

        internal virtual void OnClickRemoveConnection(Connection connection)
        {
            connections.Remove(connection);
            //hasUnsavedChanges = true;
        }
        internal virtual void ClearConnectionSelection()
        {
            selectedNodeIn = null;
            selectedNodeOut = null;
        }

        internal virtual void OnClickRemoveNode(NodeView nodeView)
        {
            if (connections != null)
            {
                List<Connection> connectionsToRemove = new List<Connection>();

                for (int i = 0; i < connections.Count; i++)
                {
                    if (connections[i].InNode == nodeView || connections[i].OutNode == nodeView)
                    {
                        connectionsToRemove.Add(connections[i]);
                    }
                }

                for (int i = 0; i < connectionsToRemove.Count; i++)
                {
                    connections.Remove(connectionsToRemove[i]);
                }
            }

            Debug.Log("remove");
            nodesView.Remove(nodeView);
            if (nodeView == selectedNodeIn)
                selectedNodeIn = null;
            if (nodeView == selectedNodeOut)
                selectedNodeOut = null;
            hasUnsavedChanges = true;
        }


        internal virtual void OnDragNode(NodeView node)
        {
            isDragging = true;
            if (nodesView.Count > 1)
                SelectedNodes.ForEach(x =>
                {
                    if (x != node)
                        x.SetPosition(x.rect.position + Event.current.delta);
                });
            hasUnsavedChanges = true;
            isDragging = false;
        }



        internal virtual void OnClickNode(NodeView node)
        {
            if (!SelectedNodes.Contains(node)) SelectNode(node);
            else DeselectNode(node);
            if (node.IsSelected)
            {
                return;
            }
            DeselectAllNodes();
            SelectNode(node);
        }
        internal virtual void OnClickUpNode(NodeView node)
        {
            //Debug.Log("Click up on node " + node.rect.position);
            if (!isDragging && SelectedNodes.Count > 1)
            {
                DeselectAllNodes();
                SelectNode(node);
            }

            isDragging = false;
        }

        internal virtual void SelectNode(NodeView node)
        {
            node.IsSelected = true;
            GUI.changed = true;
        }

        internal virtual void DeselectNode(NodeView node)
        {
            node.IsSelected = false;
            GUI.changed = true;
        }
        internal virtual void DeselectAllNodes()
        {
            //Debug.Log($"Deselect {SelectedNodes.Count} nodes");
            SelectedNodes.ForEach(node => node.IsSelected = false);
            GUI.changed = true;
        }
        protected virtual void SelectAllNodes()
        {
            nodesView.ForEach(node => SelectNode(node));
            Repaint();
        }

        protected virtual void OnClickBackground()
        {
            DeselectAllNodes();
            ClearConnectionSelection();
        }
    }
}
