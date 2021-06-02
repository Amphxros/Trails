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

		private List<BTEditorGraphNode> m_children;
		private BehaviourNode m_node;
		private BTEditorGraphNode m_parent;
		private BTEditorGraph m_graph;
		private Vector2 m_dragOffset;
		private float? m_lastClickTime;
		private bool m_isSelected;
		private bool m_isDragging;
		private bool m_canBeginDragging;
		
		public BehaviourNode Node
		{
			get { return m_node; }
		}

		public BTEditorGraphNode Parent
		{
			get { return m_parent; }
		}

		public BTEditorGraph Graph
		{
			get { return m_graph; }
		}

		public int ChildCount
		{
			get { return m_children.Count; }
		}

		public bool IsRoot
		{
			get { return m_graph.IsRoot(this); }
		}

		public Vector2 NodePositon
		{
			get { return m_node.Position; }
			set
			{
				if(!IsRoot)
				{
					m_node.Position = value;
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
			if(m_children == null)
			{
				m_children = new List<BTEditorGraphNode>();
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
			for(int i = m_children.Count - 1; i >= 0; i--)
			{
				m_children[i].Update();
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
			if(m_parent != null)
			{
				m_parent.RemoveChild(this);
				BTEditorGraphNode.DestroyImmediate(this);
			}
		}

		public void OnDeleteChild(int index)
		{
		
		}

		public int GetChildIndex(BTEditorGraphNode child)
		{
			return m_children.IndexOf(child);
		}

		public void ChangeChildIndex(int sourceIndex, int destinationIndex)
		{
		
		}

		public BTEditorGraphNode GetChild(int index)
		{
			if(index >= 0 && index < m_children.Count)
			{
				return m_children[index];
			}

			return null;
		}

		private void RemoveChild(BTEditorGraphNode child)
		{
			if(m_children.Remove(child))
			{
				if(m_node is Composite)
				{
					Composite composite = m_node as Composite;
					composite.RemoveChild(child.Node);
				}
				else if(m_node is Decorator)
				{
					Decorator decorator = m_node as Decorator;
					decorator.SetChildren(null);
				}
			}
		}

		private void DestroyChildren()
		{
			for(int i=0;i<m_children.Count;i++){
				
			}
			if(m_node is Composite){
				//(Composite)m_node.RemoveAllChildren();
			}
			else if(m_node is Decorator){
				//(Decorator)m_node.SetChildren(null);
			}
	
		}

		private void OnDestroy()
		{
			// m_graph.OnNodeDeselect(this);
			for(int i=0;i<m_children.Count;i++){

			}
		}

		private static BTEditorGraphNode CreateEmptyNode()
		{
			return null;
		}

		private static BTEditorGraphNode CreateExistingNode(BTEditorGraphNode parent, BehaviourNode node)
		{
			BTEditorGraphNode gNode = BTEditorGraphNode.CreateEmptyNode();
			gNode.m_parent = parent;
			gNode.m_graph = parent.Graph;
			gNode.SetExistingNode(node);

			return gNode;
		}

		public static BTEditorGraphNode CreateRoot(BTEditorGraph graph, Root node)
		{
			if(graph!=null && node!=null){
				BTEditorGraphNode gNode= BTEditorGraphNode.CreateEmptyNode();
				gNode.m_graph=graph;
				gNode.SetExistingNode(node);

				return gNode;
			}
			else{
				return null;
			}
		}
	}
}