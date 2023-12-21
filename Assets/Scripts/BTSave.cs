using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace BehaviorTree
{
    [CreateAssetMenu(fileName = "New behavior tree", menuName = "Behavior Tree", order = 82)]
    public class BTSave : ScriptableObject
    {
        public static readonly string baseFolder = "Assets/Resources/Behavior Trees";
        public Root root;
        public List<Node> nodes = new List<Node>();

        public NodeState state = NodeState.RUNNING;


        public NodeState BTUpdate(BTApp app)
        {
            switch (state)
            {
                case NodeState.RUNNING:
                    state = root.BTUpdate(app);
                    break;
            }
            return state;
        }

        public Node CreateNode(Type type)
        {
            Node node = CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();

            if (type != typeof(Root))
            {
                nodes.Add(node);
            }
            else
            {
                root = node as Root;
            }

            EditorUtility.SetDirty(this); 
            AssetDatabase.SaveAssets();
            //Debug.Log(nodes[0]);
            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
        }

        public static string EnsureFolderIsInAssets(params string[] folders)
        {
            string completeFolderPath = "Assets";
            for (int i = 0; i < folders.Length; i++)
            {
                if (!AssetDatabase.IsValidFolder(completeFolderPath + "/" + folders[i]))
                {
                    AssetDatabase.CreateFolder(completeFolderPath, folders[i]);
                    Debug.Log("Folder created: " + completeFolderPath);
                }

                completeFolderPath += "/" + folders[i];
            }
            return completeFolderPath + "/";
        }

        public BTSave SaveToNew(string name)
        {
            BTSave treeFile = this;

            // Create a new file with a unique asset path
            if (string.IsNullOrWhiteSpace(name)) name = "New Behavior Tree";
            string folderPath = EnsureFolderIsInAssets("Resources", "Behavior Trees");
            string completePath = folderPath + name + ".asset";
            string uniquePath = AssetDatabase.GenerateUniqueAssetPath(completePath);
            AssetDatabase.CreateAsset(this, uniquePath);
            EditorUtility.SetDirty(this);

            Debug.Log(treeFile);

            if (root == null)
            {
                UnityEngine.Debug.LogError("Root is null");
                root = CreateInstance<Root>();
            }
            AssetDatabase.AddObjectToAsset(root, treeFile);

            foreach (var node in nodes)
            {
                AssetDatabase.AddObjectToAsset(node, treeFile);
                continue;
            }

            SaveTo(ref treeFile);
            return treeFile;
        }

        public void SaveTo(ref BTSave treeFile)
        {
            Debug.Log(treeFile);
            treeFile.root.CopyData(root);

            // Go through all of the behavior tree's nodes and add them to the asset if they have not been added yet
            foreach (var node in nodes)
            {
                // Try and find a node with the same GUID in the tree file
                int nodeIndex = treeFile.nodes.FindIndex(n => n.guid == node.guid);
                if (nodeIndex == -1)
                {
                    // If not present, add it to the tree file
                    treeFile.nodes.Add(node);
                    AssetDatabase.AddObjectToAsset(node, treeFile);
                }
                else
                {
                    // If present, update
                    treeFile.nodes[nodeIndex].CopyData(node);
                }
            }

            UnityEngine.Object[] nodeAssets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(treeFile));
            for (int i = 0; i < nodeAssets.Length; i++)
            {
                UnityEngine.Debug.Log("File found: " + nodeAssets[i].name);
                if (nodeAssets[i].GetType() == typeof(Root))
                {
                    UnityEngine.Debug.Log("Skip root node");
                    continue;
                }

                Node nodeAsset = nodeAssets[i] as Node;

                if (nodeAsset == null)
                {
                    UnityEngine.Debug.Log("Could not convert to node?");
                    continue;
                }
                else if (nodes.Exists(node => node.guid == nodeAsset.guid))
                {
                    UnityEngine.Debug.Log(nodeAssets[i] + " found in nodes list. Skipping...");
                    continue;
                }

                UnityEngine.Debug.Log(nodeAssets[i].name + " no longer exists.");
                AssetDatabase.RemoveObjectFromAsset(nodeAssets[i]);
                //treeFile.DeleteNode(nodeAsset);
            }

            // Create a root node if not yet present. Update it if there is
            if (string.IsNullOrWhiteSpace(AssetDatabase.GetAssetPath(treeFile.root)))
            {
                if (treeFile.root != null)
                {
                    AssetDatabase.AddObjectToAsset(treeFile.root, treeFile);
                }
                else
                {
                    AssetDatabase.AddObjectToAsset(root, treeFile);
                }
            }

            // Match all the nodes with children with the children in the file
            // First, the root node

            // Then every other node
            foreach (Node nodeAsset in treeFile.nodes)
            {
                List<Node> childNodes = nodeAsset.children;
                List<Node> newChildList = new List<Node>();
                if (childNodes != null)
                {
                    childNodes = childNodes.OrderBy(x => x.positionOnView.x).ToList();
                    for (int i = 0; i < childNodes.Count; i++)
                    {
                        Node childNode = childNodes[i];
                        Node childNodeInFile = treeFile.nodes.Find(n => n.guid == childNode.guid);
                        if (childNodeInFile != null)
                        {
                            // Override the node reference
                            childNode = childNodeInFile;
                        }
                        else
                        {
                            AssetDatabase.AddObjectToAsset(childNode, treeFile);
                        }
                        newChildList.Add(childNode);
                    }
                }
            }
            AssetDatabase.SaveAssets();
        }

        public BTSave Clone()
        {
            BTSave tree =  ScriptableObject.CreateInstance<BTSave>();
            for (int i = 0; i < tree.nodes.Count; i++)
            {
                if (tree.nodes[i] == null)
                {
                    UnityEngine.Debug.LogError("Node at position " + i + " of " + name + " is missing. Removing...", this);
                }
                tree.nodes[i] = tree.nodes[i].Clone();
            }
            if (tree.root == null) tree.root = (Root)CreateNode(typeof(Root));
            tree.root = (Root)tree.root.Clone();
            return tree;
        }
    }
}
