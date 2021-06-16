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
		private bool mIsSelected_;
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
			get { 
				

				return mGraph_.IsRoot(this); 
			}
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
			return !(mNode_ is NodeGroup) || mGraph_.IsRoot(this);
			}
		}

		private bool CanDrawChildren
		{
			get
			{
			return !(mNode_ is NodeGroup) || mGraph_.IsRoot(this);
			}
		}
		
		private void OnCreated()
		{
			if(mChildren_ == null)
			{
				mChildren_ = new List<BTEditorGraphNode>();
			}
			
			mIsSelected_ = false;
			m_isDragging = false;
			m_canBeginDragging = false;
			m_dragOffset = Vector2.zero;
			m_lastClickTime = null;
		}

		public void Update()
		{
			if(CanUpdateChildren){
				UpdateChildren();
			}

			HandleEvents();
		}

		private void UpdateChildren()
		{
			for(int i = 0; i < mChildren_.Count; i++)
			{
				mChildren_[i].Update();
			}
		}

		private void HandleEvents()
		{
			Rect pos = new Rect(mNode_.Position, BTEditorStyle.GetNodeSize(mNode_));
			Vector2 mousePos = BTEditorCanvas.Current.WindowSpaceToCanvasSpace(BTEditorCanvas.Current.Event.mousePosition);

			//click
			if (BTEditorCanvas.Current.Event.type == EventType.MouseDown && BTEditorCanvas.Current.Event.button == SELECT_MOUSE_BUTTON)
			{
				if (pos.Contains(mousePos)) {

					// single click
					if (!mIsSelected_) mGraph_.OnNodeSelect(this);

					// double click
					if(m_lastClickTime > 0)
                    {
						if (Time.realtimeSinceStartup <= m_lastClickTime + DOUBLE_CLICK_THRESHOLD)
						{
							OnDoubleClicked();
						}
					}

					m_lastClickTime = Time.realtimeSinceStartup;

					m_canBeginDragging = !IsRoot;
					BTEditorCanvas.Current.Event.Use();
				}
			}
			//
			else if (BTEditorCanvas.Current.Event.type == EventType.MouseDown && BTEditorCanvas.Current.Event.button == CONTEXT_MOUSE_BUTTON)
			{
				if (pos.Contains(mousePos))
				{
					ShowContextMenu();
					BTEditorCanvas.Current.Event.Use();
				}
			}
			//release
			else if (BTEditorCanvas.Current.Event.type == EventType.MouseUp && BTEditorCanvas.Current.Event.button == SELECT_MOUSE_BUTTON)
			{
				if (m_isDragging)
				{
					mGraph_.OnNodeEndDrag(this);
					BTEditorCanvas.Current.Event.Use();
				}
				m_canBeginDragging = false;
			}
			//drag
			else if (BTEditorCanvas.Current.Event.type == EventType.MouseDrag && BTEditorCanvas.Current.Event.button == DRAG_MOUSE_BUTTON)
			{
				if (!mGraph_.ReadOnly && !m_isDragging && m_canBeginDragging && pos.Contains(mousePos))
				{
					mGraph_.OnNodeBeginDrag(this, mousePos);
					BTEditorCanvas.Current.Event.Use();
				}
				else if (m_isDragging)
				{
					mGraph_.OnNodeDrag(this, mousePos);
					BTEditorCanvas.Current.Event.Use();
				}
			}

			else if (mGraph_.SelectionBox.HasValue)
			{
				if (mGraph_.SelectionBox.Value.Contains(pos.center))
				{
					if (!mIsSelected_)
					{
						mGraph_.OnNodeSelect(this);
					}
				}
				else
				{
					if (mIsSelected_)
					{
						mGraph_.OnNodeDeselect(this);
					}
				}
			}
		}

		public void Draw()
		{
			if(CanDrawChildren)
				DrawTransitions();

			DrawSelf();
			DrawComment();

			if(CanDrawChildren)
				DrawChildren();
		}

		private void DrawTransitions()
		{
			Vector2 size= BTEditorStyle.GetNodeSize(mNode_);
			Rect pos= new Rect(NodePositon + BTEditorCanvas.Current.Position, size);
			BTEditorTreeLayout treeLayout= BTEditorStyle.TreeLayout;
			foreach(var child in mChildren_)
			{
				Vector2 cSize= BTEditorStyle.GetNodeSize(child.Node);
				Rect dest= new Rect(child.Node.Position + BTEditorCanvas.Current.Position, cSize);
		
				BehaviourNodeStatus status= child.Node.Status;
				Color color = BTEditorStyle.GetTransitionColor(status);
				Vector2 nodeCenter = pos.center;
				Vector2 childCenter = dest.center;

				switch (treeLayout)
				{
					case BTEditorTreeLayout.Vertical:
					if(Mathf.Approximately(nodeCenter.y, childCenter.y) || Mathf.Approximately(nodeCenter.x, childCenter.x))
					{
						
						BTEditorUtils.DrawLine(nodeCenter, childCenter, color);
					}
					else{
						BTEditorUtils.DrawLine(nodeCenter, nodeCenter + Vector2.up * (childCenter.y - nodeCenter.y) / 2, color);

						BTEditorUtils.DrawLine(nodeCenter + Vector2.up * (childCenter.y - nodeCenter.y) / 2,
											   childCenter + Vector2.up * (nodeCenter.y - childCenter.y) / 2, color);

						BTEditorUtils.DrawLine(childCenter, childCenter + Vector2.up * (nodeCenter.y - childCenter.y) / 2, color);
					}
						break;
				
					case BTEditorTreeLayout.Horizontal:
						Vector2 nodeRight = new Vector2(pos.center.x + cSize.x / 2, pos.center.y);
						Vector2 nodeLeft = new Vector2(dest.center.x - cSize.x / 2, dest.center.y);
						BTEditorUtils.DrawBezierCurve(nodeRight, nodeLeft, color);
						break;
					default:
						BTEditorUtils.DrawLine(nodeCenter, childCenter, color);
						break;
				}
				

			
			}


		}

		private void DrawSelf()
		{
			string label="node";
			

			BTGraphNodeStyle style = BTEditorStyle.GetStyle(mNode_);
			Rect pos = new Rect( mNode_.Position + BTEditorCanvas.Current.Position, new Vector2(100,40));
			BehaviourNodeStatus status = mNode_.Status;

			EditorGUI.LabelField(pos, label, style.GetStyle(status, mIsSelected_));
		

		}

		private void DrawChildren()
		{
			foreach( var c in mChildren_){
				c.Draw();
			}
			
		}

		private void DrawComment()
		{
		
		}

		public void OnSelected()
		{
			mIsSelected_ = true;
			Selection.activeObject = this;
			BTEditorCanvas.Current.Repaint();
		}

		public void OnDeselected()
		{
			mIsSelected_ = false;
			m_isDragging = false;
			if(Selection.activeObject == this)
			{
				Selection.activeObject = null;
			}
			BTEditorCanvas.Current.Repaint();
		}

		public void OnBeginDrag(Vector2 pos)
		{
			m_dragOffset = pos - NodePositon;
			m_isDragging = true;
		}

		public void OnDrag(Vector2 pos)
		{
			Vector2 nodePos = pos - m_dragOffset;
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
			node.NodePositon= new Vector2(0,0);
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
			if(graph!=null && node != null){
				BTEditorGraphNode gNode= BTEditorGraphNode.CreateEmptyNode();
				gNode.mGraph_=graph;
				gNode.mParent_=null;
				gNode.SetExistingNode(node);

				return gNode;
			}
			return null;
		}
	}
}