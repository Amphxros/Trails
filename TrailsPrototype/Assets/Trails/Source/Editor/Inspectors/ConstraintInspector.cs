using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;
using Trails;
using Trails.Serialization;
namespace TrailsEditor{
public class ConstraintInspector 
{
    private Constraint mTarget_;
    public Constraint Target{
        get{
            return mTarget_;
        }
        set{
            mTarget_=value;
        }
    }

    public void OnInspectorGUI(){
        if(mTarget_==null){
            return;
        }
        DrawInspector();
    }

    public void DrawInspector()
	{
		DrawProperties();
	}

	public void DrawProperties(){
        Type cType=null;
        var field= from f in cType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) select f;
        var property= from p in cType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) select p;

        foreach(var f in field){
                BTPropertyAttribute propertyAtb = Attribute.GetCustomAttribute(f, typeof(BTPropertyAttribute)) as BTPropertyAttribute;
				BTIgnoreAttribute ignoreAtb = Attribute.GetCustomAttribute(f, typeof(BTIgnoreAttribute)) as BTIgnoreAttribute;
				BTHideInInspectorAttribute hideAtb = Attribute.GetCustomAttribute(f, typeof(BTHideInInspectorAttribute)) as BTHideInInspectorAttribute;
				//string label = BTEditorUtils.MakePrettyName(f.Name);

        }

        foreach(var p in property){

        }

    }

    // protected bool TryToDrawField(string label, object currentValue, Type type, out object value){

    // }
}
}