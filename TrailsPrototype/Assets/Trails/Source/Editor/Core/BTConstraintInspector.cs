  
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Trails;

using UnityEditor;
using UnityEngine;

namespace TrailsEditor{
    public class BTConstraintInspectorAttribute : Attribute
    {
        public readonly Type ConstraintType;

		public BTConstraintInspectorAttribute(Type constraintType)
		{
			ConstraintType = constraintType;
		}
    }

    public static class BTConstraintInspector{
        private static Dictionary<Type, Type> mConstraintInspectors_;

		static BTConstraintInspector()
		{
			Assembly ass = Assembly.GetExecutingAssembly();

			mConstraintInspectors_ = new Dictionary<Type, Type>();
			foreach(Type t in ass.GetTypes().Where(t => t.IsSubclassOf(typeof(ConstraintInspector))))
			{
				object[] att = t.GetCustomAttributes(typeof(BTConstraintInspectorAttribute), false);
				if(att.Length > 0)
				{
					BTConstraintInspectorAttribute attribute = att[0] as BTConstraintInspectorAttribute;
					if(!mConstraintInspectors_.ContainsKey(attribute.ConstraintType))
					{
						mConstraintInspectors_.Add(attribute.ConstraintType, t);
					}
				}
			}
		}
        
		public static Type GetInspectorTypeForConstraint(Type constraintType)
		{
			Type inspectorType = null;

			if(mConstraintInspectors_.TryGetValue(constraintType, out inspectorType))
			{
				return inspectorType;
			}
			else
			{
				return typeof(ConstraintInspector);
			}
		}

        
		public static ConstraintInspector CreateInspectorForConstraint(Constraint constraint)
		{
			if(constraint != null)
			{
				Type inspectorType = GetInspectorTypeForConstraint(constraint.GetType());
				if(inspectorType != null)
				{
					ConstraintInspector inspector = Activator.CreateInstance(inspectorType) as ConstraintInspector;
					inspector.Target = constraint;

					return inspector;
				}
			}

			return null;
		}
    }
	
    

}
