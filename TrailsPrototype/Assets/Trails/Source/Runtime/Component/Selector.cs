using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails
{
    public class Selector : Composite
    {
        protected int index;
        public override string Title
        {
            get
            {
                return "Selector";
            }
        }

        protected void OnEnter(AIAgent agent)
        {
            index = 0;
        }

        protected BehaviourNodeStatus OnExecute(AIAgent agent)
        {
            BehaviourNodeStatus stat = BehaviourNodeStatus.Success;
            BehaviourNodeStatus result = BehaviourNodeStatus.Success;

            while (index < mChildren_.Count)
            {
                stat = mChildren_[index].Run(agent);
                if (stat == BehaviourNodeStatus.Success)
                {
                    result = stat;
                }
                index++;
            }
            return result;
        }

    }
}
