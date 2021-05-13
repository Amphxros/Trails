using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails;
using UnityEditor;

namespace Trails.Editor{

public class BehaviourTreeEditor : EditorWindow {
   
   [SerializeField]
   private Texture mGrid_;
   
   [SerializeField]
   private BTAsset mBTAsset_;

    [SerializeField]
   // private BTNavigationHistory mNavigationHistory_;


        //private BTEditorGrid m_grid;
		//private BTEditorGraph m_graph;
		//private BTEditorCanvas m_canvas;


private bool isDisposed_;

     private void OnEnable() {
        if(mGrid_==null){
            mGrid_= Resources.Load<Texture>("Trails/EditorGUI/background");

        }    

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
}