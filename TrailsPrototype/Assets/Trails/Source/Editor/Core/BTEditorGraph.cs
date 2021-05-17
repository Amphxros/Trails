using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;
using Trails;


namespace Trails.Editor{
public class BTEditorGraph : ScriptableObject {
    private const int SELECT_MOUSE_BUTTON = 0;
	private const int CONTEXT_MOUSE_BUTTON = 1;
    protected List<BTEditorGraphNode> mSelection_;
    protected BTEditorGraphNode mMasterRoot_;
    protected Stack<BTEditorGraphNode> mRootStack_;

    protected bool mDrawSelectionBox_;
	protected bool mIsBehaviourTreeReadOnly_;
	protected bool mCanBeginBoxSelection_;
	protected Vector2 mSelectionBoxStartPos_;
    
   public bool ReadOnly
	{
		get{
			return mIsBehaviourTreeReadOnly || EditorApplication.isPlaying;
		}
	}

	public int Depth{
		get{
			return Mathf.Max(mRootStack.Count - 1, 0);
		}
	}

    public Rect? SelectionBox { get; set; }

    protected BTEditorGraphNode WorkingRoot
	{
		get{
			return mRootStack_.Peek();
		}
	}

	protected void OnCreated(){
			mMasterRoot_ = null;
			mRootStack_ = new Stack<BTEditorGraphNode>();
			mDrawSelectionBox_ = false;
			mIsBehaviourTreeReadOnly_ = false;
			mSelectionBoxStartPos_ = Vector2.zero;
			SelectionBox = null;
	}
    protected void OnDestroy(){
        
    }


}
}