using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails;


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

    // one for each status and BehaviourNodeStatus
    enum Styles{standardNormal, standardSelected,
                failNormal, failSelected,
                runningNormal, runningSelected,
                sucessNormal, successSelected};
    private string [] mStylesNames_;
    private GUIStyle[] mStyle_;

    public BTGraphNodeStyle(string standardNormalStyleName, string standardSelectedStyleName, 
							string failNormalStyleName, string failSelectedStyleName,
							string runningNormalStyleName, string runningSelectedStyleName, 
							string successNormalStyleName, string successSelectedStyleName){
    
        mStylesNames_= new string[8];

        mStylesNames_[(int)Styles.standardNormal]  =standardNormalStyleName;  
        mStylesNames_[(int)Styles.standardSelected]  =standardSelectedStyleName;  
        
        mStylesNames_[(int)Styles.failNormal]  =failNormalStyleName;  
        mStylesNames_[(int)Styles.failSelected]  =failSelectedStyleName; 
        
        mStylesNames_[(int)Styles.runningNormal]  =runningNormalStyleName;  
        mStylesNames_[(int)Styles.runningSelected]  =runningSelectedStyleName; 
        
        mStylesNames_[(int)Styles.sucessNormal]  = successNormalStyleName;  
        mStylesNames_[(int)Styles.successSelected]  =successSelectedStyleName; 
    
    }

    public void CreateStyles(){
       
        mStyle_= new GUIStyle[8];
       
        for(int i=0;i<mStyle_.Length;i++){
            if(mStyle_[i]==null){
                mStyle_[i]=CreateStyle(mStylesNames_[i]);
            }
        }
    }

    private GUIStyle CreateStyle(string src){
        GUIStyle style= (GUIStyle)src;
        style.wordWrap=true;
        style.alignment = TextAnchor.UpperCenter;

		return style;
    }

    public GUIStyle GetStyle(BehaviourNodeStatus status, bool isSelected)
	{
        switch (status){
            case BehaviourNodeStatus.Failure:
                break;
                
            case BehaviourNodeStatus.Running:
                break;
            case BehaviourNodeStatus.Success:
                break;

            default:
                break;


        }
            return new GUIStyle();

    }

    public Vector2 GetSize(string content, BTEditorTreeLayout layout)
	{
        if(!string.IsNullOrEmpty(content))
		{
			
		}

		return new Vector2(200, 40);
    }

    private Vector2 GetVertical(string content)
	{
	        mContent_.text = content;
			Vector2 size = mStyle_[(int)Styles.standardNormal].CalcSize(mContent_);
			size.x = Mathf.Max(size.x, VERT_MAX_NODE_W);
			size.y = Mathf.Max(size.y, VERT_MAX_NODE_H);
		
        	if(size.x > HORZ_MIN_NODE_BORDER)
			{
				size.x = VERT_MAX_NODE_W;
				size.y = Mathf.Min(mStyle_[(int)Styles.standardNormal].CalcHeight(mContent_, size.x), VERT_MAX_NODE_H);
			}

			size.x += HORZ_MIN_NODE_BORDER;

			float snapSize = BTEditorCanvas.Current.SnapSize * 2;
			size.x = (float)Mathf.Round(size.x / snapSize) * snapSize;
			size.y = (float)Mathf.Round(size.y / snapSize) * snapSize;

			return size;
    }

    private Vector2 GetHorizontal(string content)
	{
            mContent_.text = content;
			Vector2 size = new Vector2();
			
			float snapSize = BTEditorCanvas.Current.SnapSize * 2;
            size.y = Mathf.Max(mStyle_[(int)Styles.standardNormal].CalcHeight(mContent_, HORZ_NODE_W), HORZ_NODE_H);
			size.x = (float)Mathf.Round(size.x / snapSize) * snapSize;
            
			return size;

    }

}
}