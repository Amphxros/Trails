using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails;
using UnityEditor;
using System;

namespace TrailsEditor {
   public class BTEditorGraph : ScriptableObject
	{
		private const int SELECT_MOUSE_BUTTON = 0;
		private const int CONTEXT_MOUSE_BUTTON = 1;

		private BTAsset mBTAsset_ = null;
		private BehaviourTree mBehaviourTree_ = null;
		private List<BTEditorGraphNode> mSelection_;
		private BTEditorGraphNode mMasterRoot_;
		private Stack<BTEditorGraphNode> mRootStack_;
		private bool mDrawSelectionBox_;
		private bool mIsBehaviourTreeReadOnly_;
		private bool mCanBeginBoxSelection;
		private Vector2 mSelectionBoxStartPos_;

		public bool ReadOnly
		{
			get
			{
				return mIsBehaviourTreeReadOnly_ || EditorApplication.isPlaying;
			}
		}

		public int Depth
		{
			get
			{
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

		public BehaviourTree BTree { get { return mBehaviourTree_; } }


		private void OnCreated()
		{
			mMasterRoot_ = null;
			mRootStack_ = new Stack<BTEditorGraphNode>();
			mDrawSelectionBox_ = false;
			mIsBehaviourTreeReadOnly_ = false;
			mSelectionBoxStartPos_ = Vector2.zero;
			SelectionBox = null;
		}

		private void OnDestroy()
		{
			BTEditorGraphNode.DestroyImmediate(mMasterRoot_);
			mMasterRoot_ = null;
			mRootStack_.Clear();
		}

		public void SetBehaviourTree(BTAsset asset, BehaviourTree behaviourTree)
		{
			this.mBTAsset_ = asset;
			this.mBehaviourTree_ = behaviourTree;

			if(mMasterRoot_ != null)
			{
				BTEditorGraphNode.DestroyImmediate(mMasterRoot_);
				mMasterRoot_ = null;
				mRootStack_.Clear();
			}

			mIsBehaviourTreeReadOnly_ = behaviourTree.isReadOnly;
			mMasterRoot_ = BTEditorGraphNode.CreateRoot(this, behaviourTree.Root);
			mRootStack_.Push(mMasterRoot_);
			
		}

		public void DrawGUI(Rect screenRect)
		{
			DrawTreeInfo();

			if(WorkingRoot != null)
			{
				WorkingRoot.Update();
				WorkingRoot.Draw();
				DrawSelectionBox();
				HandleEvents(screenRect);
			}
		}

		private void DrawTreeInfo()
		{
			
			
		}

		private void DrawSelectionBox()
		{
			if(mDrawSelectionBox_)
			{
				Vector2 mousePosition = BTEditorCanvas.Current.Event.mousePosition;
				Rect position = new Rect();
				position.x = Mathf.Min(mSelectionBoxStartPos_.x, mousePosition.x);
				position.y = Mathf.Min(mSelectionBoxStartPos_.y, mousePosition.y);
				position.width = Mathf.Abs(mousePosition.x - mSelectionBoxStartPos_.x);
				position.height = Mathf.Abs(mousePosition.y - mSelectionBoxStartPos_.y);

				GUI.Box(position, "", BTEditorStyle.SelectionBox);
				BTEditorCanvas.Current.Repaint();

				SelectionBox = new Rect(BTEditorCanvas.Current.WindowSpaceToCanvasSpace(position.position), position.size);
			}
			else
			{
				SelectionBox = null;
			}
		}

		private void HandleEvents(Rect screenRect)
		{
			if (BTEditorCanvas.Current.Event.type == EventType.MouseDown && BTEditorCanvas.Current.Event.button == SELECT_MOUSE_BUTTON)
			{
				if (screenRect.Contains(BTEditorCanvas.Current.Event.mousePosition))
				{
					ClearSelection();

					mCanBeginBoxSelection = true;
					mSelectionBoxStartPos_ = BTEditorCanvas.Current.Event.mousePosition;
					BTEditorCanvas.Current.Event.Use();
				}
			}
			else if (BTEditorCanvas.Current.Event.type == EventType.MouseDrag && BTEditorCanvas.Current.Event.button == SELECT_MOUSE_BUTTON)
			{
				if (screenRect.Contains(BTEditorCanvas.Current.Event.mousePosition))
				{
					if (!mDrawSelectionBox_ && mCanBeginBoxSelection)
					{
						mDrawSelectionBox_ = true;
					}

					BTEditorCanvas.Current.Event.Use();
				}
			}
			else if (BTEditorCanvas.Current.Event.type == EventType.MouseUp)
			{
				if (screenRect.Contains(BTEditorCanvas.Current.Event.mousePosition))
				{
					if (BTEditorCanvas.Current.Event.button == SELECT_MOUSE_BUTTON)
					{
						if (mDrawSelectionBox_)
						{
							mDrawSelectionBox_ = false;
						}

						BTEditorCanvas.Current.Event.Use();
					}
					else if (BTEditorCanvas.Current.Event.button == CONTEXT_MOUSE_BUTTON)
					{
						GenericMenu menu = BTContextMenuFactory.CreateGraphContextMenu(this);
						menu.DropDown(new Rect(BTEditorCanvas.Current.Event.mousePosition, Vector2.zero));

						BTEditorCanvas.Current.Event.Use();
					}
				}

				mCanBeginBoxSelection = false;
			}

		}

		public void OnPushNodeGroup(BTEditorGraphNode node)
		{
			if(node != null && node.Node is NodeGroup)
			{
				mRootStack_.Push(node);

				SelectSingle(node);
			}
		}

		public void OnPopNodeGroup()
		{
			if(mRootStack_.Count > 1)
			{
				var oldWorkingRoot = mRootStack_.Pop();
				SelectEntireNodeGroup(oldWorkingRoot);
			}
		}

		public void OnNodeSelect(BTEditorGraphNode node)
		{
			if(BTEditorCanvas.Current.Event.shift && (node.Node is Composite || node.Node is Decorator))
			{
				SelectBranch(node);
			}
			else if(BTEditorCanvas.Current.Event.control || SelectionBox.HasValue)
			{
				if(!mSelection_.Contains(node))
				{
					if(node.Node is NodeGroup && !IsRoot(node))
						SelectEntireNodeGroupAdditive(node);
					else
						SelectSingleAdditive(node);
				}
			}
			else
			{
				if(node.Node is NodeGroup && !IsRoot(node))
					SelectEntireNodeGroup(node);
				else
					SelectSingle(node);
			}
		}

		public void OnNodeDeselect(BTEditorGraphNode node)
		{
			if(mSelection_.Remove(node))
			{
				node.OnDeselected();
			}
		}

		public void OnNodeBeginDrag(BTEditorGraphNode node, Vector2 position)
		{
			if(mSelection_.Contains(node))
			{
				for(int i = 0; i < mSelection_.Count; i++)
				{
					mSelection_[i].OnBeginDrag(position);
				}
			}
		}

		public void OnNodeDrag(BTEditorGraphNode node, Vector2 position)
		{
			if(mSelection_.Contains(node))
			{
				for(int i = 0; i < mSelection_.Count; i++)
				{
					mSelection_[i].OnDrag(position);
				}
			}
		}

		public void OnNodeEndDrag(BTEditorGraphNode node)
		{
			if(mSelection_.Contains(node))
			{
				for(int i = 0; i < mSelection_.Count; i++)
				{
					mSelection_[i].OnEndDrag();
				}
			}
		}

		public void OnNodeCreateChild(BTEditorGraphNode parent, Type childType)
		{
			if(parent != null && childType != null)
			{
				BTEditorGraphNode child = parent.OnCreateChild(childType);
				if(child != null)
				{
					
				}
			}
		}

		public void OnNodeSwitchType(BTEditorGraphNode target, Type newType)
		{
			if(target == null || newType == null)
				return;

			BTEditorGraphNode parentNode = target.Parent;
			Vector2 oldPosition = target.Node.Position;
			int oldIndex = target.Parent.GetChildIndex(target);

			BehaviourNode node = BTUtils.CreateNode(newType);
			if(node != null)
			{
				node.Constraints.AddRange(target.Node.Constraints);

				if (node is Decorator)
				{
					Decorator original = target.Node as Decorator;
					Decorator decorator = node as Decorator;

					decorator.SetChildren(original.GetChildren());
				}
				else if(node is Composite)
				{
					Composite original = target.Node as Composite;
					Composite composite = node as Composite;

					for(int i = 0; i < original.Count; i++)
						composite.AddChild(original.GetChild(i));
				}

				target.OnDelete();

				BTEditorGraphNode newNode = parentNode.OnInsertChild(oldIndex, node);
				if(newNode != null)
				{
					newNode.Node.Position = oldPosition;
				
				}
			}
		}

		public void OnNodeDelete(BTEditorGraphNode node)
		{
			if(node != null)
			{
				node.OnDelete();
			}
		}

		public void OnNodeDeleteChildren(BTEditorGraphNode node)
		{
			if(node != null)
			{
				int childIndex = 0;
				while(node.ChildCount > 0)
				{
					node.OnDeleteChild(0);
					childIndex++;
				}
			
			}
		}

		public void OnCopyNode(BTEditorGraphNode source)
		{
			if(CanCopy(source))
			{
				BTEditorCanvas.Current.Clipboard = BTUtils.SerializeNode(source.Node);
			}
		}

		public bool CanCopy(BTEditorGraphNode source)
		{
			return source != null && source.Node != null;
		}

		public void OnPasteNode(BTEditorGraphNode destination)
		{
			if(CanPaste(destination))
			{
				BehaviourNode node = BTUtils.DeserializeNode(BTEditorCanvas.Current.Clipboard);
				BTEditorGraphNode child = destination.OnCreateChild(node);
				if(child != null)
				{
					SelectBranch(child);
				}
			}
		}

		public bool CanPaste(BTEditorGraphNode destination)
		{
			if(destination != null && destination.Node != null && !string.IsNullOrEmpty(BTEditorCanvas.Current.Clipboard))
			{
				if(destination.Node is NodeGroup)
				{
					return IsRoot(destination) && destination.ChildCount == 0;
				}
				else if(destination.Node is Decorator)
				{
					return destination.ChildCount == 0;
				}
				else if(destination.Node is Composite)
				{
					return true;
				}
			}

			return false;
		}

		public void IncreaseEditingDepth(BTEditorGraphNode node)
		{
			if(node != null && (node.Node is NodeGroup || node.Node is Root))
			{
				mRootStack_.Push(node);
			}
		}

		public void DecreaseEditingDepth()
		{
			if(mRootStack_.Count > 1)
			{
				mRootStack_.Pop();
			}
		}

		public bool IsRoot(BTEditorGraphNode node)
		{
			return node == WorkingRoot;
		}

		public void SelectEntireGraph()
		{
			ClearSelection();
			SelectBranchRecursive(WorkingRoot);
		}

		private void SelectEntireNodeGroup(BTEditorGraphNode node)
		{
			SelectBranch(node);
			node.OnSelected();
		}
		
		private void SelectEntireNodeGroupAdditive(BTEditorGraphNode node)
		{
			SelectBranchAdditive(node);
			node.OnSelected();
		}

		private void SelectBranch(BTEditorGraphNode root)
		{
			ClearSelection();
			SelectBranchRecursive(root);
		}

		private void SelectBranchAdditive(BTEditorGraphNode root)
		{
			SelectBranchRecursive(root);
		}

		private void SelectSingle(BTEditorGraphNode node)
		{
			ClearSelection();
			mSelection_.Add(node);
			node.OnSelected();
		}

		private void SelectSingleAdditive(BTEditorGraphNode node)
		{
			mSelection_.Add(node);
			node.OnSelected();
		}

		private void SelectBranchRecursive(BTEditorGraphNode node)
		{
			mSelection_.Add(node);
			node.OnSelected();

			for(int i = 0; i < node.ChildCount; i++)
			{
				SelectBranchRecursive(node.GetChild(i));
			}
		}

		private void ClearSelection()
		{
			if(mSelection_.Count > 0)
			{
				for(int i = 0; i < mSelection_.Count; i++)
				{
					mSelection_[i].OnDeselected();
				}

				mSelection_.Clear();
			}
		}

		public void DeleteAllBreakpoints()
		{
			DeleteBreakpointsRecursive(mMasterRoot_);
		}

		private void DeleteBreakpointsRecursive(BTEditorGraphNode node)
		{

		}

		public string GetNodeHash(BTEditorGraphNode node)
		{
			List<byte> path = new List<byte>();
			for(BTEditorGraphNode n = node; n != null && n.Parent != null; n = n.Parent)
			{
				path.Add((byte)n.Parent.GetChildIndex(n));
			}
			path.Reverse();

			return Convert.ToBase64String(path.ToArray());
		}

		public BTEditorGraphNode GetNodeByHash(string path)
		{
			byte[] actualPath = Convert.FromBase64String(path);
			if(actualPath != null)
			{
				BTEditorGraphNode node = mMasterRoot_;

				for(int i = 0; i < actualPath.Length; i++)
				{
					node = node.GetChild(actualPath[i]);
					if(node == null)
					{
						return null;
					}
				}

				return node;
			}

			return null;
		}

		public static BTEditorGraph Create()
		{
			BTEditorGraph graph = ScriptableObject.CreateInstance<BTEditorGraph>();
			graph.OnCreated();
			graph.hideFlags = HideFlags.HideAndDontSave;
			graph.mSelection_ = new List<BTEditorGraphNode>();

			return graph;
		}

		public int GetSelectedNodeCount()
		{
			return mSelection_.Count;
		}

		public BTEditorGraphNode GetSelectedNode(int index)
		{
			return mSelection_[index];
		}

		public BTEditorGraphNode GetFirstSelectedNode()
		{
			if (mSelection_.Count > 0)
				return mSelection_[0];
			else
				return null;
		}

		public BTEditorGraphNode GetLastSelectedNode()
		{
			if (mSelection_.Count > 0)
				return mSelection_[mSelection_.Count - 1];
			else
				return null;
		}

	}
}
