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
         mGraph_= new BTEditorGraph();   
        }



    }
    
    private void OnDisable(){
			Dispose();
	
    }

    private void OnDestroy() {
        Dispose(); 
    }

    private void Dispose(){
        
        if(!isDisposed_){
            if(mGraph_!=null){
                BTEditorGraph.DestroyImmediate(mGraph_);
                mGraph_=null;
            }

            if(mBTAsset_!=null){
                
                mBTAsset_.Dispose();
            }

            isDisposed_=true;

        }
    }

    private void SetBTAsset(BTAsset asset, bool clearNavigationHistory){
        if(asset!=null && (clearNavigationHistory || asset!=mBTAsset_)){
            if(mBTAsset_!=null){
                mBTAsset_.Dispose();
                mBTAsset_=null;
            }

            BehaviourTree mBT_=asset.GetEditModeTree();
            if(mBT_!=null){
                mBTAsset_=asset;
            

            if(clearNavigationHistory){
                
            }
            }
            else{

            }


        }
        
        else{

        }
    }
    private void CrashEditor(string message){
			
	}
    public void CreateNewBehaviourTree(){
    
    }
    
    }
}