using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails
{
    public class Composite : MonoBehaviour
    {
       protected List<BehaviourNode> mChildren_;

       public int Count{
        get{
            return mChildren_.Count;
        }
       }

       public Composite(){
           mChildren_= new List<BehaviourNode>();
       }
    }
}