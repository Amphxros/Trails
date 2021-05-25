using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails
{
    public class Decorator : BehaviourNode
    {
        protected BehaviourNode mChildren_;

        public override void OnBeforeSerialize(BTAsset btAsset)
        {
            base.OnBeforeSerialize(btAsset);
            if (mChildren_ != null)	mChildren_.OnBeforeSerialize(btAsset);
            
        }

        public override void OnAfterDeserialize(BTAsset btAsset)
        {
            base.OnAfterDeserialize(btAsset);
            if (mChildren_ != null)	mChildren_.OnAfterDeserialize(btAsset);
            
        }

        public override void OnEnter(AIAgent agent)
        {
            base.OnEnter(agent);
            if (mChildren_ != null)	mChildren_.OnEnter(agent);

        }

        public override BehaviourNodeStatus OnExecute(AIAgent agent) {
            return BehaviourNodeStatus.None;
        }

        public override void OnStart(AIAgent agent)
        {
            base.OnStart(agent);
            if (mChildren_ != null)	mChildren_.OnStart(agent);
            
        }

        public override void OnReset()
        {
            base.OnReset();
            if (mChildren_ != null)	mChildren_.OnReset();
            
        }

        public BehaviourNode GetChildren(){ return mChildren_;}
        public void SetChildren(BehaviourNode nodo){ mChildren_ = nodo;}

	}
}