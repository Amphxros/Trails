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

                }
            }
        
            private static string CreateScriptContent(CustomScriptType mScript, string scriptName){

                return "";
            }

            [MenuItem("Assets/Create/Trails/Composite")]
            private static void CreateCustomCompositeScript()
		    {
			    
            }

            [MenuItem("Assets/Create/Trails/Decorator")]
            private static void CreateCustomDecoratorScript()
		    {
			    
            }
            [MenuItem("Assets/Create/Trails/Action")]
            private static void CreateCustomActionScript()
		    {
			    
            }
            [MenuItem("Assets/Create/Trails/Constraint")]
            private static void CreateCustomConstraintScript()
		    {
			    
            }
            [MenuItem("Assets/Create/Trails/Service")]
            private static void CreateCustomServiceScript()
		    {
			    
            }

            

            

    }

}
