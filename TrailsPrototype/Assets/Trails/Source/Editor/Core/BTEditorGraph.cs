using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails;
using UnityEditor;

namespace TrailsEditor{
    public class BTEditorGraph : ScriptableObject{
        private const int SELECT_MOUSE_BUTTON = 0;
		private const int CONTEXT_MOUSE_BUTTON = 1;
		private List<BTEditorGraphNode> mSelection_;
        private BTEditorGraphNode mMasterRoot_;
        private Stack<BTEditorGraphNode> mRootStack_;

        private bool mDrawSelectionBox_;
        private bool mBehaviourTreeIsReadOnly_=false;
        private bool mCanBeginBoxSelection_;
        private Vector2 mSelectionBoxPos_;

        public bool ReadOnly{
            get{
            return mBehaviourTreeIsReadOnly_;
            }
        }

        public int Depth{
            get{
                return Mathf.Max(mRootStack_.Count - 1, 0);
            }
        }

		public Rect? SelectionBox { get; set; }

        private BTEditorGraphNode WorkingRoot
		{
			get
			{
				return mRootStack_.Peek();
			}
		}

        private void OnCreated(){
            mMasterRoot_=null;
            mRootStack_=new Stack<BTEditorGraphNode>();
            mDrawSelectionBox_= false;
            mBehaviourTreeIsReadOnly_=false;
        }

         private void OnDestroy() {
            BTEditorGraphNode.DestroyImmediate(mMasterRoot_);
            mMasterRoot_=null;
            if(mRootStack_!=null)
                mRootStack_.Clear();
        }
        public void SetBehaviourTree(BehaviourTree bT){
            if(mMasterRoot_!=null){

            }
            mBehaviourTreeIsReadOnly_=bT.isReadOnly;
            // mMasterRoot_ = BTEditorGraphNode.CreateRoot(this, behaviourTree.Root);
            mRootStack_.Push(mMasterRoot_);

        }












	    public bool IsRoot(BTEditorGraphNode node)
		{
			return node == WorkingRoot;
		}



    }
}