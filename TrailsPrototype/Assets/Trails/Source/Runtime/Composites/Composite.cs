using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails
{
    public class Composite : BehaviourNode
    {
        protected List<BehaviourNode> mChildren_;

        public int Count
        {
            get
            {
                return mChildren_.Count;
            }
        }

        public override void OnBeforeSerialize(BTAsset btAsset)
        {
            base.OnBeforeSerialize(btAsset);
            foreach (BehaviourNode b in mChildren_)
            {
                b.OnBeforeSerialize(btAsset);
            }
        }

        public override void OnAfterDeserialize(BTAsset btAsset)
        {
            base.OnAfterDeserialize(btAsset);
            foreach (BehaviourNode b in mChildren_)
            {
                b.OnAfterDeserialize(btAsset);
            }
        }

        public override void OnEnter(AIAgent agent)
        {
            base.OnEnter(agent);
            foreach (BehaviourNode b in mChildren_)
            {
                b.OnEnter(agent);
            }

        }

        public override BehaviourNodeStatus OnExecute(AIAgent agent) {
            return BehaviourNodeStatus.None;
        }

        public override void OnStart(AIAgent agent)
        {
            base.OnStart(agent);
            foreach (BehaviourNode b in mChildren_)
            {
                b.OnStart(agent);
            }
        }

        public override void OnReset()
        {
            base.OnReset();
            foreach (BehaviourNode b in mChildren_)
            {
                b.OnReset();
            }
        }

        public void InsertChild(int index, BehaviourNode child)
        {
            if (child != null)
                mChildren_.Insert(index,child);
        }

        public void AddChild(BehaviourNode child)
        {
            if (child != null)
                mChildren_.Add(child);
        }

        public void RemoveChild(BehaviourNode child)
        {
            if (child != null)
                mChildren_.Remove(child);
        }

        public void RemoveChild(int index)
        {
            if (index >= 0 && index < mChildren_.Count)
                mChildren_.RemoveAt(index);
        }

        public void RemoveAllChildren()
        {
            mChildren_.Clear();
        }

        public void MoveChildPriorityUp(int index)
        {
            if (index > 0 && index < mChildren_.Count)
            {
                var temp = mChildren_[index];
                mChildren_[index] = mChildren_[index + 1];
                mChildren_[index + 1] = temp;
            }
        }

        public void MoveChildPriorityDown(int index)
        {
            if (index >= 0 && index < mChildren_.Count - 1)
            {
                var temp = mChildren_[index];
                mChildren_[index] = mChildren_[index - 1];
                mChildren_[index - 1] = temp;
            }
        }

        public BehaviourNode GetChild(int index)
        {
            if (index >= 0 && index < mChildren_.Count)
            {
                return mChildren_[index];
            }
            else return null;
        }

        public Composite()
        {
            mChildren_ = new List<BehaviourNode>();
        }
    }
}