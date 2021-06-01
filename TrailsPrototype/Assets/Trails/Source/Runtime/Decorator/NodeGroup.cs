using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails
{
    [AddNodeMenu("Utility/Node Group")]

    public class NodeGroup : Decorator
    {
        public override string Title
        {
            get
            {
                return "Node Group";
            }
        }

        public override BehaviourNodeStatus OnExecute(AIAgent agent)
        {
            if (mChildren_ != null) mChildren_.Run(agent);
            return BehaviourNodeStatus.Success; 
        }
    }
}
