using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails
{
	public class Repeater : Decorator
	{
		public int repeatCount;
		private int iteration;
        public override string Title
        {
            get
            {
                return "Repeater";
            }
        }

		 public override void OnEnter(AIAgent agent)
        {
        	iteration = 0;
        }

        // AGH
        public override BehaviourNodeStatus OnExecute(AIAgent agent) 
        {
        	BehaviourNodeStatus nodo = BehaviourNodeStatus.Success;
        	
        	if (mChildren_ != null && repeatCount >= 0){ 
        		if (iteration <= repeatCount){
        			
        			if (mChildren_.get() != BehaviourNodeStatus.None ||
        				mChildren_.get() != BehaviourNodeStatus.Running){
        				mChildren_.OnReset();
        			}
        			nodo = mChildren_.Run(agent);

        			if (nodo == BehaviourNodeStatus.Success) {
        				iteration++;
        				if (iteration <= repeatCount) nodo = BehaviourNodeStatus.Running;
        			}
        		}
        	
        	}
        	
        	return nodo;
        }

	}
}