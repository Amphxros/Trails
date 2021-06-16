using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails
{
    public class BehaviourTree
    {
        private Root mRoot_; //inicio de cuaquier arbol

        public Root Root
        {
            get
            {
                return mRoot_;
            }
        }

        public bool isReadOnly { get; set; }

        public BehaviourTree()
        {
    
            mRoot_ = new Root();
        
        }

    }
}