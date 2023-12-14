using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace BehaviorTree
{
    public class BTWindow : EditorWindow
    {
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

            if(GUI.changed)
            {
                Repaint();
            }
        }


        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 1)
                    {
                        GenericMenu genericMenu = new GenericMenu();
                        var nodeType = TypeCache.GetTypesDerivedFrom<Node>();
                        foreach(var type in nodeType)
                        {
                            genericMenu.AddItem(new GUIContent("Actions/" + type.Name), false, () => CreateNodeViewLotre(e.mousePosition));
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

        internal virtual void DrawNodes()
        {
            if (nodes == null) return;

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Draw();
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

        internal virtual NodeView CreateNodeViewLotre(Vector2 position)
        {
            position.x -= 200 * .5f;
            position.y -= 50 * .5f;
            NodeView nodeView = new NodeView(new Rect(position,  new Vector2(200, 50)),
                new ConnectionPoint(new Vector2(10f,10f), ConnectionPointType.In, OnClickConnectionPoint),
                new ConnectionPoint(new Vector2(10f, 10f), ConnectionPointType.Out, OnClickConnectionPoint),
                OnClickConnectionPoint);
            AddNodeView(nodeView);
            return nodeView;
            
        }


        internal NodeView selectedNodeIn;
        internal NodeView selectedNodeOut;
        public List<NodeView> nodes = new List<NodeView>();
        public List<Connection> connections = new List<Connection>();


        internal virtual void AddNodeView(NodeView nodeView)
        {
            if (nodes == null) nodes = new List<NodeView>();
            nodes.Add(nodeView);
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
            int indexOfNodeOut = nodes.IndexOf(nodeOut);
            if (indexOfNodeOut < 0) return;
            int indexOfNodeIn = nodes.IndexOf(nodeIn);
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
            foreach(var connection in connections)
            {
                if(connection.OutNode == nodeView)
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
    }
}
