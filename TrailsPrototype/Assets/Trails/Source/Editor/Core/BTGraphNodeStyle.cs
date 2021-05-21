using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrailsEditor{
public class BTGraphNodeStyle
{
    // NODES CONSTS
    
    // Vertical consts
    const float VERT_MAX_NODE_W=180;
    const float VERT_MAX_NODE_H=200;
    const float VERT_MIN_NODE_W=60;
    const float VERT_MIN_NODE_H=40;

    //Horizontal consts
    const float HORZ_NODE_W=180;
    const float HORZ_NODE_H=200;
    const float HORZ_MIN_NODE_BORDER=60;

    private static GUIContent mContent_= new GUIContent();

    // none node status
    private GUIStyle mStandardNormal_;
    private GUIStyle mStandardSelected_;
    
    // fail status
    private GUIStyle mFailNormal_;
    private GUIStyle mFailSelected_;
    
    // running status
    private GUIStyle mRunningNormal_;
    private GUIStyle mRunningSelected_;

    // sucess status
    private GUIStyle mSuccessNormal_;
	private GUIStyle mSuccessSelected_;


    public BTGraphNodeStyle(){
        
    }
   
   
}
}