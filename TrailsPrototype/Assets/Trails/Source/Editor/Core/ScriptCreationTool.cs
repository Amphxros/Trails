using UnityEngine;
using UnityEditor;
using System.IO;

namespace  TrailsEditor
{
    public class ScriptCreationTool 
    {
        enum CustomScriptType{Composite, Decorator, Action, Constraint, Service};
      
            private static void CreateScript(CustomScriptType mScript, string defaultFileName= "MyCustomScript"){
           
                string path = EditorUtility.SaveFilePanel("Save script", "", defaultFileName, "cs");
			
                if(!string.IsNullOrEmpty(path)){
                    string customScript=System.IO.Path.GetFileNameWithoutExtension(path);
                    using(var writer = System.IO.File.CreateText(path))
    				{
					writer.Write(CreateScriptContent(mScript, customScript));
	    			}
			    	AssetDatabase.Refresh();
                }
            }
        
            private static string CreateScriptContent(CustomScriptType mScript, string scriptName){

               string content = string.Empty;

			try
			{
				TextAsset template = null;
				switch(mScript)
				{
				case CustomScriptType.Composite:
					template = Resources.Load<TextAsset>("Trails/Templates 1/Composite");
					break;
				case CustomScriptType.Decorator:
					template = Resources.Load<TextAsset>("Trails/Templates 1/Decorator");
					break;
				case CustomScriptType.Action:
					template = Resources.Load<TextAsset>("Trails/Templates 1/Action");
					break;
				case CustomScriptType.Constraint:
					template = Resources.Load<TextAsset>("Trails/Templates 1/Constraint");
					break;
				case CustomScriptType.Service:
					template = Resources.Load<TextAsset>("Trails/Templates 1/Service");
					break;
				}

				if(template != null)
					content = string.Format(template.text, scriptName);
            }

            catch (System.Exception e){
                Debug.LogException(e);
                content=string.Empty;
            }

            return content;
            
            }

            [MenuItem("Assets/Create/Trails/Composite")]
            private static void CreateCustomCompositeScript()
		    {
			    CreateScript(CustomScriptType.Composite, "CustomComposite");
            }

            [MenuItem("Assets/Create/Trails/Decorator")]
            private static void CreateCustomDecoratorScript()
		    {
			    
			    CreateScript(CustomScriptType.Decorator, "CustomDecorator");
            }
            [MenuItem("Assets/Create/Trails/Action")]
            private static void CreateCustomActionScript()
		    {
			    CreateScript(CustomScriptType.Action, "CustomAction");
            }
            [MenuItem("Assets/Create/Trails/Constraint")]
            private static void CreateCustomConstraintScript()
		    {
			    CreateScript(CustomScriptType.Constraint, "CustomConstraint");
            }
            [MenuItem("Assets/Create/Trails/Service")]
            private static void CreateCustomServiceScript()
		    {
			    CreateScript(CustomScriptType.Service, "CustomService");
            }

            

            

    }

}
