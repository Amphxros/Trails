using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails;
using UnityEditor;

namespace Trails.Editor{

public class BehaviourTreeEditor : EditorWindow {
   
   [SerializeField]
   private Texture mTextureGrid_;
   
   [SerializeField]
   private BTAsset mBTAsset_;

    [SerializeField]
   // private BTNavigationHistory mNavigationHistory_;


    private BTEditorGrid mGrid_;
	 private BTEditorGraph mGraph_;
	private BTEditorCanvas mCanvas_;


    private bool isDisposed_;

     private void OnEnable() {
        if(mTextureGrid_==null){
            mTextureGrid_= Resources.Load<Texture>("Trails/EditorGUI/background");

        }

        if(mCanvas_==null){

        }
        
        if(mGrid_==null){

        }
        
        if(mGraph_==null){
            
        }


    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
}