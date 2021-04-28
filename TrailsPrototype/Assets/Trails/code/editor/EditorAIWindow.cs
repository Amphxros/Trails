using UnityEditor;
using UnityEngine;
using System.Collections;

public class EditorAIWindow : EditorWindow
{
    private const string SaveDataPath = "Assets/Resources/TrailsSaveData.asset";





    [MenuItem("Window/Trails")]
    private static void ShowWindow()
    {
        EditorWindow window = GetWindow<EditorAIWindow>("Behaviour tree");
        window.Show();
    }

    private void OnEnable()
    {
       
    }

    private void OnGUI()
    {
      
    }

    private void DrawSelectionGUI()
    {
  
    }

    private void SaveSelectedPreset(string name)
    {
       
    }

    private void RenameSelectedPreset(string name)
    {
       
    }

    private void DeleteSelectedPreset()
    {
    
    }

    private void DrawConfigGUI()
    {
       
    }
}
