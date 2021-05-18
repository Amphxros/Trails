using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails
{
    public class Sequence : Composite
    {
        protected int index;
        public override string Title
        {
            get
            {
                return "Sequence";
            }
        }

        public override void OnEnter(AIAgent agent)
        {
            index = 0;
        }


        public override BehaviourNodeStatus OnExecute(AIAgent agent)
        {
            BehaviourNodeStatus stat = BehaviourNodeStatus.Success;
            bool exit = false;  // Exit condition of the sequence

            while (!exit && index < mChildren_.Count)
            {
                stat = mChildren_[index].Run(agent);
                exit = (stat != BehaviourNodeStatus.Success);
                index++;
            }
            return stat;
        }

    }
}
