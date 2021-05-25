using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails
{
    public class RepeatForever : Decorator
    {

        public override string Title
        {
            get
            {
                return "Repeat Forever";
            }
        }

        public override BehaviourNodeStatus OnExecute(AIAgent agent)
        {
            if (mChildren_ != null) {

                if (mChildren_.get() != BehaviourNodeStatus.None ||
                    mChildren_.get() != BehaviourNodeStatus.Running)
                {
                    mChildren_.OnReset();
                }
                mChildren_.Run(agent);
            }
            return BehaviourNodeStatus.Running;
        }
    }
}