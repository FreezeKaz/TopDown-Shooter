using UnityEngine;
using BehaviorTree;
using Unity.VisualScripting;

public class BTApp : BehaviorTree.Tree
{
    [SerializeField] private BTSave _save;
    public GameObject GO;
    Node root;
    //public Transform[] waypoints;

    private void Start()
    {
        GO = GetComponent<GameObject>();
        root = _save.root;
        
    }
    public void applyChildren(Node node)
    {
        foreach (Node child in node.children)
        {
            if (child != null)
            {
                //if(verif type)
                if(child.GetType() == typeof(ActionNode))
                {
                    //(ActionNode)child.GO = GO;
                }

                //else
                applyChildren(child);
            }
        }
    }
    protected override Node SetupTree()
    {
        
        Debug.Log(root.children.Count);
        return root;
    }
}
