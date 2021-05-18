using System;
using UnityEngine;

namespace Trails{
public abstract class Action : BehaviourNode
{
    public Action(){

    }
    
    public override string Title{
        get{
            return "Action";
        }
    }
}
}