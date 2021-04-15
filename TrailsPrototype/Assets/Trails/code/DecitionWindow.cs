using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Trail
{
    public class DecitionWindow : EditorWindow
    {
        [MenuItem("Trails/DecitionTree")]
        public static void ShowWindow()
        {
            GetWindow<DecitionWindow>("DecitionTree");
        }

        //window code
        void OnGUI()
        {
            if (hasEveryonetheComponent()) //if every selected object has the DecitionManager then we can add nodes
            {
                GUILayout.Label("DecitionTree", EditorStyles.boldLabel);
                //show the nodes



                //button to add nodes
                if (GUILayout.Button("addNode"))
                {

                }
            }
            else
            {
                GUILayout.Label("There is no decition manager");
                if (GUILayout.Button("addDecitionManager"))
                {
                    addDecitionManager();
                }
            }
        }
        private void addDecitionManager()
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                if (obj.GetComponent<DecitionManager>() == null)
                {
                    obj.AddComponent<DecitionManager>();
                }
            }
        }
        private bool hasEveryonetheComponent()
        {
            bool result = true;
            int i = 0;
            while (i < Selection.gameObjects.Length && result)
            {
                result = Selection.gameObjects[i].GetComponent<DecitionManager>() != null &&
                    Selection.gameObjects[i].GetComponent<DecitionManager>().enabled;
                i++;
            }

            return result;
        }

    }
}