using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails.Serialization;

namespace Trails{

// [AddNodeMenu("Action/Wait")]
public class Wait : Action
{
	[BTProperty("Duration")]
	private MemoryVar mDuration_;

	private float m_startTime;
   
    public Wait(){
		mDuration_ = new MemoryVar();
	}
    public override void OnEnter(AIAgent agent){
			m_startTime = Time.time;
	}

    public override BehaviourNodeStatus OnExecute(AIAgent agent){
			float duration =0;// mDuration_.AsFloat.HasValue ? mDuration_.AsFloat.Value : m_duration.Evaluate<float>(agent.Blackboard, 0.0f);

			if(Time.time < m_startTime + duration){
				return BehaviourNodeStatus.Running;
            }
			return BehaviourNodeStatus.Success;
	}

}
}