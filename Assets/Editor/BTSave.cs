using System;
using System.Collections;
using System.Collections.Generic;
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

        public Node CreateNode(Type type)
        {
            Node node = CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            Debug.Log(node.name);

            if (type != typeof(Root))
            {
                nodes.Add(node);
            }
            else
            {
                root = node as Root;
            }

            Debug.Log(nodes.Count);

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            return node;
        }

        public BTSave Clone()
        {
            BTSave tree = Instantiate(this);
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
