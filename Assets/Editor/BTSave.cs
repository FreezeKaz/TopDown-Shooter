using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    [CreateAssetMenu(fileName = "New behavior tree", menuName = "Behavior Tree", order = 62)]
    public class BTSave : ScriptableObject
    {
        public static readonly string baseFolder = "Assets/Resources/Behavior Trees";
        public Node root;
        public List<Node> nodes = new List<Node>();

        public Node CreateNode(Type type)
        {
            Node node = CreateInstance(type) as Node;
            node.name = type.Name;

            nodes.Add(node);

            return node;
        }
    }
}
