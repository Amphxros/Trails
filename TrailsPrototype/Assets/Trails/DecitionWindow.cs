using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DecitionWindow : EditorWindow
{
    List<Node>openNodes;
    [MenuItem("Window/TrAIl/DecitionTree")]
    public static void ShowWindow()
    {
        GetWindow<DecitionWindow>("DecitionTree");
    }

    //window code
   void OnGUI()
   {
        GUILayout.Label("Decition", EditorStyles.boldLabel);

        if(GUILayout.Button("Create Node"))
        {
            Debug.Log("Node created");
        }

   }

    void AddNode()
    {
        openNodes.Add(new Node());
    }


}

public class Node: MonoBehaviour
{
    
    MonoBehaviour myAction;
    public Node()
    {

    }
    public Node(string mAction)
    {

    }

}
