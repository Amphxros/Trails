using UnityEditor;
using UnityEngine;
using System.Collections;

namespace Trails
{
    /// <summary>
    /// Ventana principal dnde se ven los nodos en si
    /// </summary>
    public class EditorAIWindow : EditorWindow
    {
       public Task[] behaviour;
        [MenuItem("Trails/Main Window")]
        public static void ShowWindow()
        {
            EditorWindow window=  GetWindow<EditorAIWindow>("Trails");
            window.Show();
        }

        private void OnEnable()
        {

        }

        private void OnGUI()
        {

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Trails", EditorStyles.boldLabel);
            if (GUILayout.Button("add Behaviour tree"))
            {

            }
            if (GUILayout.Button("inspector"))
            {

            }
            if (GUILayout.Button("inspector"))
            {

            }
            if (GUILayout.Button("inspector"))
            {

            }

            EditorGUILayout.EndHorizontal();



            EditorGUILayout.BeginHorizontal();

            GUILayout.Space(20);
            DrawButtons();
            GUILayout.Space(20);
            DrawButtons();
            GUILayout.Space(20);
            DrawButtons();
            EditorGUILayout.EndHorizontal();


        }

        private void DrawButtons()
        {
            EditorGUILayout.BeginVertical();

            GUILayout.Label("Nodes");
            //button to add nodes
            if (GUILayout.Button("add Repeater"))
            {

            }
            GUILayout.Space(5);
            //button to delete all
            if (GUILayout.Button("add Sequence"))
            {

            }
            GUILayout.Space(5);
            if (GUILayout.Button("add Selector"))
            {

            }
            GUILayout.Space(5);
            //button to add a custom tool
            if (GUILayout.Button("add custom Task"))
            {

            }
            GUILayout.Space(5);

            //button to delete all
            if (GUILayout.Button("clear behaviour tree"))
            {

            }
            GUILayout.Space(5);


            GUILayout.Label("Conditionals");
            GUILayout.Space(5);
            if (GUILayout.Button("And"))
            {

            }
            GUILayout.Space(5);
            
            if (GUILayout.Button("Or"))
            {

            }
            GUILayout.Space(5);

            if (GUILayout.Button("Float"))
            {

            }
            GUILayout.Space(5);

            EditorGUILayout.EndVertical();
        }

        private void DrawInspector()
        {
          
        }
        private void SaveSelectedPreset(string name)
        {

        }

        private void RenameSelectedPreset(string name)
        {

        }
        
        private void addTrailsManager()
        {

        }

    }
}