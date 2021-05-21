using UnityEngine;
using Trails;
using UnityEditor;


namespace TrailsEditor{

[CustomEditor(typeof(AIAgent))]
public class AIAgentInspector : Editor {
    public override void OnInspectorGUI() {
             DrawDefaultInspector();

			AIAgent agent = (AIAgent)target;
			BTAsset btAsset = agent.mBehaviourTree_;
			BehaviourTree mBTInstance = agent.mBTInstance_;

			GUI.enabled = btAsset != null;
			if(EditorApplication.isPlaying && mBTInstance != null)
			{
				if(GUILayout.Button("Preview", GUILayout.Height(24.0f)))
				{
					// BehaviourTreeEditor.OpenDebug(btAsset, btInstance);
				}
			}
			else
			{
				if(GUILayout.Button("Edit", GUILayout.Height(24.0f)))
				{
					BehaviourTreeEditor.Open(btAsset);
				}
			}
			GUI.enabled = true;
        
    }
    
}

}