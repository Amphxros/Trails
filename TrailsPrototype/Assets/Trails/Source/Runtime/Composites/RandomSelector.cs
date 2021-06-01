using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails
{
    public class RandomSelector : Composite
    {
        protected int index;
        protected List<int> state;
        protected int currentChild;

        public override string Title
        {
            get
            {
                return "Random Selector";
            }
        }

        public override void OnEnter(AIAgent agent)
        {
            index = 0;
            state = new List<int>();
        }

        public override BehaviourNodeStatus OnExecute(AIAgent agent)
        {
            BehaviourNodeStatus stat = BehaviourNodeStatus.Success;
            BehaviourNodeStatus result = BehaviourNodeStatus.Success;

            while (currentChild < mChildren_.Count)
            {
                if (state.Count == 0)
                {
                    index = Random.Range(0, mChildren_.Count);
                    state.Add(index);
                }
                else {
                    int j = 0;
                    while(j < state.Count && state[j] != index){

                        j++;
                    }
                    if (j >= state.Count) {
                        state.Add(index);

                        stat = mChildren_[index].Run(agent);
                        if (stat == BehaviourNodeStatus.Success)
                        {
                            result = stat;
                        }
                        currentChild++;
                    }
                
                }

            }
            return result;
        }

    }
}