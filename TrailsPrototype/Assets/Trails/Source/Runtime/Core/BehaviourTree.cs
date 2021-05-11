using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

 
}
