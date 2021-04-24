using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Trail
{
    public class DecitionWindow : EditorWindow
    {
        public int marginUp=100, marginLeft=10;
        private bool hasToAddComponent = false;
        private bool hasToAddNode = false;

       [MenuItem("Trails/DecitionBehaviourTree")]
        public static void ShowWindow()
        {
            EditorWindow window= GetWindow<DecitionWindow>("Decition Behaviour tree");
            window.Show();
        }

        //window code
        void OnGUI()
        {
            if (hasEveryonetheComponent()) //if every selected object has the DecitionManager then we can add nodes
            {
                GUILayout.Label("DecitionTree", EditorStyles.boldLabel);
                //show the nodes

                GUILayout.Label("Nodes:", EditorStyles.boldLabel);

                //layer 0 
                GUILayout.BeginHorizontal();

                if (Selection.activeGameObject)
                {
                    Selection.activeGameObject.name =
                        EditorGUILayout.TextField("Object name: ", Selection.activeGameObject.name);
                }

                GUILayout.EndHorizontal();

                string nLayer0="";
                int numLayer0 = 0;
                if (Selection.activeGameObject)
                {
                    Selection.activeGameObject.name =
                        EditorGUILayout.TextField("Layer0: ", nLayer0);

                    numLayer0 = int.Parse(nLayer0);
                    nLayer0 = numLayer0.ToString();
                }

                GUILayout.BeginHorizontal();
                for (int i = 0; i < numLayer0; i++)
                {
                    createNode("Node", "Task");
                }
                GUILayout.EndHorizontal();
                //layer 1



                //aqui estan los botones para añadir cajitas y borrar la lista
                EditorGUILayout.BeginHorizontal();
                //button to add nodes
                if (GUILayout.Button("addNode"))
                {
                    hasToAddNode = true;
                    this.Repaint();
                }
                if (GUILayout.Button("clearList"))
                {
                    clearList();
                    this.Repaint();

                }
               
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Label("There is no decition manager");
                if (GUILayout.Button("addDecitionManager"))
                {
                    hasToAddComponent = true;
                }
            }
        }

        void Update()
        {
          if (hasToAddComponent)
          {
              addDecitionManager();
              hasToAddComponent = false;
          }
          else if (hasToAddNode)
          {
                addNode();
                Repaint();
                hasToAddNode = false;
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

        private void createNode(string nodeId, string behaviour)
        {

            GUILayout.BeginVertical("HelpBox");
            GUILayout.Label(nodeId);
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical("GroupBox");
            GUILayout.Label(behaviour);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private void addNode()
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                go.GetComponent<DecitionManager>().addNode();

            }
        }
        
        private bool isEqual()
        {
            int tam = 0;
            foreach (GameObject go in Selection.gameObjects)
            {
                if(tam!=0 && tam==go.GetComponent<DecitionManager>().getTam())
                {
                tam=go.GetComponent<DecitionManager>().getTam();
                }
                else
                {
                    return false;
                }

            }

            return true;
        }

        private int getTam()
        {
            int tam = 0;
            foreach (GameObject go in Selection.gameObjects)
            {
                if (tam != 0 && tam == go.GetComponent<DecitionManager>().getTam())
                {
                    tam = go.GetComponent<DecitionManager>().getTam();
                }
          
            }

            return tam;
        }
        private void clearList()
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                go.GetComponent<DecitionManager>().clear();
            }
        }

    }
}