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
    public static class BTNodeFactory
    {
        private static List<Tuple<Type, string>> mNodeMenuPaths;

        static BTNodeFactory()
        {
            Type node = typeof(BehaviourNode);
            Assembly ass = node.Assembly;

            mNodeMenuPaths = new List<Tuple<Type, string>>();

            foreach (Type t in ass.GetTypes().Where(s => s.IsSubclassOf(node)))
            {
                object[] att = t.GetCustomAttributes(typeof(AddNodeMenuAttribute), false);
                if (att.Length > 0)
                {
                    AddNodeMenuAttribute a = att[0] as AddNodeMenuAttribute;

                    mNodeMenuPaths.Add(new Tuple<Type, string>(t, a.menuPath));
                }
            }

        }

        public static void AddChild(GenericMenu menu, BTEditorGraphNode target)
        {
            GenericMenu.MenuFunction2 onCreateChild = t => target.Graph.OnNodeCreateChild(target, t as Type);
            foreach (var v in mNodeMenuPaths)
            {
                menu.AddItem(new GUIContent("Add child/" + v.Item2), false, onCreateChild, v.Item1);
            }
        }

        public static void SwitchType(GenericMenu menu, BTEditorGraphNode target)
        {
            //GenericMenu.MenuFunction2 onSwitchType = t => target.Graph.OnNodeSwitchType(target, t as Type);

            //Type nodeType = typeof(NodeGroup);
            //if(!target.Node is NodeGroup)
            //{
            //    foreach (var v in mNodeMenuPaths)
            //    {
            //        if (v.Item1.IsSameOrSubClass(nodeType))
            //            continue;

            //        if (((target.Node is Trails.Action) && v.Item1.IsSubclassOf(typeof(Trails.Action))) ||
            //           ((target.Node is Decorator) && v.Item1.IsSubclassOf(typeof(Decorator))) ||
            //           ((target.Node is Composite) && v.Item1.IsSubclassOf(typeof(Composite))))
            //        {
            //            menu.AddItem(new GUIContent("SwitchType/" + v.Item2), false, onSwitchType, v.Item1);
            //        }


            //    }
            //}


        }
    }
}
