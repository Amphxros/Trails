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
    public static class BTConstraintFactory
    {
        private static List<Tuple<Type, string>> mConstraintMenuPaths;

        static BTConstraintFactory(){
            Type cType= typeof(Constraint);
            Assembly assembly=cType.Assembly;

            mConstraintMenuPaths= new List<Tuple<Type,string>>();
            
            foreach (var type in assembly.GetTypes().Where(t=> t.IsSubclassOf(cType)))
            {
                object[] attrb=type.GetCustomAttributes(typeof(AddConstraintMenuAttribute), false);
                
                if(attrb.Length > 0){
                    AddConstraintMenuAttribute atr = attrb[0] as AddConstraintMenuAttribute;
                    mConstraintMenuPaths.Add(new Tuple<Type,string>(type,atr.menuPath));
                }
            }


        }
        public static void AddConstraint(GenericMenu menu, BehaviourNode node){
            GenericMenu.MenuFunction2 onCreateConstraint= t =>{
               Constraint c= BTUtils.CreateConstraint(t as Type);
                if(c!=null){
                    node.Constraints.Add(c);
                }
            };

            foreach(var cons in mConstraintMenuPaths){
                menu.AddItem(new GUIContent("Add Constraint/" + cons.Item2), false, onCreateConstraint, cons.Item1);
            }
        }
    
    }
}