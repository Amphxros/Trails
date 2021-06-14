using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;
using UnityEditor;

using Trails;

namespace TrailsEditor
{
    public static class BTContextMenuFactory 
    {
        public static GenericMenu CreateNodeContextMenu(BTEditorGraphNode target)
        {
            
            GenericMenu menu = new GenericMenu();
            bool canAddChild = false;

            if (target.Node is Composite)
            {
                canAddChild = true;
            }
            else if(target.Node is Decorator)
            {
                canAddChild = ((Decorator)target.Node).GetChildren() == null;
            }
            else if (target.Node is NodeGroup)
            {
                canAddChild = ((NodeGroup)target.Node).GetChildren() == null && target.IsRoot;
            }
            else if (target.Node is Trails.Action)
            {
                canAddChild = false;
            }


            if (target.Graph.ReadOnly)
            {
                if (canAddChild)
                {
                    //añadir hijo aqui
                }

                //añadir constraint aqui

                if (!(target.Node is NodeGroup))
                {

                }

                if (canAddChild || !(target.Node is NodeGroup))
                {
                    menu.AddSeparator("");
                }

                if (target.IsRoot)
                {
                    menu.AddDisabledItem(new GUIContent("Copy"));
                    menu.AddDisabledItem(new GUIContent("Cut"));
                }
                else
                {
                    menu.AddItem(new GUIContent("Copy"), false, () => { /*target.Graph.OnCopyNode(target))*/; });
                    menu.AddItem(new GUIContent("Cut"), false, () =>
                    {
                       // target.Graph.OnCopyNode(targetNode);
                        //target.Graph.OnNodeDelete(targetNode);
                    });
                }

            }


            return menu;

        }

        public static GenericMenu CreateGraphContextMenu(BTEditorGraph graph)
        {

            GenericMenu menu = new GenericMenu();
            return menu;

        }
        public static GenericMenu CreateBehaviourTreeEditorMenu(BehaviourTreeEditor editor)
        {

            GenericMenu menu = new GenericMenu();
            return menu;

        }

        private static void CreateHelpOptions(GenericMenu menu)
        { 
       
        }

        public static GenericMenu CreateNodeInspectorContextMenu(BehaviourNode targetNode)
        {

            GenericMenu menu = new GenericMenu();
            return menu;
        }

        public static GenericMenu CreateConstraintContextMenu(BehaviourNode targetNode, int constraintIndex)
        {
            GenericMenu menu = new GenericMenu();
            return menu;
        }

        public static GenericMenu CreateServiceContextMenu(BehaviourNode targetNode, int serviceIndex)
        {

            GenericMenu menu = new GenericMenu();
            return menu;

        }

    }
}