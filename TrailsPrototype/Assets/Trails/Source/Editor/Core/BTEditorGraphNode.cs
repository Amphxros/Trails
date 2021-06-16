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
		private const float DOUBLE_CLICK_THRESHOLD = 0.2f;

		private List<BTEditorGraphNode> mChildren_;
		private BehaviourNode mNode_;
		private BTEditorGraphNode mParent_;
		private BTEditorGraph mGraph_;
		private Vector2 mDragOffset_;
		private float mLastClickTime_;
		private bool mIsSelected_;
		private bool mIsDragging_;
		private bool mCanBeginDragging_;

		
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

		public Vector2 NodePosition
		{
			get { return mNode_.Position; }
			set
			{
				if(!IsRoot)
				{
					//Vector2 offset = value - mNode_.Position;
					mNode_.Position = value;
					//UpdateChildrenPosition(offset);
				}
			}
		}

		private bool CanUpdateChildren
		{
			get
			{
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
			mIsDragging_ = false;
			mCanBeginDragging_ = false;
			mDragOffset_ = Vector2.zero;
			mLastClickTime_ = -1;

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
			Rect position = new Rect(NodePosition, BTEditorStyle.GetNodeSize(mNode_));
			Vector2 mousePosition = BTEditorCanvas.Current.WindowSpaceToCanvasSpace(BTEditorCanvas.Current.Event.mousePosition);

			if(BTEditorCanvas.Current.Event.type == EventType.MouseDown && BTEditorCanvas.Current.Event.button == SELECT_MOUSE_BUTTON)
			{
				if(position.Contains(mousePosition))
				{
					if(!mIsSelected_)
						mGraph_.OnNodeSelect(this);

					if(mLastClickTime_ > 0)
					{
						if(Time.realtimeSinceStartup <= mLastClickTime_ + DOUBLE_CLICK_THRESHOLD)
						{
							OnDoubleClicked();
						}
					}
					
					mLastClickTime_ = Time.realtimeSinceStartup;

					mCanBeginDragging_ = !IsRoot;
					BTEditorCanvas.Current.Event.Use();
				}
			}
			else if(BTEditorCanvas.Current.Event.type == EventType.MouseDown && BTEditorCanvas.Current.Event.button == CONTEXT_MOUSE_BUTTON)
			{
				if(position.Contains(mousePosition))
				{
					ShowContextMenu();
					BTEditorCanvas.Current.Event.Use();
				}
			}
			else if(BTEditorCanvas.Current.Event.type == EventType.MouseUp && BTEditorCanvas.Current.Event.button == SELECT_MOUSE_BUTTON)
			{
				if(mIsDragging_)
				{
					mGraph_.OnNodeEndDrag(this);
					BTEditorCanvas.Current.Event.Use();
				}
				mCanBeginDragging_ = false;
			}
			else if(BTEditorCanvas.Current.Event.type == EventType.MouseDrag && BTEditorCanvas.Current.Event.button == DRAG_MOUSE_BUTTON)
			{
				if(!mGraph_.ReadOnly && !mIsDragging_ && mCanBeginDragging_ && position.Contains(mousePosition))
				{
					mGraph_.OnNodeBeginDrag(this, mousePosition);
					BTEditorCanvas.Current.Event.Use();
				}
				else if(mIsDragging_)
				{
					mGraph_.OnNodeDrag(this, mousePosition);
					BTEditorCanvas.Current.Event.Use();
				}
			}
			else if(mGraph_.SelectionBox.HasValue)
			{
				if(mGraph_.SelectionBox.Value.Contains(position.center))
				{
					if(!mIsSelected_)
					{
						mGraph_.OnNodeSelect(this);
					}
				}
				else
				{
					if(mIsSelected_)
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

			DrawConstraints();

			if(CanDrawChildren)
				DrawChildren();
		}

		private BehaviourNodeStatus GetNodeStatus(BehaviourNode node)
		{
			BehaviourNodeStatus status = BehaviourNodeStatus.None;
			
			return status;
		}

		private BehaviourNodeStatus GetConstraintResult(BehaviourNode node, int index)
		{
			BehaviourNodeStatus status = BehaviourNodeStatus.None;
		
			return status;
		}

		private void DrawTransitions()
		{
			Vector2 nodeSize = BTEditorStyle.GetNodeSize(mNode_);
			Rect position = new Rect(NodePosition + BTEditorCanvas.Current.Position, nodeSize);
			BTEditorTreeLayout treeLayout = BTEditorStyle.TreeLayout;

			foreach(var child in mChildren_)
			{
				Vector2 childNodeSize = BTEditorStyle.GetNodeSize(child.Node);
				Rect childPosition = new Rect(child.Node.Position + BTEditorCanvas.Current.Position, childNodeSize);
				BehaviourNodeStatus childStatus = child.Node.Status;
				Color color = BTEditorStyle.GetTransitionColor(childStatus);
				Vector2 nodeCenter = position.center;
				Vector2 childCenter = childPosition.center;

				if(treeLayout == BTEditorTreeLayout.Vertical)
				{
					if(Mathf.Approximately(nodeCenter.y, childCenter.y) || Mathf.Approximately(nodeCenter.x, childCenter.x))
					{
						BTEditorUtils.DrawLine(nodeCenter, childCenter, color);
					}
					else
					{
						BTEditorUtils.DrawLine(nodeCenter, nodeCenter + Vector2.up * (childCenter.y - nodeCenter.y) / 2, color);

						BTEditorUtils.DrawLine(nodeCenter + Vector2.up * (childCenter.y - nodeCenter.y) / 2,
											   childCenter + Vector2.up * (nodeCenter.y - childCenter.y) / 2, color);

						BTEditorUtils.DrawLine(childCenter, childCenter + Vector2.up * (nodeCenter.y - childCenter.y) / 2, color);
					}
				}
				else if(treeLayout == BTEditorTreeLayout.Horizontal)
				{
					//BTEditorUtils.DrawBezier(nodeCenter, childCenter, color);
					Vector2 nodeRight = new Vector2(position.center.x + nodeSize.x / 2, position.center.y);
					Vector2 childLeft = new Vector2(childPosition.center.x - childNodeSize.x / 2, childPosition.center.y);
					BTEditorUtils.DrawBezierCurve(nodeRight, childLeft, color);
				}
				else
				{
					BTEditorUtils.DrawLine(nodeCenter, childCenter, color);
				}
			}
		}

		private void DrawSelf()
		{
			string label = string.IsNullOrEmpty(mNode_.Name) ? mNode_.Title : mNode_.Name;
			BTGraphNodeStyle nodeStyle = BTEditorStyle.GetStyle(mNode_);
			Vector2 nodeSize = BTEditorStyle.GetNodeSize(mNode_);
			Rect position = new Rect(mNode_.Position + BTEditorCanvas.Current.Position, nodeSize);
			BehaviourNodeStatus status =  mNode_.Status;

			GUI.Box(position, BTEditorStyle.ArrowUP);
			


			int iconSize = 32;
			int iconOffsetY = 7;
			Rect iconPos = new Rect(position.x + (nodeSize.x - iconSize) / 2, position.y + (nodeSize.y - iconSize) / 2 - iconOffsetY, iconSize, iconSize);
			//GUI.DrawTexture(iconPos, BTEditorStyle.GetNodeIcon(mNode_));

			Rect titlePos = new Rect(position);
			titlePos.y = titlePos.y - 5;

			// show index of composite's children.
			if (Parent != null && Parent.Node is Composite)
			{
				Composite compNode = Parent.Node as Composite;
				int index = compNode.GetIndex(mNode_);
				Rect nodeLeftPos = new Rect(position.x + 2, position.center.y - 8, 20, 16);
				EditorGUI.LabelField(nodeLeftPos, index.ToString(), EditorStyles.label);
			}


		}

		private void DrawChildren()
		{
			for(int i = 0; i < mChildren_.Count; i++)
			{
				mChildren_[i].Draw();
			}
		}

		private void DrawComment()
		{
			
		}

		private void DrawConstraints()
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
			mIsDragging_ = false;
			if(Selection.activeObject == this)
			{
				Selection.activeObject = null;
			}
			BTEditorCanvas.Current.Repaint();
		}

		public void OnBeginDrag(Vector2 position)
		{
			mDragOffset_ = position - NodePosition;
			mIsDragging_ = true;
		}

		public void OnDrag(Vector2 position)
		{
			Vector2 nodePos = position - mDragOffset_;
			if(BTEditorCanvas.Current.SnapToGrid)
			{
				float snapSize = BTEditorCanvas.Current.SnapSize;
				nodePos.x = (float)Math.Round(nodePos.x / snapSize) * snapSize;
				nodePos.y = (float)Math.Round(nodePos.y / snapSize) * snapSize;
			}

			NodePosition = nodePos;

			BTEditorCanvas.Current.RecalculateSize(NodePosition);
			BTEditorCanvas.Current.Repaint();
		}

		public void OnEndDrag()
		{
			mIsDragging_ = false;

			UpdateSiblingNodeIndex();
		}

		private void OnDoubleClicked()
		{
			if(mNode_ is NodeGroup)
			{
				if(IsRoot)
					mGraph_.OnPopNodeGroup();
				else
					mGraph_.OnPushNodeGroup(this);
			}
		}

		private void SetExistingNode(BehaviourNode node)
		{
			DestroyChildren();

			mNode_ = node;
			mIsSelected_ = false;

			if(node is Composite)
			{
				Composite composite = node as Composite;
				for(int i = 0; i < composite.Count; i++)
				{
					BehaviourNode childNode = composite.GetChild(i);
					BTEditorGraphNode graphNode = BTEditorGraphNode.CreateExistingNode(this, childNode);
					mChildren_.Add(graphNode);
				}
			}
			else if(node is Decorator)
			{
				Decorator decorator = node as Decorator;
				BehaviourNode childNode = decorator.GetChildren();
				if(childNode != null)
				{
					BTEditorGraphNode graphNode = BTEditorGraphNode.CreateExistingNode(this, childNode);
					mChildren_.Add(graphNode);
				}
			}
		}

		private void ShowContextMenu()
		{
			GenericMenu menu = BTContextMenuFactory.CreateNodeContextMenu(this);
			menu.DropDown(new Rect(BTEditorCanvas.Current.Event.mousePosition, Vector2.zero));
		}

		public BTEditorGraphNode OnCreateChild(Type type)
		{
			if(type != null)
			{
				BehaviourNode node = BTUtils.CreateNode(type);
				if(node != null)
				{
					Vector2 nodeSize = BTEditorStyle.GetNodeSize(mNode_);
					Vector2 nodePos = NodePosition + nodeSize + new Vector2(50, 0);
					nodePos.x = Mathf.Max(nodePos.x, 0.0f);
					nodePos.y = Mathf.Max(nodePos.y, 0.0f);

					// force horizontal
					nodePos.y = NodePosition.y;

					node.Position = nodePos;

					return OnCreateChild(node);
				}
			}

			return null;
		}

		public BTEditorGraphNode OnCreateChild(BehaviourNode node)
		{
			if(node != null && ((mNode_ is Composite) || (mNode_ is Decorator)))
			{
				if(mNode_ is Composite)
				{
					Composite composite = mNode_ as Composite;
					composite.AddChild(node);
				}
				else if(mNode_ is Decorator)
				{
					Decorator decorator = mNode_ as Decorator;

					DestroyChildren();
					decorator.SetChildren(node);
				}

				BTEditorGraphNode graphNode = BTEditorGraphNode.CreateExistingNode(this, node);
				mChildren_.Add(graphNode);

				BTEditorCanvas.Current.RecalculateSize(node.Position);
				return graphNode;
			}

			return null;
		}

		public BTEditorGraphNode OnInsertChild(int index, Type type)
		{
			if(type != null)
			{
				BehaviourNode node = BTUtils.CreateNode(type);
				if(node != null)
				{
					Vector2 nodeSize = BTEditorStyle.GetNodeSize(mNode_);
					Vector2 nodePos = NodePosition + nodeSize + new Vector2(50, 0);
					nodePos.x = Mathf.Max(nodePos.x, 0.0f);
					nodePos.y = Mathf.Max(nodePos.y, 0.0f);

					// force horizontal
					nodePos.y = NodePosition.y;

					node.Position = nodePos;

					return OnInsertChild(index, node);
				}
			}

			return null;
		}

		public BTEditorGraphNode OnInsertChild(int index, BehaviourNode node)
		{
			if(node != null && ((mNode_ is Composite) || (mNode_ is Decorator)))
			{
				BTEditorGraphNode graphNode = null;

				if(mNode_ is Composite)
				{
					Composite composite = mNode_ as Composite;
					composite.InsertChild(index, node);

					graphNode = BTEditorGraphNode.CreateExistingNode(this, node);
					mChildren_.Insert(index, graphNode);
				}
				else if(mNode_ is Decorator)
				{
					Decorator decorator = mNode_ as Decorator;

					DestroyChildren();
					decorator.SetChildren(node);

					graphNode = BTEditorGraphNode.CreateExistingNode(this, node);
					mChildren_.Add(graphNode);
				}

				BTEditorCanvas.Current.RecalculateSize(node.Position);
				return graphNode;
			}

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
			BTEditorGraphNode child = GetChild(index);
			if(child != null)
			{
				child.OnDelete();
			}
		}

		public int GetChildIndex(BTEditorGraphNode child)
		{
			return mChildren_.IndexOf(child);
		}

		public void ChangeChildIndex(int sourceIndex, int destinationIndex)
		{
			if(sourceIndex >= 0 && sourceIndex < ChildCount && destinationIndex >= 0 && destinationIndex < ChildCount)
			{
			}
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
			for(int i = 0; i < mChildren_.Count; i++)
			{
				BTEditorGraphNode.DestroyImmediate(mChildren_[i]);
			}

			if(mNode_ is Composite)
			{
				((Composite)mNode_).RemoveAllChildren();
			}
			else if(mNode_ is Decorator)
			{
				((Decorator)mNode_).SetChildren(null);
			}

			mChildren_.Clear();
		}

		private void OnDestroy()
		{
			mGraph_.OnNodeDeselect(this);
			foreach(var child in mChildren_)
			{
				BTEditorGraphNode.DestroyImmediate(child);
			}
		}

		private static int SortSiblingCompare(BehaviourNode n1, BehaviourNode n2)
		{
			float y1 = n1.Position.y;
			float y2 = n2.Position.y;
			if (y1 > y2)
				return 1;
			else if (y1 < y2)
				return -1;
			else
				return 0;
		}

		private void UpdateSiblingNodeIndex()
		{
			if (Parent != null && Parent.Node is Composite)
			{
			
			}
		}

		private static BTEditorGraphNode CreateEmptyNode()
		{
			BTEditorGraphNode graphNode = ScriptableObject.CreateInstance<BTEditorGraphNode>();
			graphNode.OnCreated();
			graphNode.hideFlags = HideFlags.HideAndDontSave;

			return graphNode;
		}

		private static BTEditorGraphNode CreateExistingNode(BTEditorGraphNode parent, BehaviourNode node)
		{
			BTEditorGraphNode graphNode = BTEditorGraphNode.CreateEmptyNode();
			graphNode.mParent_ = parent;
			graphNode.mGraph_ = parent.Graph;
			graphNode.SetExistingNode(node);

			return graphNode;
		}

		public static BTEditorGraphNode CreateRoot(BTEditorGraph graph, Root node)
		{
			if(graph != null && node != null)
			{
				BTEditorGraphNode graphNode = BTEditorGraphNode.CreateEmptyNode();
				graphNode.mGraph_ = graph;
				graphNode.mParent_ = null;
				graphNode.SetExistingNode(node);

				return graphNode;
			}

			return null;
		}

	}
}