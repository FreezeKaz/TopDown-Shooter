using UnityEngine;
using BehaviorTree;
using Unity.VisualScripting;

public class BTApp : BehaviorTree.Tree
{
    [SerializeField] private BTSave _save;
    public Transform GO;
    Node root;
    //public Transform[] waypoints;

    private void Start()
    {
        GO = GetComponent<Transform>();
        root = _save.root;
        applyChildren(root);
    }
    public void applyChildren(Node node)
    {
        foreach (Node child in node.children)
        {
            if (child != null)
            {
                if(child.type == NodeType.TASK)
                {
                    child.GO = GO;
                }
                else
                {
                    applyChildren(child);
                }
            }
        }
    }
    protected override Node SetupTree()
    {
        Debug.Log(root.children.Count);
        return root;
    }
}
