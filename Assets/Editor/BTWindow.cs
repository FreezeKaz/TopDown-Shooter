using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEditor.UIElements;
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

        internal virtual void OnEnable()
        {
            PopulateView();
        }

        internal virtual void OnGUI()
        {
            DrawNodes();
            DrawConnections();
            DrawToolbar();
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

        #region Node

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
            return BTSave.CreateNode(type);
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

        internal virtual void DrawNodes()
        {
            if (nodesView == null) return;

            for (int i = 0; i < nodesView.Count; i++)
            {
                nodesView[i].Draw();
            }
        }

        #endregion

        #region Connection

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

        #endregion

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

        #region Action

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

        #endregion

        #region Save

        public string fileName = "New File";
        BTSave BTSave;
        public BTSave file;

        internal void PopulateView()
        {
            if (file == null) BTSave = CreateInstance<BTSave>();
            else BTSave = (file as BTSave).Clone();

            BTSave = file as BTSave;

            if (BTSave == null) BTSave = CreateInstance<BTSave>();

            nodesView = new List<NodeView>();

            if (BTSave.root == null) BTSave.root = (Root)BTSave.CreateNode(typeof(Root));
            //Vector2 rootPos = BTSave.root.child == null ? new Vector2(position.width * 0.5f, 100f) : BTSave.root.child.positionOnView + new Vector2(0, -50f);
            CreateNodeView(BTSave.root, new Vector2(50f,50f));

            for (int i = 0; i < BTSave.nodes.Count; i++)
            {
                CreateNodeView(BTSave.nodes[i].Clone(), BTSave.nodes[i].positionOnView);
            }
        }

        internal virtual void NewFile()
        {
            if (!UnsavedChangesCheck()) return;

            fileName = $"New {"Node Structure"}";
            file = default;
            nodesView = new List<NodeView>();
            connections = new List<Connection>();
            PopulateView();
            ClearConnectionSelection();
            hasUnsavedChanges = false;
        }

        internal virtual bool UnsavedChangesCheck()
        {
            if (hasUnsavedChanges)
            {
                int result = EditorUtility.DisplayDialogComplex("Unsaved Changes Detected", saveChangesMessage, "Yes", "Cancel", "Discard");
                switch (result)
                {
                    case 0:
                        // Yes
                        if (this is BTWindow)
                        {
                            (this as BTWindow).Save(); // Vies//
                        }
                        else
                        {
                            Save(); // TODO: Does not call overridden method
                        }
                        break;
                    case 1:
                        // Cancel
                        return false;
                    case 2:
                        // Discard
                        break;
                    default:
                        break;
                }
            }
            return true;
        }

        public virtual void Save()
        {
            if (!hasUnsavedChanges) return;
            UnityEngine.Debug.Log("Nodes saved: " + BTSave.nodes.Count);
            BTSave btFile = file as BTSave;
            if (btFile == null)
            {
                btFile = BTSave.SaveToNew(fileName);
            }
            else
            {
                BTSave.SaveTo(ref btFile);
            }
            file = btFile;
            foreach (var nodeView in nodesView)
            {
                nodeView.node.positionOnView = new Vector2(nodeView.rect.position.x, nodeView.rect.position.y);
                List<Connection> outgoingConnections = connections.FindAll(c => c.OutNode == nodeView);
                // Get all the child nodes from those connections
                List<NodeView> childNodes = outgoingConnections.ConvertAll(c => c.InNode);
                foreach (var childNode in childNodes)
                {
                    Node childNodeInTree = BTSave.nodes.Find(n => n.guid == childNode.node.guid);
                    if (childNodeInTree != null)
                    {
                        nodeView.node.Attach(childNodeInTree);
                    }
                }
                //Debug.Log(nodeView.node.name)
            }



            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }
            if (!AssetDatabase.IsValidFolder("Assets/Resources/Nodes"))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "Nodes");
            }
            if (string.IsNullOrWhiteSpace(AssetDatabase.GetAssetPath(file)))
            {
                string assetPath = AssetDatabase.GenerateUniqueAssetPath($"Assets/Resources/Nodes/{fileName}.asset");
                AssetDatabase.CreateAsset(file, assetPath);
            }
            SaveChanges();



            Load(file);
        }

        public override void SaveChanges()
        {
            EditorUtility.SetDirty(file);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = file;
            EditorGUIUtility.PingObject(Selection.activeObject);
            // TODO: test if assetdatabase refresh is needed & if it has great performance impact in large projects

            //Debug.Log("Nodes saved successfully to file " + file.name, file);
            base.SaveChanges();
        }

        public virtual bool Load(BTSave structure)
        {
            if (!UnsavedChangesCheck()) return false;
            if (structure == null)
            {
                NewFile();
                return true;
            }

            nodesView.Clear();
            connections.Clear();

            file = structure;
            hasUnsavedChanges = false;
            ClearConnectionSelection();
            return true;
        }

        #endregion

        #region ToolBar

        private float toolbarHeight = 30f;
        private float toolbarButtonWidth = 50f;



        public void DrawToolbar()
        {
            Rect menuBarRect = new Rect(0, 0, position.width, toolbarHeight);
            GUILayout.BeginArea(menuBarRect, EditorStyles.toolbar);
            GUILayout.BeginHorizontal();

            if (file == null)
                fileName = EditorGUILayout.TextField(fileName, GUILayout.MinWidth(50), GUILayout.MaxWidth(150));
            BTSave oldReference = file;
            EditorGUI.BeginChangeCheck();
            BTSave newReference = (BTSave)EditorGUILayout.ObjectField(file, typeof(BTSave), false, GUILayout.MinWidth(50), GUILayout.MaxWidth(150));
            if (EditorGUI.EndChangeCheck())
            {
                Debug.Log("New file selected");
                if (Load(newReference))
                {
                    file = newReference;
                }
                else
                {
                    file = oldReference;
                }
            }

            if (!hasUnsavedChanges) GUI.enabled = false;
            if (GUILayout.Button("Save", EditorStyles.toolbarButton, GUILayout.Width(toolbarButtonWidth)))
            {
                Save();
            }
            GUI.enabled = true;
            if (file == null && !hasUnsavedChanges)
            {
                GUI.enabled = false;
            }
            if (GUILayout.Button("New", EditorStyles.toolbarButton, GUILayout.Width(toolbarButtonWidth)))
            {
                NewFile();
            }
            GUI.enabled = true;


            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }



        #endregion
    }
}
