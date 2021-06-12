using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using Trails;

namespace TrailsEditor{
    
    public class BTEditorGraphNode : ScriptableObject {
   private const int DRAG_MOUSE_BUTTON = 0;
		private const int SELECT_MOUSE_BUTTON = 0;
		private const int CONTEXT_MOUSE_BUTTON = 1;
		private const float DOUBLE_CLICK_THRESHOLD = 0.4f;

		private List<BTEditorGraphNode> mChildren_;
		private BehaviourNode mNode_;
		private BTEditorGraphNode mParent_;
		private BTEditorGraph mGraph_;
		private Vector2 m_dragOffset;
		private float? m_lastClickTime;
		private bool m_isSelected;
		private bool m_isDragging;
		private bool m_canBeginDragging;
		
		public BehaviourNode Node
		{
			get { return mNode_; }
		}

		public BTEditorGraphNode Parent
		{
			get { return mParent_; }
		}

		public BTEditorGraph Graph
		{
			get { return mGraph_; }
		}

		public int ChildCount
		{
			get { return mChildren_.Count; }
		}

		public bool IsRoot
		{
			get { return mGraph_.IsRoot(this); }
		}

		public Vector2 NodePositon
		{
			get { return mNode_.Position; }
			set
			{
				if(!IsRoot)
				{
					mNode_.Position = value;
				}
			}
		}

		private bool CanUpdateChildren
		{
			get{
			return false;
			}
		}

		private bool CanDrawChildren
		{
			get
			{
			return false;
			}
		}
		
		private void OnCreated()
		{
			if(mChildren_ == null)
			{
				mChildren_ = new List<BTEditorGraphNode>();
			}
			
			m_isSelected = false;
			m_isDragging = false;
			m_canBeginDragging = false;
			m_dragOffset = Vector2.zero;
			m_lastClickTime = null;
		}

		public void Update()
		{
			if(CanUpdateChildren)
				UpdateChildren();

			HandleEvents();
		}

		private void UpdateChildren()
		{
			for(int i = mChildren_.Count - 1; i >= 0; i--)
			{
				mChildren_[i].Update();
			}
		}

		private void HandleEvents()
		{
			
		}

		public void Draw()
		{
			if(CanDrawChildren)
				DrawTransitions();

			DrawSelf();

			if(CanDrawChildren)
				DrawChildren();
		}

		private void DrawTransitions()
		{
		}

		private void DrawSelf()
		{
		
		}

		private void DrawChildren()
		{
			
		}

		public void OnSelected()
		{
			m_isSelected = true;
			Selection.activeObject = this;
			BTEditorCanvas.Current.Repaint();
		}

		public void OnDeselected()
		{
			m_isSelected = false;
			m_isDragging = false;
			if(Selection.activeObject == this)
			{
				Selection.activeObject = null;
			}
			BTEditorCanvas.Current.Repaint();
		}

		public void OnBeginDrag(Vector2 position)
		{
			m_dragOffset = position - NodePositon;
			m_isDragging = true;
		}

		public void OnDrag(Vector2 position)
		{
			Vector2 nodePos = position - m_dragOffset;
			if(BTEditorCanvas.Current.SnapToGrid)
			{
				float snapSize = BTEditorCanvas.Current.SnapSize;
				nodePos.x = (float)Math.Round(nodePos.x / snapSize) * snapSize;
				nodePos.y = (float)Math.Round(nodePos.y / snapSize) * snapSize;
			}

			NodePositon = nodePos;

			BTEditorCanvas.Current.RecalculateSize(NodePositon);
			BTEditorCanvas.Current.Repaint();
		}

		public void OnEndDrag()
		{
			m_isDragging = false;
		}

		private void OnDoubleClicked()
		{
			
		}

		private void SetExistingNode(BehaviourNode node)
		{
			
		}

		private void ShowContextMenu()
		{
			
		}

		public BTEditorGraphNode OnCreateChild(Type type)
		{
			return null;
		}

		public BTEditorGraphNode OnCreateChild(BehaviourNode node)
		{
			return null;
		}

		public BTEditorGraphNode OnInsertChild(int index, Type type)
		{
		
			return null;
		}

		public BTEditorGraphNode OnInsertChild(int index, BehaviourNode node)
		{
			return null;
		}

		public void OnDelete()
		{
			if(mParent_ != null)
			{
				mParent_.RemoveChild(this);
				BTEditorGraphNode.DestroyImmediate(this);
			}
		}

		public void OnDeleteChild(int index)
		{
		
		}

		public int GetChildIndex(BTEditorGraphNode child)
		{
			return mChildren_.IndexOf(child);
		}

		public void ChangeChildIndex(int sourceIndex, int destinationIndex)
		{
		
		}

		public BTEditorGraphNode GetChild(int index)
		{
			if(index >= 0 && index < mChildren_.Count)
			{
				return mChildren_[index];
			}

			return null;
		}

		private void RemoveChild(BTEditorGraphNode child)
		{
			if(mChildren_.Remove(child))
			{
				if(mNode_ is Composite)
				{
					Composite composite = mNode_ as Composite;
					composite.RemoveChild(child.Node);
				}
				else if(mNode_ is Decorator)
				{
					Decorator decorator = mNode_ as Decorator;
					decorator.SetChildren(null);
				}
			}
		}

		private void DestroyChildren()
		{
			for(int i=0;i<mChildren_.Count;i++){
				
			}
			if(mNode_ is Composite){
				Composite composite = mNode_ as Composite;
				composite.RemoveAllChildren();
				}
			else if(mNode_ is Decorator){

				Decorator decorator = mNode_ as Decorator;
				decorator.SetChildren(null);
				//(Decorator)mNode_.SetChildren(null);
			}
	
		}

		private void OnDestroy()
		{
			mGraph_.OnNodeDeselect(this);
			for(int i=0;i<mChildren_.Count;i++){
				BTEditorGraphNode.DestroyImmediate(mChildren_[i]);
			}
		}

		private static BTEditorGraphNode CreateEmptyNode()
		{
			BTEditorGraphNode node = ScriptableObject.CreateInstance<BTEditorGraphNode>();
			node.OnCreated();
			node.hideFlags = HideFlags.HideAndDontSave;

			return node;
		}

		private static BTEditorGraphNode CreateExistingNode(BTEditorGraphNode parent, BehaviourNode node)
		{
			BTEditorGraphNode gNode = BTEditorGraphNode.CreateEmptyNode();
			gNode.mParent_ = parent;
			gNode.mGraph_= parent.Graph;
			gNode.SetExistingNode(node);

			return gNode;
		}

		public static BTEditorGraphNode CreateRoot(BTEditorGraph graph, Root node)
		{
			if(graph!=null){
				BTEditorGraphNode gNode= BTEditorGraphNode.CreateEmptyNode();
				gNode.mGraph_=graph;
				gNode.SetExistingNode(node);

				return gNode;
			}
			else{
				return null;
			}
		}
	}
}