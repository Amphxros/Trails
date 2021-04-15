using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trail
{
    public class DecitionManager : MonoBehaviour
    {
        List<Node> behaviour_tree;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void addNode()
        {
            behaviour_tree.Add(new Node());
        }
    }
}