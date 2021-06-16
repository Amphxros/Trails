using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails;
using UnityEditor;
using System;

namespace TrailsEditor {
    public class BTEditorGraph : ScriptableObject {
        private const int SELECT_MOUSE_BUTTON = 0;
        private const int CONTEXT_MOUSE_BUTTON = 1;
        private List<BTEditorGraphNode> mSelection_;
        private BTEditorGraphNode mMasterRoot_;
        private Stack<BTEditorGraphNode> mRootStack_;

        private bool mDrawSelectionBox_;
        private bool mBehaviourTreeIsReadOnly_ = false;
        private bool mCanBeginBoxSelection_;
        private Vector2 mSelectionBoxPos_;

        public bool ReadOnly {
            get {
                return mBehaviourTreeIsReadOnly_;
            }
        }

        public int Depth {
            get {
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

        private void OnCreated() {
            mMasterRoot_ = null;
            mRootStack_ = new Stack<BTEditorGraphNode>();
            mDrawSelectionBox_ = false;
            mBehaviourTreeIsReadOnly_ = false;
        }

        private void OnDestroy() {
            BTEditorGraphNode.DestroyImmediate(mMasterRoot_);
            mMasterRoot_ = null;
            if (mRootStack_ != null)
                mRootStack_.Clear();
        }

        public void SetBehaviourTree(BehaviourTree bT) {

                mBehaviourTreeIsReadOnly_ = bT.isReadOnly;
                mMasterRoot_ = BTEditorGraphNode.CreateRoot(this, bT.Root);
                mRootStack_.Push(mMasterRoot_);
            
        }

        public void DrawGUI(Rect dest)
        {
            if (WorkingRoot == null)
            {
                return;
            }

            WorkingRoot.Update();
            WorkingRoot.Draw();
            DrawSelectionBox();
            HandleEvents(dest);

        }
        private void DrawSelectionBox()
        {
            if (mDrawSelectionBox_) {
                Vector2 mousePosition = BTEditorCanvas.Current.Event.mousePosition;
                Rect pos = new Rect();
                pos.x = 0;
                pos.y = 0;
                pos.width = 0;
                pos.height = 0;

                GUI.Box(pos, "", BTEditorStyle.SelectionBox);
                BTEditorCanvas.Current.Repaint();

                SelectionBox = new Rect(BTEditorCanvas.Current.WindowSpaceToCanvasSpace(pos.position), pos.size);


            }
            else {
                SelectionBox = null;
            }

        }

        private void HandleEvents(Rect dest)
        {
            if (dest.Contains(BTEditorCanvas.Current.Event.mousePosition)) {
                switch (BTEditorCanvas.Current.Event.type) {
                    case EventType.MouseDown:

                        if (BTEditorCanvas.Current.Event.button == SELECT_MOUSE_BUTTON) {
                            ClearSelection();
                            mCanBeginBoxSelection_ = true;
                            mSelectionBoxPos_ = BTEditorCanvas.Current.Event.mousePosition;
                            BTEditorCanvas.Current.Event.Use();
                        }

                        break;

                    case EventType.MouseUp:

                        if (BTEditorCanvas.Current.Event.button == SELECT_MOUSE_BUTTON)
                        {
                            if (dest.Contains(BTEditorCanvas.Current.Event.mousePosition))
                            {

                                mDrawSelectionBox_ = false;
                                BTEditorCanvas.Current.Event.Use();

                            }
                        }

                        else if (BTEditorCanvas.Current.Event.button == CONTEXT_MOUSE_BUTTON)
                        {
                            GenericMenu menu = BTContextMenuFactory.CreateGraphContextMenu(this);
                            menu.DropDown(new Rect(BTEditorCanvas.Current.Event.mousePosition, Vector2.zero));
                            BTEditorCanvas.Current.Event.Use();

                        }
                        mCanBeginBoxSelection_ = false;
                        break;

                    case EventType.MouseDrag:
                        if (!mDrawSelectionBox_ && mCanBeginBoxSelection_)
                        {
                            mDrawSelectionBox_ = true;
                        }

                        BTEditorCanvas.Current.Event.Use();

                        break;

                }
            }

        }

        /////////////////////
        //// Node Groups ////
        /////////////////////

        public void OnPushNodeGroup(BTEditorGraphNode node)
        {

            if (node != null && node.Node is NodeGroup)
            {
                mRootStack_.Push(node);
                SelectSingle(node);
            }

        }

        public void OnPopNodeGroup() {
            if(mRootStack_.Count > 1){
                var oldRoot = mRootStack_.Pop();
                SelectNodeGroup(oldRoot);
                //BTUndoSystem.registerUndo(new UndoNodeGroupPop(oldRool));
            }

        }

        ////////////////////////
        // Seleccion de nodos //
        ////////////////////////

        public void OnNodeSelect(BTEditorGraphNode node) {

            if (BTEditorCanvas.Current.Event.shift && (node.Node is Composite || node.Node is Decorator)) {
                // seleccionar hijos y padre
            }
            else if (BTEditorCanvas.Current.Event.control || SelectionBox.HasValue) {
                if (mSelection_.Contains(node)) {
                    if (node.Node is NodeGroup && !IsRoot(node)) {
                        SelectNodeAdditive(node);
                    }
                    else {
                        SelectNodeGroup(node);
                    }
                }
            }
            else {
                if (node.Node is NodeGroup && !IsRoot(node)) {
                    SelectNodeGroup(node);
                }

                else{
                    SelectSingle(node);
                }
            }
        }
        
        public void OnNodeDeselect(BTEditorGraphNode node) {

			if(mSelection_.Remove(node))    node.OnDeselected();

        }


        /////////////////////
        //// Mover nodos ////
        /////////////////////
        
        public void OnNodeBeginDrag(BTEditorGraphNode node, Vector2 pos) {
            if (!mSelection_.Contains(node)) {
                return;
            }
            // drag the rest of the tree
            for (int i = 0; i < mSelection_.Count; i++) {
                mSelection_[i].OnBeginDrag(pos);
            }

        }
       
        public void OnNodeDrag(BTEditorGraphNode node, Vector2 pos, bool recursive = false) {
            if (!mSelection_.Contains(node)) {
                return;
            }
            // drag the rest of the tree
            for (int i = 0; i < mSelection_.Count; i++) {
                mSelection_[i].OnDrag(pos);
            }
        }
       
        public void OnNodeEndDrag(BTEditorGraphNode node) {
            if (!mSelection_.Contains(node)) {
                return;
            }

            for (int i = 0; i < mSelection_.Count; i++) {
                mSelection_[i].OnEndDrag();
            }

        }
        
        ////////////////////////
        //// Branch selects ////
        ////////////////////////
        
        // Aux de limpieza
        private void ClearSelection() {
            for(int i=0;i<mSelection_.Count;i++){
               mSelection_[i].OnDeselected();
            }
            
			mSelection_.Clear();
        }


        private void SelectGraph(){
            ClearSelection();
            SelectBranchRecursive(WorkingRoot);
        }
        

        private void SelectBranch(BTEditorGraphNode parent){
            ClearSelection();
            SelectBranchRecursive(parent);
        }
        
        private void SelectBranchAdditive(BTEditorGraphNode parent){

            SelectBranchRecursive(parent);
        }
        
        private void SelectBranchRecursive(BTEditorGraphNode node){
            mSelection_.Add(node);
            node.OnSelected();

            for(int i=0; i<node.ChildCount; i++){
                SelectBranchRecursive(node.GetChild(i));
            }
        }
		
        private void SelectNodeGroup(BTEditorGraphNode node){
            SelectBranch(node);
            //node.OnNodeSelect();
        }
        
        private void SelectNodeAdditive(BTEditorGraphNode node){
            mSelection_.Add(node);
			//node.OnNodeSelect();
        }

		private void SelectSingle(BTEditorGraphNode node)
		{
			ClearSelection();
			mSelection_.Add(node);
			//node.OnNodeSelect();
		}

		private void SelectSingleAdditive(BTEditorGraphNode node)
		{
			mSelection_.Add(node);
			//node.OnNodeSelect();
		}

        public bool IsRoot(BTEditorGraphNode node)
        {
            	if (WorkingRoot != null)	Debug.Log("this completo");
				if (WorkingRoot == null)	Debug.Log("this invalido");
            return node == WorkingRoot;
        }


        public void OnNodeCreateChild(BTEditorGraphNode parent, Type childType)
        {
            if (parent != null && childType != null)
            {
                BTEditorGraphNode child = parent.OnCreateChild(childType);
                if (child != null)
                {

                }
            }
        }


        public static BTEditorGraph Create()
        {
            BTEditorGraph graph = ScriptableObject.CreateInstance<BTEditorGraph>();
            graph.OnCreated();
            graph.hideFlags = HideFlags.HideAndDontSave;
            graph.mSelection_ = new List<BTEditorGraphNode>();

            return graph;

        }


    }
}