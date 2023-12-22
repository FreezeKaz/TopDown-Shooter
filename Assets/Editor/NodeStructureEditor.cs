using System.Collections.Generic;
using UnityEditor;
using System;
using UnityEditor.Callbacks;
using BehaviorTree;

[CustomEditor(typeof(Node))]
public class NodeStructureEditor : Editor
{
    public static Dictionary<Type, string> typedEditorNames = new Dictionary<Type, string>
    {
        {typeof(Node), "Node editor" },
        {typeof(BTSave), "Behavior tree" }
    };

    [OnOpenAsset()]
    public static bool Open(int instanceID, int line)
    {
        UnityEngine.Object asset = EditorUtility.InstanceIDToObject(instanceID);
        BTSave behaviorTree = asset as BTSave;
        if (behaviorTree)
        {
            var editor = BTWindow.OpenBTWindow();
            editor.Load(behaviorTree);
            string name = EditorUtility.InstanceIDToObject(instanceID).name;
            return true;
        }

        Node nodeStructure = asset as Node;
        if (nodeStructure)
        {
            var editor = BTWindow.OpenBTWindow();
            editor.Load(asset as BTSave);
            string name = EditorUtility.InstanceIDToObject(instanceID).name;
            return true;
        }
        return false;
    }
}
