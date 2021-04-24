using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trail
{
    public class Node : MonoBehaviour
    {
       public enum NodeType { NODE, SELECTOR, SEQUENCE};
       // MonoBehaviour mComponent;
        
        public Node()
        {
            mNode = NodeType.NODE;
            next = null;
        }
        
        public Node( NodeType node)
        {
            mNode = node;
        }
        NodeType mNode;
        Node[] next;


    }
}