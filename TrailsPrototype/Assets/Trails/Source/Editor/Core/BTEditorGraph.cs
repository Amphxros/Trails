using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails;
using UnityEditor;

namespace Trails.Editor{
    public class BTEditorGraph : ScriptableObject{
        private const int SELECT_MOUSE_BUTTON = 0;
		private const int CONTEXT_MOUSE_BUTTON = 1;
		private List<BTEditorGraphNode> mSelection_;
        private BTEditorGraphNode mMasterRoot_;
        private Stack<BTEditorGraphNode> mRootStack_;

        private bool mDrawSelectionBox_;
        private bool mBehavivourTreeIsReadOnly_=false;
        private bool mCanBeginBoxSelection_;
        private Vector2 mSelectionBoxPos_;

        public bool ReadOnly{
            get{
            return mBehavivourTreeIsReadOnly_;
            }
        }

        public int Depth{
            get{
                return Mathf.Max(mRootStack_.Count - 1, 0);
            }
        }

		public Rect? SelectionBox { get; set; }

        private void OnCreated(){
            mMasterRoot_=null;
            mRootStack_=new Stack<BTEditorGraphNode>();
            mDrawSelectionBox_= false;
            mBehavivourTreeIsReadOnly_=false;
        }

         private void OnDestroy() {
         
        }

    }
}