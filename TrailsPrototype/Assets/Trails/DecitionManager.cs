using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trail
{
    public class DecitionManager : MonoBehaviour //the Tree itself
    {
        List<Node> behaviour_tree;
        int tam = 0;
        // Start is called before the first frame update
        void Start()
        {
            behaviour_tree = new List<Node>();
            int tam = 0;
        }
        void OnDisable()
        {
            behaviour_tree.Clear();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public int getTam()
        {
            return tam;
        }
        public void addNode()
        {
            //behaviour_tree.Add(new Node());
            tam++;
            Debug.Log("Node added " +tam );
        }
        public void clear()
        {
            behaviour_tree.Clear();
            tam = 0;

            Debug.Log("cleared list " + tam);
        }
    }
}